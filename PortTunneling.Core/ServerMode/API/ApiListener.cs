using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PortTunneling.Server.API
{
    internal class ApiListener
    {
        public static ApiListener Instance { get; } = new ApiListener();
        private readonly SocketAsyncEventArgs _args = new SocketAsyncEventArgs();
        private Socket _listener;

        private ApiListener() { }

        public void Enable()
        {
            _listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _listener.Bind(new IPEndPoint(IPAddress.Loopback, 1111));
            _listener.Listen(1);

            _args.Completed += ProcessAccept;
            if (!_listener.AcceptAsync(_args))
                ProcessAccept(null, _args);
        }

        private void StartAccept()
        {
            if (!_listener.AcceptAsync(_args))
                ProcessAccept(null, _args);
        }

        private void ProcessAccept(object sender, SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                var client = e.AcceptSocket;
                ConnectionHandler.Instance.ProcessMessage(client);

            }
            else
                StartAccept();
        }
    }
}
