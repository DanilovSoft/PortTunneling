using PortTunneling.Models;
using PortTunneling.Properties;
using PortTunneling.Server;
using PortTunneling.ServerMode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortTunneling
{
	public partial class Form1 : Form
	{
        private readonly TunnelsHolder _tunnelsHolder = new TunnelsHolder();
        private readonly PhysicalAddress _mac;
        /// <summary>
        /// Список активных туннелей для режима клиент.
        /// Содержит <see cref="ClientModeConnectionHolder"/>.
        /// </summary>
        private readonly BindingList<TcpTunnel> _clientModeOnlineTunnels = new BindingList<TcpTunnel>();
        /// <summary>
        /// Список подключений к серверу.
        /// </summary>
        private readonly BindingList<SocketConnectionModel> _serverActiveConnections = new BindingList<SocketConnectionModel>();
        private readonly BindingList<ClientModeReadyToUseModel> _clientModeReadyToUseConnections = new BindingList<ClientModeReadyToUseModel>();
		private bool _needSaveProperties;
        private string _clientMode_host;
        private int _clientMode_port;
        private Server.Listener _listener;
        private ClientModeConnectionHolder _conHolder;

        // ctor.
        public Form1()
		{
			InitializeComponent();
            PopulateImageList();

            numericUpDown_serverPort.DataBindings.Add("Value", GlobalVars.Settings, nameof(Config.ServerPort), false, DataSourceUpdateMode.OnPropertyChanged);
			numericUpDown_client.DataBindings.Add("Value", GlobalVars.Settings, "ClientPort", false, DataSourceUpdateMode.OnPropertyChanged);
			serviceControl_server.DataBindings.Add("ServiceName", GlobalVars.Settings, "ServerServiceName", false, DataSourceUpdateMode.OnPropertyChanged);
			serviceControl_client.DataBindings.Add("ServiceName", GlobalVars.Settings, "ClientServiceName", false, DataSourceUpdateMode.OnPropertyChanged);
			textBox_host.DataBindings.Add("Text", GlobalVars.Settings, "ClientHost", false, DataSourceUpdateMode.OnPropertyChanged);
            GlobalVars.Settings.PropertyChanged += delegate { _needSaveProperties = true; };

			serviceControl_client.Args = "/client";

            _mac = NetworkInterface.GetAllNetworkInterfaces().First(x => x.OperationalStatus == OperationalStatus.Up && x.GetIPProperties().GatewayAddresses.Any()).GetPhysicalAddress();
			maskedTextBox_mac.Text = _mac.ToString();

			dataGridView_clientMode_OnlineTunnels.AutoGenerateColumns = false;
			dataGridView_clientMode_OnlineTunnels.DataSource = _clientModeOnlineTunnels;

			dataGridView_ServerActiveConnections.AutoGenerateColumns = false;
			dataGridView_ServerActiveConnections.DataSource = _serverActiveConnections;

            dataGridView_ClientModeConnections.AutoGenerateColumns = false;
            dataGridView_ClientModeConnections.DataSource = _clientModeReadyToUseConnections;

			UpdateServices();

			TcpTunnel.TunnelCreated += TunnelCreated;
			TcpTunnel.TunnelClosed += TunnelClosed;

            dataGridView1.AutoGenerateColumns = false;

            DataTable tunnels = GlobalVars.SQL.GetAllTunnels();
            tunnels.RowChanged += Tunnels_RowChanged;
            tunnels.ColumnChanged += Tunnels_ColumnChanged;
            dataGridView1.DataSource = tunnels;
        }

        private void Tunnels_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            linkLabel_save.Text = "save*";
        }

        private void Tunnels_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            linkLabel_save.Text = "save*";

            //var added = dt.GetChanges(DataRowState.Added);
            //var deleted = dt.GetChanges(DataRowState.Deleted);
            //var modified = dt.GetChanges(DataRowState.Modified);

            //if(added != null || deleted != null || modified != null)
            //{
            //    linkLabel_save.Text = "save*";
            //}
            //else
            //{
            //    linkLabel_save.Text = "save";
            //}
        }

        //private void TunnelsHolder_ListenerClosed(object sender, TunnelsHolder.Listener listener)
        //{
        //    if (InvokeRequired)
        //        BeginInvoke((Action<TunnelsHolder.Listener>)TunnelsHolder_OnListenerClosed, listener);
        //    else
        //        TunnelsHolder_OnListenerClosed(listener);
        //}

        //private void TunnelsHolder_OnListenerClosed(TunnelsHolder.Listener listener)
        //{
        //    var model = (ListenerModel)listener.Tag;
        //    _activeListeners.Remove(model);
        //}

        //private void TunnelsHolder_NewListener(object sender, TunnelsHolder.Listener e)
        //{
        //    if (InvokeRequired)
        //        BeginInvoke((Action<TunnelsHolder.Listener>)TunnelsHolder_OnNewListener, e);
        //    else
        //        TunnelsHolder_OnNewListener(e);
        //}

        //private void TunnelsHolder_OnNewListener(TunnelsHolder.Listener listener)
        //{
        //    var model = new ListenerModel(listener);
        //    listener.Tag = model;
        //    _activeListeners.Add(model);
        //}

        private void PopulateImageList()
        {
            imageList1.Images.Add(Resources._0);
            imageList1.Images.Add(Resources._1);
            imageList1.Images.Add(Resources._2);
        }

        private void TunnelClosed(object sender, EventArgs e)
		{
            if (InvokeRequired)
                BeginInvoke((Action<TcpTunnel>)ClientMode_OnTunnelClosed, sender);
            else
                ClientMode_OnTunnelClosed((TcpTunnel)sender);
        }

        private void ClientMode_OnTunnelClosed(TcpTunnel tunnel)
        {
            Debug.WriteLine("_clientConnections.Remove");
            _clientModeOnlineTunnels.Remove(tunnel);

            var model = (SocketConnectionModel)tunnel.Connection.Tag;
            _serverActiveConnections.Remove(model);
        }

        private void TunnelCreated(object sender, EventArgs e)
		{
            if (InvokeRequired)
                BeginInvoke((Action<TcpTunnel>)ClientMode_OnTunnelCreated, sender);
            else
                ClientMode_OnTunnelCreated((TcpTunnel)sender);
        }

        private void ClientMode_OnTunnelCreated(TcpTunnel tunnel)
        {
            Debug.WriteLine("_clientConnections.Add");
            _clientModeOnlineTunnels.Add(tunnel);
        }

        private void ClientMode_TunnelCreated(object sender, ClientModeConnectionHolder.Connection e)
        {
            if (InvokeRequired)
                BeginInvoke((Action<ClientModeConnectionHolder.Connection>)ClientMode_OnTunnelCreated, e);
            else
                ClientMode_OnTunnelCreated(e);
        }

        private void ClientMode_OnTunnelCreated(ClientModeConnectionHolder.Connection connection)
        {
            var dto = (ClientModeReadyToUseModel)connection.Tag;
            _clientModeReadyToUseConnections.Remove(dto);
        }

		private void Button_server_Launch_Click(object sender, EventArgs e)
		{
			var me = (Button)sender;

            if (me.Text == "Stop")
            {
                _listener.Abort();

                _tunnelsHolder.DisableTunnels();

                tabControl1.TabPages.Add(tabPage_client);
                numericUpDown_serverPort.ReadOnly = false;

                me.Text = "Launch";
                GC.Collect();
            }
            else
            {
                try
                {
                    int port = (int)numericUpDown_serverPort.Value;
                    var endPoint = new IPEndPoint(IPAddress.Any, port);

                    dataGridView1.SuspendLayout();
                    _tunnelsHolder.EnableTunnels();
                    dataGridView1.ResumeLayout();

                    var listener = new Server.Listener(endPoint);
                    listener.Connected += Listener_Connected;
                    listener.Disconnected += Listener_Disconnected;
                    listener.Listen();
                    _listener = listener;

                    //Server.API.ApiListener.Instance.Enable();

                    me.Text = "Stop";
                    //me.Enabled = false;
                    tabControl1.TabPages.Remove(tabPage_client);
                    numericUpDown_serverPort.ReadOnly = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.GetBaseException().Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
		}

        private void Listener_Disconnected(object sender, SocketConnection connection)
		{
            if (InvokeRequired)
                BeginInvoke((Action<SocketConnection>)Listener_OnDisconnected, connection);
            else
                Listener_OnDisconnected(connection);
        }

        private void Listener_OnDisconnected(SocketConnection connection)
        {
            var model = (SocketConnectionModel)connection.Tag;
            _serverActiveConnections.Remove(model);
        }

        private void Listener_Connected(object sender, SocketConnection connection)
		{
            if (InvokeRequired)
                Invoke((Action<SocketConnection>)Listener_OnConnected, connection);
            else
                Listener_OnConnected(connection);
        }

        private void Listener_OnConnected(SocketConnection connection)
        {
            var model = new SocketConnectionModel(connection, this);
            connection.Tag = model;
            _serverActiveConnections.Add(model);
        }

        private void Button_client_launch_Click(object sender, EventArgs e)
		{
			var me = (Button)sender;

            if (me.Text == "Stop")
            {
                me.Text = "Launch";

                _conHolder.Abort();

                tabControl1.TabPages.Insert(0, tabPage_server);
                numericUpDown_client.ReadOnly = false;
                textBox_host.ReadOnly = false;
            }
            else
            {
                try
                {
                    _clientMode_host = textBox_host.Text;
                    _clientMode_port = (short)numericUpDown_client.Value;

                    StartConnectionHolder();

                    //me.Enabled = false;
                    me.Text = "Stop";
                    tabControl1.TabPages.Remove(tabPage_server);
                    numericUpDown_client.ReadOnly = true;
                    textBox_host.ReadOnly = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.GetBaseException().Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
		}

        private void StartConnectionHolder()
        {
            var connection = new ClientModeConnectionHolder(new DnsEndPoint(_clientMode_host, _clientMode_port), _mac);
            connection.TunnelCreated += ClientMode_TunnelCreated;
            connection.ConnectionDestroyed += Connection_ConnectionDestroyed;
            connection.NewConnection += Connection_NewConnection;
            connection.StartConnections((int)numeric_maxCon.Value);
            _conHolder = connection;
        }

        private void Connection_NewConnection(object sender, ClientModeConnectionHolder.Connection e)
        {
            if (InvokeRequired)
                Invoke((Action<ClientModeConnectionHolder.Connection>)Connection_OnNewConnection, e);
            else
                Connection_OnNewConnection(e);
        }

        private void Connection_OnNewConnection(ClientModeConnectionHolder.Connection connection)
        {
            var dto = new ClientModeReadyToUseModel(connection, this);
            connection.Tag = dto;
            _clientModeReadyToUseConnections.Add(dto);
        }

        private void Connection_ConnectionDestroyed(object sender, ClientModeConnectionHolder.Connection e)
        {
            if(InvokeRequired)
                Invoke((Action<ClientModeConnectionHolder.Connection>)Connection_OnConnectionDestroyed, e);
            else
                Connection_OnConnectionDestroyed(e);
        }

        private void Connection_OnConnectionDestroyed(ClientModeConnectionHolder.Connection connection)
        {
            var model = (ClientModeReadyToUseModel)connection.Tag;
            _clientModeReadyToUseConnections.Remove(model);
        }

        private void DataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
            var col = dataGridView1.Columns[e.ColumnIndex];
            var row = dataGridView1.Rows[e.RowIndex];

            if (col == Column_client_mac)
            {
                row.ErrorText = "";

                // Don't try to validate the 'new row' until finished  
                // editing since there 
                // is not any point in validating its initial value.
                if (row.IsNewRow)
                    return;

                Control control = dataGridView1.EditingControl;
                if (e.FormattedValue is string sMac && control != null)
                {
                    sMac = sMac
                        .Replace(":", "")
                        .Replace("-", "")
                        .Replace(" ", "");

                    if (sMac.Length == 12 && Utils.IsHex(sMac))
                    {
                        control.Text = sMac;
                    }
                    else
                    {
                        dataGridView1.Rows[e.RowIndex].ErrorText = "Invalid MAC";
                        e.Cancel = true;
                    }
                }
            }
            else if (col == Column_port_a || col == Column_port_b)
            {
                row.ErrorText = "";

                if (row.IsNewRow)
                    return;

                Control control = dataGridView1.EditingControl;
                if (e.FormattedValue is string port && control != null)
                {
                    if(!ushort.TryParse(port, out _))
                    {
                        dataGridView1.Rows[e.RowIndex].ErrorText = $"Port should be not greater than {ushort.MaxValue}";
                        e.Cancel = true;
                    }
                }
            }
		}

		private void DataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
		{
			dataGridView1.Rows[e.RowIndex].ErrorText = "";
		}

		private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			dataGridView1.Rows[e.RowIndex].ErrorText = "";
		}

		private void LinkLabel_save_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
                if (dataGridView1.DataSource is DataTable dt)
                {
                    var added = dt.GetChanges(DataRowState.Added);
                    var deleted = dt.GetChanges(DataRowState.Deleted);
                    var modified = dt.GetChanges(DataRowState.Modified);

                    if (added != null)
                    {
                        var args = added.AsEnumerable().Select(x => new object[]
                        {
                            x["dst_host"],
                            x["src_port"],
                            x["dst_port"],
                            x["description"],
                            x["mac"],
                            x["enabled"],
                        }).ToArray();

                        int n = 0; var argNames = string.Join(",", args.Select(x => $"({string.Join(",", x.Select(y => "@" + n++))})"));
                        GlobalVars.SQL.Void($"INSERT INTO tunnels (dst_host, src_port, dst_port, description, mac, enabled) VALUES {argNames}", args.SelectMany(x => x).ToList());
                    }

                    if (modified != null)
                    {
                        foreach (DataRow row in modified.Rows)
                        {
                            foreach (DataColumn col in modified.Columns)
                            {
                                var val = row[col, DataRowVersion.Original];
                                var val2 = row[col, DataRowVersion.Current];

                                if (!val.Equals(val2))
                                {
                                    GlobalVars.SQL.Void(string.Format("UPDATE tunnels SET {0} = @0 WHERE id = {1}", col.ColumnName, row["id"]), new object[] { val2 });
                                }
                            }
                        }
                    }

                    if (deleted != null)
                    {
                        string ids = string.Join(",", deleted.AsEnumerable().Select(x => x["id", DataRowVersion.Original]));
                        GlobalVars.SQL.Void($"DELETE FROM tunnels WHERE id IN({ids})");
                    }

                    dt.AcceptChanges();
                    _tunnelsHolder.UpdateMapping();
                    linkLabel_save.Text = "save";
                }
            }
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.GetBaseException().Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void Timer_services_Tick(object sender, EventArgs e)
		{
			UpdateServices();
		}

        private void UpdateServices()
		{
			var services = ServiceController.GetServices();

			var serverServName = serviceControl_server.ServiceName;
			var clientServName = serviceControl_client.ServiceName;

			ServiceController service;
			if (!string.IsNullOrEmpty(serverServName) && (service = services.FirstOrDefault(x => x.ServiceName.Equals(serverServName, StringComparison.InvariantCultureIgnoreCase))) != null)
				serviceControl_server.ServiceSatatus = service.Status.ToString();
			else
				serviceControl_server.ServiceSatatus = "Service not exist";

			if (!string.IsNullOrEmpty(clientServName) && (service = services.FirstOrDefault(x => x.ServiceName.Equals(clientServName, StringComparison.InvariantCultureIgnoreCase))) != null)
				serviceControl_client.ServiceSatatus = service.Status.ToString();
			else
				serviceControl_client.ServiceSatatus = "Service not exist";
		}

		private void RoleChoose_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (_needSaveProperties)
				GlobalVars.Settings.Save();
		}

		private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.ColumnIndex > -1)
			{
				if (dataGridView1.Columns[e.ColumnIndex] == Column_client_mac)
				{
					var sMac = e.Value as string;
					if (!string.IsNullOrEmpty(sMac))
					{
						try
						{
                            e.Value = PhysicalAddress.Parse(sMac).ToString(":");
							e.FormattingApplied = true;
						}
						catch (Exception ex)
                        {
                            Debug.WriteLine(ex);
                        }
					}
				}
			}
		}

		private void DataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
		{
			e.Row.Cells["enabled"].Value = false;
		}

		private void DataGridView_connections_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
            DataGridViewColumn column = dataGridView_ServerActiveConnections.Columns[e.ColumnIndex];
			if (e.Value != null)
			{
				if (column == Column_macServ)
				{
                    e.Value = ((PhysicalAddress)e.Value).ToString(":");
                    e.FormattingApplied = true;
                }
				else if (column == Column_dstaddr)
				{
                    if (e.CellStyle.Font.Italic)
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Regular);
                    
                    var endPoint = (DnsEndPoint)e.Value;
					e.Value = endPoint.Host + ":" + endPoint.Port;
                    e.FormattingApplied = true;
                }
			}
            else
            {
                if (column == Column_dstaddr)
                {
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Italic);
                    var model = (SocketConnectionModel)dataGridView_ServerActiveConnections.Rows[e.RowIndex].DataBoundItem;

                    if (model.Initialized)
                    {
                        e.Value = "Ready to use";
                    }
                    else
                    {
                        e.Value = "Initializing";
                    }
                    e.FormattingApplied = true;
                }
            }
		}

		private void DataGridView_client_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (dataGridView_clientMode_OnlineTunnels.Columns[e.ColumnIndex] == Column_endPoint)
			{
				var endPoint = (DnsEndPoint)e.Value;
				e.Value = endPoint.Host + ":" + endPoint.Port;
                e.FormattingApplied = true;
            }
		}

		//private void DataGridView_servTunnels_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		//{
		//	var column = dataGridView_servOnlineTunnels.Columns[e.ColumnIndex];
		//	if (column == Column2)
		//	{
		//		var endPoint = (DnsEndPoint)e.Value;
		//		e.Value = endPoint.Host + ":" + endPoint.Port;
  //              e.FormattingApplied = true;
  //          }
		//	else if (column == Column_mac3)
		//	{
  //              e.Value = ((PhysicalAddress)e.Value).ToString(":");
  //              e.FormattingApplied = true;
  //          }
		//}

		private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Application.Restart();
		}

        private void DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= PortNumber_KeyPress;
            e.Control.KeyPress -= Mac_KeyPress;

            var col = dataGridView1.CurrentCell.OwningColumn;
            if (col == Column_port_a || col == Column_port_b)
            {
                if (e.Control is TextBox tb)
                    tb.KeyPress += PortNumber_KeyPress;
            }
            else if (col == Column_client_mac)
            {
                if (e.Control is TextBox tb)
                {
                    tb.KeyPress += Mac_KeyPress;
                }
            }
        }

        private void PortNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //private void Mac_KeyPressCancel(object sender, KeyPressEventArgs e)
        //{
        //    if (!char.IsControl(e.KeyChar))
        //    {
        //        e.Handled = true;
        //    }
        //}

        private void Mac_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                if (!Utils.IsHex(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            var con = (int)numeric_maxCon.Value;

            _conHolder?.SetMaxConnections(con);
        }
    }
}
