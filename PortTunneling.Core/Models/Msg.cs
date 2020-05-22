using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortTunneling.Models
{
	[DebuggerDisplay(@"\{Command = {Command}\}")]
	internal class Msg
	{
		[JsonProperty("M")]
		public Command Command { get; set; }

		[JsonProperty("A", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public object[] Args { get; set; }
	}
}
