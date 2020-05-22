using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace PortTunneling.ServerMode.Dto
{
    internal sealed class TunnelModel
    {
        public int ID { get; internal set; }
        public bool Enabled { get; internal set; }
        public int SrcPort { get; internal set; }
        public DnsEndPoint DstEndPoint { get; internal set; }
        public PhysicalAddress Mac { get; internal set; }
        public string Description { get; internal set; }
    }
}
