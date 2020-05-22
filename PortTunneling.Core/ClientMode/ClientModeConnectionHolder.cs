using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace PortTunneling
{
    [DebuggerDisplay(@"\{{ConnectTo.Host,nq}:{ConnectTo.Port}\}")]
	public sealed class ClientModeConnectionHolder
	{
        private const string MutexName = "Global\\PortTunnelingClient";
		private static Mutex _mutex;
        private readonly List<Connection> Connections = new List<Connection>();
        public DnsEndPoint ConnectTo { get; }
        private readonly PhysicalAddress ThisMachineMac;
        private int _maxConnections = 2;
        private int ConectionIdSeq;
		public event EventHandler<Connection> TunnelCreated;
		public event EventHandler<Connection> ConnectionDestroyed;
		public event EventHandler<Connection> NewConnection;

        private static void CreateMutex()
        {
            if (_mutex == null)
            {
                var mutex = new Mutex(initiallyOwned: true, name: MutexName, out bool createdNew);
                if (!createdNew)
                {
                    mutex.Dispose();
                    mutex = null;
                    throw new InvalidOperationException("Another copy already running in client mode.");
                }
                else
                    _mutex = mutex;
            }
        }

        // ctor.
        public ClientModeConnectionHolder(DnsEndPoint connectTo, PhysicalAddress thisMachineMac)
		{
            if (thisMachineMac == null)
            {
                thisMachineMac = NetworkInterface.GetAllNetworkInterfaces()
                    .First(x => x.OperationalStatus == OperationalStatus.Up && x.GetIPProperties().GatewayAddresses.Any())
                    .GetPhysicalAddress();
            }

            ThisMachineMac = thisMachineMac;
            ConnectTo = connectTo;
        }

        public void StartConnections(int maxConnections)
        {
            CreateMutex();

            _maxConnections = maxConnections;

            var connection = new Connection(this);
            Connections.Add(connection);
            OnNewConnection(connection);
            connection.CreateNewConnection();
        }

        private void OnTunnelCreated(Connection connection)
        {
            TunnelCreated?.Invoke(this, connection);
        }

        private void OnConnectionDestroyed(Connection connection)
        {
            ConnectionDestroyed?.Invoke(this, connection);
        }

        private void OnNewConnection(Connection connection)
        {
            NewConnection?.Invoke(this, connection);
        }

        public void Abort()
        {
            lock (Connections)
            {
                foreach (var connection in Connections.ToArray())
                {
                    Connections.Remove(connection);
                    OnConnectionDestroyed(connection);
                    connection.Abort();
                }
            }
        }

        public void SetMaxConnections(int con)
        {
            lock (Connections)
            {
                _maxConnections = con;

                if (Connections.Count < _maxConnections && Connections.All(x => x.Status == ConnectionStatus.Connected))
                {
                    var newCon = new Connection(this);
                    Connections.Add(newCon);
                    OnNewConnection(newCon);
                    newCon.CreateNewConnection();
                }
                else
                // Удалить лишние но только которые не подключены.
                {
                    // Сколько нужно удалить.
                    int toDeleteCount = Connections.Count - _maxConnections;
                    for (int i = 0; i < toDeleteCount; i++)
                    {
                        var toDelete = Connections.Where(x => x.Status == ConnectionStatus.Disconnected).OrderBy(x => x.ConnectionId).Take(toDeleteCount);
                        foreach (var conn in toDelete)
                        {
                            Connections.Remove(conn);
                            OnConnectionDestroyed(conn);
                            conn.Abort();
                        }
                    }
                }
            }
        }

        [DebuggerDisplay(@"\{ConnectionId = {ConnectionId}, Status = {Status}\}")]
        public sealed class Connection
        {
            /// <summary>
            /// Доступ только через блокировку <see cref="Connections"/>.
            /// </summary>
            public ConnectionStatus Status { get; private set; } = ConnectionStatus.Disconnected;
            public ClientModeConnectionHolder Parent { get; }
            public int ConnectionId { get; }
            public IPEndPoint RemoteEndPoint { get; private set; }
            public object Tag { get; set; }
            public event EventHandler<ConnectionStatus> StatusChanged;
            private TcpSocket _socket;

            // ctor.
            public Connection(ClientModeConnectionHolder parent)
            {
                Parent = parent;
                ConnectionId = Parent.ConectionIdSeq++;
            }

            public void CreateNewConnection()
            {
                var args = new SocketAsyncEventArgs();
                args.Completed += Completed;

                // Куда подключаться.
                args.RemoteEndPoint = Parent.ConnectTo;

                var socket = new TcpSocket();
                _socket = socket;
                StartConnect(socket, args);
            }

            private void StartConnect(TcpSocket socket, SocketAsyncEventArgs e)
            {
                lock (Parent.Connections)
                {
                    Status = ConnectionStatus.Connecting;
                    StatusChanged?.Invoke(this, Status);
                    if (socket.IsDisposed)
                    {
                        e.Dispose();
                        return;
                    }
                }

                if (!socket.ConnectAsync(e))
                    Completed(socket, e);
            }

            private async void Completed(object sender, SocketAsyncEventArgs e)
            {
                var socket = (TcpSocket)sender;

                if (e.SocketError == SocketError.Success)
                {
                    e.Dispose();

                    RemoteEndPoint = (IPEndPoint)socket.RemoteEndPoint;

                    lock (Parent.Connections)
                    {
                        Status = ConnectionStatus.Connected;
                        StatusChanged?.Invoke(this, Status);

                        if (Parent.Connections.Count < Parent._maxConnections)
                        {
                            var newCon = new Connection(Parent);
                            Parent.Connections.Add(newCon);
                            Parent.OnNewConnection(newCon);
                            newCon.CreateNewConnection();
                        }
                    }

                    socket.Disposed += Socket_Disposed;

                    var connection = new SocketConnection(socket, Parent.ThisMachineMac, ConnectionId);
                    connection.StartingTunneling += SocketConnection_StartingTunneling;
                    connection.StartClientLoopReadMessages();   
                }
                else if (e.SocketError == SocketError.OperationAborted)
                {
                    e.Dispose();
                }
                else
                {
                    Debug.WriteLine($"ConnectionId: {ConnectionId}, ClientModeConnectionHolder SocketError");

                    lock (Parent.Connections)
                    {
                        Status = ConnectionStatus.Disconnected;
                        StatusChanged?.Invoke(this, Status);
                    }

                    await Task.Delay(new Random().Next(5000, 7000)).ConfigureAwait(false);
                    StartConnect(socket, e);
                }
            }

            private void SocketConnection_StartingTunneling(object sender, TcpSocket socket)
            {
                // Сокетом теперь полностью владеет SocketConnection.
                socket.Disposed -= Socket_Disposed;

                Parent.OnTunnelCreated(this);

                lock (Parent.Connections)
                {
                    Parent.Connections.Remove(this);

                    if (Parent.Connections.Count < Parent._maxConnections)
                    {
                        var newCon = new Connection(Parent);
                        Parent.Connections.Add(newCon);
                        Parent.OnNewConnection(newCon);
                        newCon.CreateNewConnection();
                    }
                }
            }

            private async void Socket_Disposed(object sender, EventArgs e)
            {
                Debug.WriteLine($"ConnectionId: {ConnectionId}, ClientModeConnectionHolder.Socket_Disposed");

                lock (Parent.Connections)
                {
                    Status = ConnectionStatus.Disconnected;
                    StatusChanged?.Invoke(this, Status);

                    if (Parent.Connections.Count > Parent._maxConnections)
                    {
                        Parent.Connections.Remove(this);
                        Parent.OnConnectionDestroyed(this);
                        return;
                    }
                }

                await Task.Delay(new Random().Next(5000, 7000)).ConfigureAwait(false);

                bool reconnect;
                lock (Parent.Connections)
                {
                    if (!Parent.Connections.Contains(this))
                    {
                        reconnect = false;
                    }
                    else
                    {
                        if (Parent.Connections.Count > 1 && Parent.Connections.All(x => x.Status == ConnectionStatus.Disconnected))
                        {
                            var toDelete = Parent.Connections.OrderByDescending(x => x.ConnectionId).Skip(1).ToList();
                            while (toDelete.Count > 0)
                            {
                                var con = toDelete[0];
                                toDelete.RemoveAt(0);
                                Parent.Connections.Remove(con);
                                Parent.OnConnectionDestroyed(con);
                            }
                            reconnect = Parent.Connections[0] == this;
                        }
                        else if (Parent.Connections.Count == 1)
                        {
                            reconnect = true;
                        }
                        else
                        {
                            if (Parent.Connections.Except(new[] { this }).All(x => x.Status == ConnectionStatus.Connected))
                            // Если все остальные подключены.
                            {
                                reconnect = true;
                            }
                            else
                            {
                                reconnect = false;
                                Parent.Connections.Remove(this);
                                Parent.OnConnectionDestroyed(this);
                            }
                        }
                    }

                    if (reconnect)
                    {
                        Status = ConnectionStatus.Connecting;
                        StatusChanged?.Invoke(this, Status);
                    }
                }

                if (reconnect)
                    CreateNewConnection();
            }

            internal void Abort()
            {
                _socket?.Dispose();
            }
        }
    }
}
