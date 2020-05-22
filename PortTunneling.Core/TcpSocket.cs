using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PortTunneling
{
    public sealed class TcpSocket : Socket
    {
        public event EventHandler Disposed;
        public bool IsDisposed { get; private set; }

        public TcpSocket() : base(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        {
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            IsDisposed = true;
            Disposed?.Invoke(this, EventArgs.Empty);
        }
    }
}
