using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PortTunneling
{
    public sealed class SocketConnectionModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly ISynchronizeInvoke _synchronizingObject;
        public SocketConnection Connection { get; }

        public IPAddress RemoteIPAddress { get; }
        public DateTime ConnectionDate { get; }

        private PhysicalAddress _macAddress;
        public PhysicalAddress MacAddress
        {
            get => _macAddress;
            private set
            {
                if (_macAddress != value)
                {
                    _macAddress = value;
                    OnPropertyChanged();
                }
            }
        }

        private DnsEndPoint _dstEndPoint;
        public DnsEndPoint DstEndPoint
        {
            get => _dstEndPoint;
            private set
            {
                if (_dstEndPoint != value)
                {
                    _dstEndPoint = value;
                    OnPropertyChanged();
                }
            }
        }

        private int? _ConnectionId;
        public int? ConnectionId
        {
            get => _ConnectionId;
            private set
            {
                if(_ConnectionId != value)
                {
                    _ConnectionId = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _Initialized;
        public bool Initialized
        {
            get => _Initialized;
            private set
            {
                if(_Initialized != value)
                {
                    _Initialized = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(DstEndPoint));
                }
            }
        }

        public SocketConnectionModel(SocketConnection connection, ISynchronizeInvoke synchronizingObject)
        {
            Connection = connection;
            _synchronizingObject = synchronizingObject;
            connection.MacAddressChanged += MacAddressChanged;
            connection.DstEndPointChanged += DstEndPointChanged;
            connection.ConnectionIdChanged += Connection_ConnectionIdChanged;
            connection.Initialized += Connection_Initialized;

            MacAddress = connection.MacAddress;
            DstEndPoint = connection.DstEndPoint;
            RemoteIPAddress = connection.RemoteIPAddress;
            ConnectionDate = connection.ConnectionDate;
            //ConnectionId = connection.ConnectionId;
        }

        private void Connection_Initialized(object sender, EventArgs e)
        {
            if (_synchronizingObject.InvokeRequired)
                _synchronizingObject.BeginInvoke((Action)OnInitialized, null);
            else
                OnInitialized();
        }

        private void OnInitialized()
        {
            Initialized = true;
        }

        private void Connection_ConnectionIdChanged(object sender, int connectionId)
        {
            if (_synchronizingObject.InvokeRequired)
                _synchronizingObject.BeginInvoke((Action<int>)Connection_OnConnectionIdChanged, new object[] { connectionId });
            else
                Connection_OnConnectionIdChanged(connectionId);
        }

        private void Connection_OnConnectionIdChanged(int connectionId)
        {
            ConnectionId = connectionId;
        }

        private void DstEndPointChanged(object sender, DnsEndPoint e)
        {
            if (_synchronizingObject.InvokeRequired)
                _synchronizingObject.BeginInvoke((Action<DnsEndPoint>)OnDstEndPointChanged, new object[] { e });
            else
                OnDstEndPointChanged(e);
        }

        private void OnDstEndPointChanged(DnsEndPoint e)
        {
            DstEndPoint = e;
        }

        private void MacAddressChanged(object sender, PhysicalAddress e)
        {
            if (_synchronizingObject.InvokeRequired)
                _synchronizingObject.BeginInvoke((Action<PhysicalAddress>)OnMacAddressChanged, new object[] { e });
            else
                OnMacAddressChanged(e);
        }

        private void OnMacAddressChanged(PhysicalAddress e)
        {
            MacAddress = e;
        }

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            var e = new PropertyChangedEventArgs(name);
            if (_synchronizingObject.InvokeRequired)
                _synchronizingObject.BeginInvoke((Action<object, PropertyChangedEventArgs>)OnPropertyChanged, new object[] { this, e });
            else
                OnPropertyChanged(this, e);
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(sender, e);
        }
    }
}
