using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortTunneling.Models
{
	internal enum Command
	{
		StartTunnel,
        /// <summary>
        /// Клиент отправляет свои идентификаторы.
        /// </summary>
		Init,
        /// <summary>
        /// Идентификатор соединения.
        /// </summary>
        //ConId,
		EndPoint,
	}
}
