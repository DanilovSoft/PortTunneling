using Newtonsoft.Json;
using PortTunneling.ServerMode;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PortTunneling
{
	public static class GlobalVars
	{
		private static readonly string _databasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SqLite.DatabaseFile);
		public static SqLite SQL { get; private set; }
        /// <summary>
        /// Коллекция соединений на стороне сервера, где ключ это MAC адрес удалённой машины.
        /// </summary>
		internal static ConcurrentConnections ClientConnections { get; private set; }
		public static Config Settings { get; private set; }

		static GlobalVars()
		{
			ClientConnections = new ConcurrentConnections();
			SQL = new SqLite($"Data Source={_databasePath}");

			Settings = new Config();
			Settings.Initialize();
		}

		public static long ToDecMac(string mac)
		{
            byte[] bytes = PhysicalAddress.Parse(mac).GetAddressBytes();

			long num = 0;
			for (int i = bytes.Length - 1; i >= 0; i--)
				num = (num << 8) | bytes[i];

			return num;
		}

		public static long ToDecMac(PhysicalAddress mac)
		{
			var b = mac.GetAddressBytes();

			long num = 0;
			for (int i = b.Length - 1; i >= 0; i--)
				num = (num << 8) | b[i];

			return num;
		}

		[Conditional("DEBUG")]
		public static void WaitDebugger()
		{
#if DEBUG
			while (!Debugger.IsAttached)
				Thread.Sleep(100);

			Debugger.Break();
#endif
		}
	}
}
