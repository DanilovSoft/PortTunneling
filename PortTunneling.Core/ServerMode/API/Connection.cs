using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PortTunneling.Server.API
{
    internal class ConnectionHandler
    {
        public static ConnectionHandler Instance { get; } = new ConnectionHandler();

        private ConnectionHandler() { }

        public void ProcessMessage(Socket socket)
        {
            using (var nstream = new NetworkStream(socket, true))
            {
                
            }
        }
    }
}
