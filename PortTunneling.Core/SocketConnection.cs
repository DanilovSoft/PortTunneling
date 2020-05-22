using Newtonsoft.Json;
using PortTunneling.Models;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PortTunneling
{
    [DebuggerDisplay(@"\{ConnectionId = {ConnectionId}, Dst Address = {DebugDisplay,nq}\}")]
    public sealed class SocketConnection
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebugDisplay => $"{(DstEndPoint == null ? "null" : DstEndPoint.Host + ":" + DstEndPoint.Port)}";
        private readonly bool _clientMode;
        private readonly byte[] _buffer = new byte[8192];
        private readonly TcpSocket _socket;
        private int _closedFlag;
        private TcpSocket _srcSocket;
        public object Tag { get; set; }
        public IPAddress RemoteIPAddress { get; }
        public DateTime ConnectionDate { get; }
        public int? ConnectionId { get; private set; }

        private PhysicalAddress _MacAddress;
        /// <summary>
        /// Мак адрес клиента.
        /// </summary>
        public PhysicalAddress MacAddress
        {
            get => _MacAddress;
            private set
            {
                if(_MacAddress != value)
                {
                    _MacAddress = value;
                    MacAddressChanged?.Invoke(this, value);
                }
            }
        }
        public DnsEndPoint DstEndPoint { get; private set; }
        public event EventHandler<TcpSocket> StartingTunneling;
        public event EventHandler<PhysicalAddress> MacAddressChanged;
        public event EventHandler<DnsEndPoint> DstEndPointChanged;
        public event EventHandler Initialized;
        public event EventHandler<int> ConnectionIdChanged;

        // ctor Server
        public SocketConnection(TcpSocket socket)
        {
            _socket = socket ?? throw new ArgumentNullException(nameof(socket));
            RemoteIPAddress = ((IPEndPoint)socket.RemoteEndPoint).Address;
            ConnectionDate = DateTime.Now;
            _clientMode = false;
        }

        // ctor
        public SocketConnection(TcpSocket socket, PhysicalAddress mac, int conectionId)
        {
            _socket = socket ?? throw new ArgumentNullException(nameof(socket));
            RemoteIPAddress = ((IPEndPoint)socket.RemoteEndPoint).Address;
            ConnectionDate = DateTime.Now;
            _clientMode = true;
            MacAddress = mac;
            ConnectionId = conectionId;
        }

        public async void StartClientLoopReadMessages()
        {
            _socket.SetKeepAlive(TimeSpan.FromSeconds(30.0), TimeSpan.FromSeconds(1.0));
            try
            {
                await InitConnectionAsync().ConfigureAwait(false);

                while (true)
                {
                    Msg msg = await ReadMessageAsync().ConfigureAwait(false);
                    if (msg.Command == Command.EndPoint)
                    {
                        await EndPointFromServerAsync(new DnsEndPoint((string)msg.Args[0], (int)(long)msg.Args[1])).ConfigureAwait(false);
                        return;
                    }
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Обрыв соединения на клиенте.");
                AtomicClose();
            }
        }

        public async Task<TcpTunnel> StartServerLoopReadMessagesAsync()
        {
            _socket.SetKeepAlive(TimeSpan.FromSeconds(30.0), TimeSpan.FromSeconds(1.0));
            try
            {
                while (true)
                {
                    Msg msg = await ReadMessageAsync().ConfigureAwait(false);
                    if (msg.Command == Command.Init)
                    {
                        ConnectionId = int.Parse((string)msg.Args[0]);
                        ConnectionIdChanged?.Invoke(this, ConnectionId.Value);

                        ServerGotClientMac(PhysicalAddress.Parse((string)msg.Args[1]));

                        // Соединение готово для использования в качестве туннеля.
                        Initialized?.Invoke(this, EventArgs.Empty);
                    }
                    else if (msg.Command == Command.StartTunnel)
                    {
                        break;
                    }
                }

                // Начать использовать сокет для тунелирования.
                DstEndPointChanged?.Invoke(this, DstEndPoint);
                var serverSideTunnel = new TcpTunnel(this, _srcSocket, _socket, DstEndPoint);
                serverSideTunnel.StartExchange();
                return serverSideTunnel;
            }
            catch (Exception)
            {
                Debug.WriteLine("Обрыв соединения на сервере.");
                AtomicClose();
                return null;
            }
        }

        public Task InitConnectionAsync()
        {
            return SendMessageAsync(Command.Init, ConnectionId.ToString(), MacAddress.ToString());
        }

        public async void SendEndPoint(TcpSocket srcSocket, DnsEndPoint dstEndPoint)
        {
            // Фиксируем адрес который будем использовать.
            DstEndPoint = dstEndPoint;
            _srcSocket = srcSocket;
            try
            {
                await SendMessageAsync(Command.EndPoint, dstEndPoint.Host, dstEndPoint.Port).ConfigureAwait(false);
            }
            catch (Exception)
            {
                AtomicClose();
                srcSocket.Dispose();
            }
        }

        /// <summary>
        /// Когда соединение полностью готово для туннелирования.
        /// </summary>
        private void ServerGotClientMac(PhysicalAddress mac)
        {
            GlobalVars.ClientConnections.Add(mac, this);
            MacAddress = mac;
        }

        private async Task EndPointFromServerAsync(DnsEndPoint endPoint)
        {
            StartingTunneling?.Invoke(this, _socket);
            var dstSocket = new TcpSocket();
            await SendMessageAsync(Command.StartTunnel).ConfigureAwait(false);
            try
            {
                dstSocket.Connect(endPoint);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                dstSocket.Dispose();
                AtomicClose();
                return;
            }
            var clientSideTunnel = new TcpTunnel(this, dstSocket, _socket, endPoint);
            clientSideTunnel.StartExchange();
        }

        private async Task SendMessageAsync(Command method, params object[] args)
        {
            string json = JsonConvert.SerializeObject(new Msg
            {
                Command = method,
                Args = args
            });
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            int num = bytes.Length;
            byte[] array = (num >= 254) ? ((num >= 65535) ? new byte[5]
            {
                255,
                (byte)num,
                (byte)(num >> 8),
                (byte)(num >> 16),
                (byte)(num >> 24)
            } : new byte[3]
            {
                254,
                (byte)num,
                (byte)(num >> 8)
            }) : new byte[1]
            {
                (byte)num
            };
            byte[] array2 = new byte[array.Length + bytes.Length];
            array.CopyTo(array2, 0);
            bytes.CopyTo(array2, array.Length);
            await _socket.SendTaskAsync(array2, 0, array2.Length).ConfigureAwait(false);
        }

        private async Task<Msg> ReadMessageAsync()
        {
            await _socket.ReceiveAsyncN(_buffer, 0, 1).ConfigureAwait(false);
            byte b = _buffer[0];
            int size;
            if (b < 254)
            {
                size = b;
            }
            else if (b == 254)
            {
                await _socket.ReceiveAsyncN(_buffer, 0, 2).ConfigureAwait(false);
                size = BitConverter.ToUInt16(_buffer, 0);
            }
            else
            {
                await _socket.ReceiveAsyncN(_buffer, 0, 4).ConfigureAwait(false);
                size = BitConverter.ToInt32(_buffer, 0);
            }
            byte[] buf = _buffer;
            if (size > _buffer.Length)
            {
                buf = new byte[size];
            }
            await _socket.ReceiveAsyncN(buf, 0, size).ConfigureAwait(false);
            string json = Encoding.UTF8.GetString(buf, 0, size);
            return JsonConvert.DeserializeObject<Msg>(json);
        }

        private void AtomicClose()
        {
            if (Interlocked.CompareExchange(ref _closedFlag, 1, 0) == 0)
            {
                _socket.Dispose();
                
                if (!_clientMode)
                {
                    GlobalVars.ClientConnections.Remove(MacAddress, this);
                }
            }
        }
    }
}
