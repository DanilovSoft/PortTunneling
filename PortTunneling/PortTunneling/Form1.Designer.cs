namespace PortTunneling
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_server = new System.Windows.Forms.TabPage();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.serviceControl_server = new PortTunneling.ServiceControl();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView_ServerActiveConnections = new System.Windows.Forms.DataGridView();
            this.ColumnConnectionId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_macServ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_dstaddr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.enabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column_port_a = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_ip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_port_b = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_client_mac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.linkLabel_save = new System.Windows.Forms.LinkLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericUpDown_serverPort = new System.Windows.Forms.NumericUpDown();
            this.button_launch = new System.Windows.Forms.Button();
            this.tabPage_client = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.dataGridView_ClientModeConnections = new System.Windows.Forms.DataGridView();
            this.ColumnNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.maskedTextBox_mac = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.dataGridView_clientMode_OnlineTunnels = new System.Windows.Forms.DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_startDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_endPoint = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBox_host = new System.Windows.Forms.TextBox();
            this.numericUpDown_client = new System.Windows.Forms.NumericUpDown();
            this.button2 = new System.Windows.Forms.Button();
            this.serviceControl_client = new PortTunneling.ServiceControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.numeric_maxCon = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage_server.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ServerActiveConnections)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_serverPort)).BeginInit();
            this.tabPage_client.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ClientModeConnections)).BeginInit();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_clientMode_OnlineTunnels)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_client)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_maxCon)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_server);
            this.tabControl1.Controls.Add(this.tabPage_client);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(792, 526);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage_server
            // 
            this.tabPage_server.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_server.Controls.Add(this.linkLabel1);
            this.tabPage_server.Controls.Add(this.serviceControl_server);
            this.tabPage_server.Controls.Add(this.groupBox3);
            this.tabPage_server.Controls.Add(this.groupBox1);
            this.tabPage_server.Controls.Add(this.groupBox2);
            this.tabPage_server.Location = new System.Drawing.Point(4, 22);
            this.tabPage_server.Name = "tabPage_server";
            this.tabPage_server.Size = new System.Drawing.Size(784, 500);
            this.tabPage_server.TabIndex = 0;
            this.tabPage_server.Text = "Server";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(43, 71);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(94, 13);
            this.linkLabel1.TabIndex = 24;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "restart application";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
            // 
            // serviceControl_server
            // 
            this.serviceControl_server.Args = null;
            this.serviceControl_server.AutoScroll = true;
            this.serviceControl_server.AutoSize = true;
            this.serviceControl_server.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.serviceControl_server.HeaderName = "Service";
            this.serviceControl_server.Location = new System.Drawing.Point(515, 6);
            this.serviceControl_server.Margin = new System.Windows.Forms.Padding(0);
            this.serviceControl_server.Name = "serviceControl_server";
            this.serviceControl_server.ServiceName = "";
            this.serviceControl_server.ServiceSatatus = "";
            this.serviceControl_server.Size = new System.Drawing.Size(264, 95);
            this.serviceControl_server.TabIndex = 23;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.dataGridView_ServerActiveConnections);
            this.groupBox3.Location = new System.Drawing.Point(6, 98);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(773, 183);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Active connections";
            // 
            // dataGridView_ServerActiveConnections
            // 
            this.dataGridView_ServerActiveConnections.AllowUserToAddRows = false;
            this.dataGridView_ServerActiveConnections.AllowUserToDeleteRows = false;
            this.dataGridView_ServerActiveConnections.AllowUserToResizeRows = false;
            this.dataGridView_ServerActiveConnections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_ServerActiveConnections.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_ServerActiveConnections.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_ServerActiveConnections.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView_ServerActiveConnections.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_ServerActiveConnections.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnConnectionId,
            this.Column_date,
            this.Column_macServ,
            this.dataGridViewTextBoxColumn2,
            this.Column_dstaddr});
            this.dataGridView_ServerActiveConnections.EnableHeadersVisualStyles = false;
            this.dataGridView_ServerActiveConnections.Location = new System.Drawing.Point(6, 20);
            this.dataGridView_ServerActiveConnections.Name = "dataGridView_ServerActiveConnections";
            this.dataGridView_ServerActiveConnections.ReadOnly = true;
            this.dataGridView_ServerActiveConnections.RowHeadersVisible = false;
            this.dataGridView_ServerActiveConnections.Size = new System.Drawing.Size(761, 157);
            this.dataGridView_ServerActiveConnections.TabIndex = 2;
            this.dataGridView_ServerActiveConnections.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridView_connections_CellFormatting);
            // 
            // ColumnConnectionId
            // 
            this.ColumnConnectionId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ColumnConnectionId.DataPropertyName = "ConnectionId";
            this.ColumnConnectionId.HeaderText = "Con Id";
            this.ColumnConnectionId.Name = "ColumnConnectionId";
            this.ColumnConnectionId.ReadOnly = true;
            this.ColumnConnectionId.Width = 64;
            // 
            // Column_date
            // 
            this.Column_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column_date.DataPropertyName = "ConnectionDate";
            this.Column_date.HeaderText = "Date";
            this.Column_date.Name = "Column_date";
            this.Column_date.ReadOnly = true;
            this.Column_date.Width = 55;
            // 
            // Column_macServ
            // 
            this.Column_macServ.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column_macServ.DataPropertyName = "MacAddress";
            this.Column_macServ.HeaderText = "MAC";
            this.Column_macServ.Name = "Column_macServ";
            this.Column_macServ.ReadOnly = true;
            this.Column_macServ.Width = 54;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "RemoteIPAddress";
            this.dataGridViewTextBoxColumn2.HeaderText = "Remote IP";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 82;
            // 
            // Column_dstaddr
            // 
            this.Column_dstaddr.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column_dstaddr.DataPropertyName = "DstEndPoint";
            this.Column_dstaddr.HeaderText = "Dst address";
            this.Column_dstaddr.Name = "Column_dstaddr";
            this.Column_dstaddr.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.linkLabel_save);
            this.groupBox1.Location = new System.Drawing.Point(6, 287);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(773, 205);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tunnels";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_id,
            this.enabled,
            this.Column_port_a,
            this.Column_ip,
            this.Column_port_b,
            this.Column_client_mac,
            this.Column_description});
            this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.NullValue = null;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(6, 33);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(6);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridView1.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataGridView1.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.Size = new System.Drawing.Size(762, 166);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellLeave);
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridView1_CellFormatting);
            this.dataGridView1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellLeave);
            this.dataGridView1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.DataGridView1_CellValidating);
            this.dataGridView1.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.DataGridView1_DefaultValuesNeeded);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.DataGridView1_EditingControlShowing);
            // 
            // Column_id
            // 
            this.Column_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column_id.DataPropertyName = "id";
            this.Column_id.HeaderText = "ID";
            this.Column_id.Name = "Column_id";
            this.Column_id.ReadOnly = true;
            this.Column_id.Visible = false;
            // 
            // enabled
            // 
            this.enabled.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.enabled.DataPropertyName = "enabled";
            this.enabled.HeaderText = "Enabled";
            this.enabled.Name = "enabled";
            this.enabled.Width = 51;
            // 
            // Column_port_a
            // 
            this.Column_port_a.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column_port_a.DataPropertyName = "src_port";
            this.Column_port_a.HeaderText = "Src port";
            this.Column_port_a.MaxInputLength = 5;
            this.Column_port_a.Name = "Column_port_a";
            this.Column_port_a.Width = 70;
            // 
            // Column_ip
            // 
            this.Column_ip.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column_ip.DataPropertyName = "dst_host";
            this.Column_ip.HeaderText = "Dst host";
            this.Column_ip.MaxInputLength = 16;
            this.Column_ip.Name = "Column_ip";
            this.Column_ip.Width = 72;
            // 
            // Column_port_b
            // 
            this.Column_port_b.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column_port_b.DataPropertyName = "dst_port";
            this.Column_port_b.HeaderText = "Dst port";
            this.Column_port_b.MaxInputLength = 5;
            this.Column_port_b.Name = "Column_port_b";
            this.Column_port_b.Width = 71;
            // 
            // Column_client_mac
            // 
            this.Column_client_mac.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column_client_mac.DataPropertyName = "mac";
            dataGridViewCellStyle8.Format = ">AA:AA:AA:AA:AA:AA";
            dataGridViewCellStyle8.NullValue = null;
            this.Column_client_mac.DefaultCellStyle = dataGridViewCellStyle8;
            this.Column_client_mac.HeaderText = "Client MAC";
            this.Column_client_mac.MaxInputLength = 17;
            this.Column_client_mac.Name = "Column_client_mac";
            this.Column_client_mac.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column_client_mac.ToolTipText = "Client identifier";
            this.Column_client_mac.Width = 120;
            // 
            // Column_description
            // 
            this.Column_description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column_description.DataPropertyName = "description";
            this.Column_description.HeaderText = "Description";
            this.Column_description.MaxInputLength = 100;
            this.Column_description.MinimumWidth = 70;
            this.Column_description.Name = "Column_description";
            // 
            // linkLabel_save
            // 
            this.linkLabel_save.AutoSize = true;
            this.linkLabel_save.Location = new System.Drawing.Point(7, 17);
            this.linkLabel_save.Name = "linkLabel_save";
            this.linkLabel_save.Size = new System.Drawing.Size(30, 13);
            this.linkLabel_save.TabIndex = 4;
            this.linkLabel_save.TabStop = true;
            this.linkLabel_save.Text = "save";
            this.linkLabel_save.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_save_LinkClicked);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericUpDown_serverPort);
            this.groupBox2.Controls.Add(this.button_launch);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(7);
            this.groupBox2.Size = new System.Drawing.Size(148, 57);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Server port";
            // 
            // numericUpDown_serverPort
            // 
            this.numericUpDown_serverPort.Location = new System.Drawing.Point(10, 22);
            this.numericUpDown_serverPort.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.numericUpDown_serverPort.Name = "numericUpDown_serverPort";
            this.numericUpDown_serverPort.Size = new System.Drawing.Size(57, 21);
            this.numericUpDown_serverPort.TabIndex = 0;
            this.numericUpDown_serverPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown_serverPort.Value = new decimal(new int[] {
            4424,
            0,
            0,
            0});
            // 
            // button_launch
            // 
            this.button_launch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_launch.Location = new System.Drawing.Point(73, 20);
            this.button_launch.Name = "button_launch";
            this.button_launch.Size = new System.Drawing.Size(65, 23);
            this.button_launch.TabIndex = 1;
            this.button_launch.Text = "Launch";
            this.button_launch.UseVisualStyleBackColor = true;
            this.button_launch.Click += new System.EventHandler(this.Button_server_Launch_Click);
            // 
            // tabPage_client
            // 
            this.tabPage_client.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_client.Controls.Add(this.groupBox8);
            this.tabPage_client.Controls.Add(this.groupBox7);
            this.tabPage_client.Controls.Add(this.groupBox6);
            this.tabPage_client.Controls.Add(this.groupBox5);
            this.tabPage_client.Controls.Add(this.serviceControl_client);
            this.tabPage_client.Location = new System.Drawing.Point(4, 22);
            this.tabPage_client.Name = "tabPage_client";
            this.tabPage_client.Size = new System.Drawing.Size(784, 500);
            this.tabPage_client.TabIndex = 1;
            this.tabPage_client.Text = "Client";
            // 
            // groupBox8
            // 
            this.groupBox8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox8.Controls.Add(this.label2);
            this.groupBox8.Controls.Add(this.numeric_maxCon);
            this.groupBox8.Controls.Add(this.dataGridView_ClientModeConnections);
            this.groupBox8.Location = new System.Drawing.Point(6, 237);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(770, 255);
            this.groupBox8.TabIndex = 21;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Ready to use connections";
            // 
            // dataGridView_ClientModeConnections
            // 
            this.dataGridView_ClientModeConnections.AllowUserToAddRows = false;
            this.dataGridView_ClientModeConnections.AllowUserToDeleteRows = false;
            this.dataGridView_ClientModeConnections.AllowUserToResizeColumns = false;
            this.dataGridView_ClientModeConnections.AllowUserToResizeRows = false;
            this.dataGridView_ClientModeConnections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_ClientModeConnections.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_ClientModeConnections.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnNum,
            this.dataGridViewTextBoxColumn1,
            this.ColumnStatus,
            this.dataGridViewTextBoxColumn3});
            this.dataGridView_ClientModeConnections.EnableHeadersVisualStyles = false;
            this.dataGridView_ClientModeConnections.Location = new System.Drawing.Point(6, 47);
            this.dataGridView_ClientModeConnections.MultiSelect = false;
            this.dataGridView_ClientModeConnections.Name = "dataGridView_ClientModeConnections";
            this.dataGridView_ClientModeConnections.ReadOnly = true;
            this.dataGridView_ClientModeConnections.RowHeadersVisible = false;
            this.dataGridView_ClientModeConnections.Size = new System.Drawing.Size(758, 202);
            this.dataGridView_ClientModeConnections.TabIndex = 0;
            // 
            // ColumnNum
            // 
            this.ColumnNum.DataPropertyName = "Number";
            this.ColumnNum.Frozen = true;
            this.ColumnNum.HeaderText = "Con Id";
            this.ColumnNum.Name = "ColumnNum";
            this.ColumnNum.ReadOnly = true;
            this.ColumnNum.Width = 64;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ConnectedDate";
            this.dataGridViewTextBoxColumn1.HeaderText = "Date";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.ToolTipText = "Дата установки соединения";
            this.dataGridViewTextBoxColumn1.Width = 55;
            // 
            // ColumnStatus
            // 
            this.ColumnStatus.DataPropertyName = "Status";
            this.ColumnStatus.HeaderText = "Status";
            this.ColumnStatus.Name = "ColumnStatus";
            this.ColumnStatus.ReadOnly = true;
            this.ColumnStatus.Width = 80;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "EndPoint";
            this.dataGridViewTextBoxColumn3.HeaderText = "Server address";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.maskedTextBox_mac);
            this.groupBox7.Controls.Add(this.label1);
            this.groupBox7.Location = new System.Drawing.Point(6, 71);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(264, 53);
            this.groupBox7.TabIndex = 22;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Identifier";
            // 
            // maskedTextBox_mac
            // 
            this.maskedTextBox_mac.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.maskedTextBox_mac.Location = new System.Drawing.Point(119, 20);
            this.maskedTextBox_mac.Mask = ">AA:AA:AA:AA:AA:AA";
            this.maskedTextBox_mac.Name = "maskedTextBox_mac";
            this.maskedTextBox_mac.ReadOnly = true;
            this.maskedTextBox_mac.Size = new System.Drawing.Size(131, 21);
            this.maskedTextBox_mac.TabIndex = 16;
            this.maskedTextBox_mac.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Gateway MAC";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.dataGridView_clientMode_OnlineTunnels);
            this.groupBox6.Location = new System.Drawing.Point(278, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(498, 223);
            this.groupBox6.TabIndex = 20;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Active tunnels";
            // 
            // dataGridView_clientMode_OnlineTunnels
            // 
            this.dataGridView_clientMode_OnlineTunnels.AllowUserToAddRows = false;
            this.dataGridView_clientMode_OnlineTunnels.AllowUserToDeleteRows = false;
            this.dataGridView_clientMode_OnlineTunnels.AllowUserToResizeColumns = false;
            this.dataGridView_clientMode_OnlineTunnels.AllowUserToResizeRows = false;
            this.dataGridView_clientMode_OnlineTunnels.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_clientMode_OnlineTunnels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_clientMode_OnlineTunnels.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.Column_startDate,
            this.Column_endPoint});
            this.dataGridView_clientMode_OnlineTunnels.EnableHeadersVisualStyles = false;
            this.dataGridView_clientMode_OnlineTunnels.Location = new System.Drawing.Point(6, 20);
            this.dataGridView_clientMode_OnlineTunnels.MultiSelect = false;
            this.dataGridView_clientMode_OnlineTunnels.Name = "dataGridView_clientMode_OnlineTunnels";
            this.dataGridView_clientMode_OnlineTunnels.ReadOnly = true;
            this.dataGridView_clientMode_OnlineTunnels.RowHeadersVisible = false;
            this.dataGridView_clientMode_OnlineTunnels.Size = new System.Drawing.Size(486, 197);
            this.dataGridView_clientMode_OnlineTunnels.TabIndex = 0;
            this.dataGridView_clientMode_OnlineTunnels.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridView_client_CellFormatting);
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column4.DataPropertyName = "ConnectionId";
            this.Column4.Frozen = true;
            this.Column4.HeaderText = "Con Id";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 65;
            // 
            // Column_startDate
            // 
            this.Column_startDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column_startDate.DataPropertyName = "StartDate";
            this.Column_startDate.Frozen = true;
            this.Column_startDate.HeaderText = "Date";
            this.Column_startDate.Name = "Column_startDate";
            this.Column_startDate.ReadOnly = true;
            this.Column_startDate.Width = 55;
            // 
            // Column_endPoint
            // 
            this.Column_endPoint.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column_endPoint.DataPropertyName = "EndPoint";
            this.Column_endPoint.HeaderText = "Requested address";
            this.Column_endPoint.Name = "Column_endPoint";
            this.Column_endPoint.ReadOnly = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBox_host);
            this.groupBox5.Controls.Add(this.numericUpDown_client);
            this.groupBox5.Controls.Add(this.button2);
            this.groupBox5.Location = new System.Drawing.Point(6, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(7);
            this.groupBox5.Size = new System.Drawing.Size(264, 57);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Server address";
            // 
            // textBox_host
            // 
            this.textBox_host.Location = new System.Drawing.Point(10, 22);
            this.textBox_host.Name = "textBox_host";
            this.textBox_host.Size = new System.Drawing.Size(106, 21);
            this.textBox_host.TabIndex = 19;
            // 
            // numericUpDown_client
            // 
            this.numericUpDown_client.Location = new System.Drawing.Point(122, 22);
            this.numericUpDown_client.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.numericUpDown_client.Name = "numericUpDown_client";
            this.numericUpDown_client.Size = new System.Drawing.Size(57, 21);
            this.numericUpDown_client.TabIndex = 0;
            this.numericUpDown_client.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown_client.Value = new decimal(new int[] {
            4424,
            0,
            0,
            0});
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(185, 20);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(65, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "Launch";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button_client_launch_Click);
            // 
            // serviceControl_client
            // 
            this.serviceControl_client.Args = null;
            this.serviceControl_client.AutoScroll = true;
            this.serviceControl_client.AutoSize = true;
            this.serviceControl_client.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.serviceControl_client.HeaderName = "Client";
            this.serviceControl_client.Location = new System.Drawing.Point(6, 133);
            this.serviceControl_client.Margin = new System.Windows.Forms.Padding(5);
            this.serviceControl_client.Name = "serviceControl_client";
            this.serviceControl_client.ServiceName = "";
            this.serviceControl_client.ServiceSatatus = "";
            this.serviceControl_client.Size = new System.Drawing.Size(264, 95);
            this.serviceControl_client.TabIndex = 23;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.Timer_services_Tick);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // numeric_maxCon
            // 
            this.numeric_maxCon.Location = new System.Drawing.Point(99, 20);
            this.numeric_maxCon.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numeric_maxCon.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numeric_maxCon.Name = "numeric_maxCon";
            this.numeric_maxCon.Size = new System.Drawing.Size(48, 21);
            this.numeric_maxCon.TabIndex = 20;
            this.numeric_maxCon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numeric_maxCon.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numeric_maxCon.ValueChanged += new System.EventHandler(this.NumericUpDown1_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Max connections";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 526);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PortTunneling";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RoleChoose_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage_server.ResumeLayout(false);
            this.tabPage_server.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ServerActiveConnections)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_serverPort)).EndInit();
            this.tabPage_client.ResumeLayout(false);
            this.tabPage_client.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ClientModeConnections)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_clientMode_OnlineTunnels)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_client)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_maxCon)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage_server;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.LinkLabel linkLabel_save;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.NumericUpDown numericUpDown_serverPort;
		private System.Windows.Forms.Button button_launch;
		private System.Windows.Forms.TabPage tabPage_client;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.NumericUpDown numericUpDown_client;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.MaskedTextBox maskedTextBox_mac;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.TextBox textBox_host;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.DataGridView dataGridView_ServerActiveConnections;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.DataGridView dataGridView_clientMode_OnlineTunnels;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column_MAC;
		private System.Windows.Forms.GroupBox groupBox7;
		private ServiceControl serviceControl_server;
		private ServiceControl serviceControl_client;
		private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.DataGridView dataGridView_ClientModeConnections;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnConnectionId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_macServ;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_dstaddr;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_startDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_endPoint;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_id;
        private System.Windows.Forms.DataGridViewCheckBoxColumn enabled;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_port_a;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_ip;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_port_b;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_client_mac;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_description;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numeric_maxCon;
    }
}