using PortTunneling.Server;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace PortTunneling.Models
{
	[DebuggerDisplay(@"\{Port {SrcPort} => {DstEndPoint.Host,nq}:{DstEndPoint.Port}\}")]
	internal sealed class TunnelBindingModel
	{
        public TunnelsHolder TunnelListener { get; }
        public int ID { get; set; }
		public int SrcPort { get; set; }
		public DnsEndPoint DstEndPoint { get; set; }
		public string Description { get; set; }
		public long DecMac { get; set; }
		public PhysicalAddress Mac { get; set; }

        public TunnelBindingModel(TunnelsHolder tunnelListener)
        {
            TunnelListener = tunnelListener;
        }
	}
}
