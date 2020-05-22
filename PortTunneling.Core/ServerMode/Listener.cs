using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PortTunneling.Server
{
	public sealed class Listener
	{
		private readonly IPEndPoint _localEndPoint;
		private readonly Socket _listener;
		public event EventHandler<SocketConnection> Connected;
		public event EventHandler<SocketConnection> Disconnected;
        public Config Settings => GlobalVars.Settings;

		public Listener(IPEndPoint localEndPoint)
		{
            _localEndPoint = localEndPoint;
            _listener = new Socket(_localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

		public void Listen()
		{
			_listener.Bind(_localEndPoint);
			_listener.Listen((int)SocketOptionName.MaxConnections);

			var e = new SocketAsyncEventArgs();
			e.Completed += ProcessAccept;
			StartAccept(e);
		}

		private void StartAccept(SocketAsyncEventArgs e)
		{
			e.AcceptSocket = new TcpSocket();
			if (!_listener.AcceptAsync(e))
				ProcessAccept(null, e);
		}

		private void ProcessAccept(object sender, SocketAsyncEventArgs e)
		{
			if (e.SocketError == SocketError.Success)
			{
				var tcpSocket = (TcpSocket)e.AcceptSocket;
				StartAccept(e);
                OnConnection(tcpSocket);
            }
            else
            {
                e.Dispose();
            }
		}

        private async void OnConnection(TcpSocket tcpSocket)
        {
            var connection = new SocketConnection(tcpSocket);
            tcpSocket.Disposed += delegate
            {
                Disconnected?.Invoke(this, connection);
            };
            Connected?.Invoke(this, connection);

            try
            {
                TcpTunnel tunnel = await connection.StartServerLoopReadMessagesAsync();
            }
            catch (Exception)
            {

            }
        }

        public void Abort()
        {
            _listener.Dispose();
        }
	}
}
