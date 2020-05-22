using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PortTunneling
{
    public sealed class ClientModeReadyToUseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ClientModeConnectionHolder.Connection Connection { get; }
        private readonly ISynchronizeInvoke _sync;

        private DateTime? _ConnectedDate;
        public DateTime? ConnectedDate
        {
            get => _ConnectedDate;
            set
            {
                if(_ConnectedDate != value)
                {
                    _ConnectedDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private IPEndPoint _EndPoint;
        public IPEndPoint EndPoint
        {
            get => _EndPoint;
            set
            {
                if(_EndPoint != value)
                {
                    _EndPoint = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _Number;
        public int Number
        {
            get => _Number;
            private set
            {
                if(_Number != value)
                {
                    _Number = value;
                    OnPropertyChanged();
                }
            }
        }

        private ConnectionStatus _status;
        public ConnectionStatus Status
        {
            get => _status;
            set
            {
                if (value != _status)
                {
                    _status = value;
                    OnPropertyChanged();
                }
            }
        }

        public ClientModeReadyToUseModel(ClientModeConnectionHolder.Connection connection, ISynchronizeInvoke sync)
        {
            Connection = connection;
            _sync = sync;
            Number = connection.ConnectionId;
            Status = connection.Status;
            connection.StatusChanged += Connection_StatusChanged;
        }

        private void Connection_StatusChanged(object sender, ConnectionStatus e)
        {
            if (_sync.InvokeRequired)
                _sync.BeginInvoke((Action<ConnectionStatus>)OnStatusChanged, new object[] { e });
            else
                OnStatusChanged(e);
        }

        private void OnStatusChanged(ConnectionStatus newStatus)
        {
            Status = newStatus;
            switch (newStatus)
            {
                case ConnectionStatus.Disconnected:
                    ConnectedDate = null;
                    EndPoint = null;
                    break;
                case ConnectionStatus.Connecting:
                    break;
                case ConnectionStatus.Connected:
                    EndPoint = Connection.RemoteEndPoint;
                    ConnectedDate = DateTime.Now;
                    break;
            }
        }

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public enum ConnectionStatus
    {
        Disconnected,
        Connecting,
        Connected,
    }
}
