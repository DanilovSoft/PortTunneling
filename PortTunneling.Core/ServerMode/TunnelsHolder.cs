using PortTunneling.Models;
using PortTunneling.ServerMode.Dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PortTunneling.Server
{
	public sealed class TunnelsHolder
	{
        private readonly Dictionary<int, Listener> _dict = new Dictionary<int, Listener>();
        private bool _enabled;
        public event EventHandler<Listener> NewListener;
        public event EventHandler<Listener> ListenerClosed;

        public void EnableTunnels()
		{
            if (!_enabled)
            {
                _enabled = true;

                var listeners = GlobalVars.SQL.GetAll();
                foreach (var m in listeners.Where(x => x.Enabled))
                {
                    var listener = new Listener(this, m.ID, m.SrcPort, m.DstEndPoint, m.Mac, m.Description);
                    _dict.Add(m.SrcPort, listener);
                    listener.Listen();
                    NewListener?.Invoke(this, listener);
                }
            }
		}

        public void DisableTunnels()
        {
            if (_enabled)
            {
                foreach (var listener in _dict.Values)
                {
                    listener.Abort();
                }
                _dict.Clear();

                _enabled = false;
            }
        }

        public TunnelsHolder()
		{
			
		}

        public void UpdateMapping()
        {
            if (_enabled)
            {
                TunnelModel[] tunnels = GlobalVars.SQL.GetAll();
                var dict = tunnels.Where(x => x.Enabled).ToDictionary(x => x.SrcPort, x => x);

                // Удалить тунели которых больше нет в бд или выключены.
                int[] toDelete = _dict.Keys.Except(dict.Keys).ToArray();
                foreach (int srcPort in toDelete)
                {
                    var listener = _dict[srcPort];
                    _dict.Remove(srcPort);
                    listener.Abort();
                }

                // Обновить отличающиеся.
                var join = _dict.Join(dict, x => x.Key, y => y.Key, (x, y) => new
                {
                    Before = x.Value,
                    After = y.Value
                });

                foreach (var pair in join)
                {
                    if(!pair.Before.Mac.Equals(pair.After.Mac))
                    {
                        pair.Before.Mac = pair.After.Mac;
                    }

                    if (!pair.Before.DstEndPoint.Equals(pair.After.DstEndPoint))
                    {
                        pair.Before.DstEndPoint = pair.After.DstEndPoint;
                    }
                }

                // Добавить новые тунели.
                int[] toAdd = dict.Keys.Except(_dict.Keys).ToArray();
                foreach (int srcPort in toAdd)
                {
                    var m = dict[srcPort];
                    var listener = new Listener(this, m.ID, m.SrcPort, m.DstEndPoint, m.Mac, m.Description);
                    _dict.Add(m.SrcPort, listener);
                    listener.Listen();
                    NewListener?.Invoke(this, listener);
                }
            }
        }

        private void OnListenerClosed(Listener listener)
        {
            ListenerClosed?.Invoke(this, listener);
        }

        public sealed class Listener
        {
            private readonly Socket _socket;
            private readonly TunnelsHolder _parent;

            /// <summary>
            /// Запись в базе.
            /// </summary>
            public int ID { get; }
            /// <summary>
            /// Уникальный ключ.
            /// </summary>
            public int SrcPort { get; }

            private volatile DnsEndPoint _DstEndPoint;
            public DnsEndPoint DstEndPoint
            {
                get => _DstEndPoint;
                set
                {
                    if (_DstEndPoint != value)
                    {
                        _DstEndPoint = value;
                        DstEndPointChanged?.Invoke(this, value);
                    }
                }
            }

            private volatile PhysicalAddress _Mac;
            public PhysicalAddress Mac
            {
                get => _Mac;
                set
                {
                    if (_Mac != value)
                    {
                        _Mac = value;
                        MacChanged?.Invoke(this, value);
                    }
                }
            }
            public string Description { get; }
            public object Tag { get; set; }

            public event EventHandler<DnsEndPoint> DstEndPointChanged;
            public event EventHandler<PhysicalAddress> MacChanged;

            public Listener(TunnelsHolder holder, int id, int srcPort, DnsEndPoint dstAddress, PhysicalAddress mac, string description)
            {
                _parent = holder;
                ID = id;
                SrcPort = srcPort;
                DstEndPoint = dstAddress;
                Mac = mac;
                Description = description;
                _socket = new TcpSocket();
            }

            public void Listen()
            {
                _socket.Bind(new IPEndPoint(IPAddress.Any, SrcPort));
                _socket.Listen((int)SocketOptionName.MaxConnections);

                var args = new SocketAsyncEventArgs();
                args.Completed += ProcessAccept;
                StartAccept(args);
            }

            private void StartAccept(SocketAsyncEventArgs e)
            {
                e.AcceptSocket = new TcpSocket();
                if (!_socket.AcceptAsync(e))
                    ProcessAccept(null, e);
            }

            private void ProcessAccept(object sender, SocketAsyncEventArgs e)
            {
                if (e.SocketError == SocketError.Success)
                {
                    var socket = (TcpSocket)e.AcceptSocket;
                    StartAccept(e);

                    if (GlobalVars.ClientConnections.TryRemove(Mac, out SocketConnection connection))
                    {
                        // Отправить клиенту запрос подключиться к указанному адресу.
                        connection.SendEndPoint(socket, DstEndPoint);
                    }
                    else
                    {
                        CloseSocket(socket);
                    }
                }
                else
                {
                    e.Dispose();
                    _parent.OnListenerClosed(this);
                }
            }

            public void Abort()
            {
                _socket.Dispose();
            }

            private void CloseSocket(Socket socket)
            {
                try
                {
                    if (socket.Connected)
                        socket.Shutdown(SocketShutdown.Both);
                }
                catch { }

                socket.Dispose();
            }
        }
    }
}
