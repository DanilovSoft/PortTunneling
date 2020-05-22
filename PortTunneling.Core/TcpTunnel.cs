using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PortTunneling
{
    [DebuggerDisplay(@"\{{EndPoint.Host,nq}:{EndPoint.Port}\}")]
    public sealed class TcpTunnel
    {
        public static event EventHandler TunnelCreated;
        public static event EventHandler TunnelClosed;
        private readonly SocketChannel _channelSrcDst;
        private readonly SocketChannel _channelDstSrc;
        private int _shutdown;
        public DnsEndPoint EndPoint { get; }
        public DateTime StartDate { get; private set; }
        public SocketConnection Connection { get; }
        public int? ConnectionId => Connection.ConnectionId;

        public TcpTunnel(SocketConnection socketConnection, Socket socketFrom, Socket socketTo, DnsEndPoint endPoint)
        {
            EndPoint = endPoint;
            Connection = socketConnection;
            _channelSrcDst = new SocketChannel(socketFrom, socketTo, OnError_AtomicShutdown);
            _channelDstSrc = new SocketChannel(socketTo, socketFrom, OnError_AtomicShutdown);
        }

        public void StartExchange()
        {
            StartDate = DateTime.Now;
            FireTunnelCreatedEvent();

            _channelSrcDst.StartExchange();
            _channelDstSrc.StartExchange();
        }

        private void OnError_AtomicShutdown()
        {
            // Если ошибка происходит в одном из каналов то закрыть оба канала.
            if (Interlocked.CompareExchange(ref _shutdown, 1, 0) == 0)
            {
                Debug.WriteLine("TcpTunnel.AtomicShutdown()");

                // Потокобезопасно останавливает сокеты.
                _channelSrcDst.Shutdown();
                _channelDstSrc.Shutdown();

                _channelSrcDst.Dispose();
                Debug.WriteLine("Channel #1 disposed");
                _channelDstSrc.Dispose();
                Debug.WriteLine("Channel #2 disposed");

                _channelSrcDst.Dispose();
                _channelDstSrc.Dispose();

                FireTunnelClosedEvent();
            }
        }

        private void FireTunnelCreatedEvent()
        {
            TunnelCreated?.Invoke(this, EventArgs.Empty);
        }

        private void FireTunnelClosedEvent()
        {
            TunnelClosed?.Invoke(this, EventArgs.Empty);
        }
    }
}
