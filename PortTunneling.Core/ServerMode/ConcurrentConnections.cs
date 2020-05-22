using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace PortTunneling.ServerMode
{
    [DebuggerDisplay(@"\{Count = {_dict.Count}\}")]
    internal sealed class ConcurrentConnections
    {
        private readonly Dictionary<PhysicalAddress, SortedList<int, SocketConnection>> _dict = new Dictionary<PhysicalAddress, SortedList<int, SocketConnection>>();

        public void Add(PhysicalAddress mac, SocketConnection socketConnection)
        {
            lock (_dict)
            {
                if(_dict.TryGetValue(mac, out var list))
                {
                    list.Add(socketConnection.ConnectionId.Value, socketConnection);
                }
                else
                {
                    _dict.Add(mac, new SortedList<int, SocketConnection>() { { socketConnection.ConnectionId.Value, socketConnection } });
                }
            }
        }

        internal bool Remove(PhysicalAddress mac, SocketConnection socketConnection)
        {
            lock (_dict)
            {
                if (_dict.TryGetValue(mac, out var list))
                {
                    bool removed = list.Remove(socketConnection.ConnectionId.Value);

                    if (list.Count == 0)
                        _dict.Remove(mac);

                    return removed;
                }
                return false;
            }
        }

        internal bool TryRemove(PhysicalAddress mac, out SocketConnection socketConnection)
        {
            lock (_dict)
            {
                if (_dict.TryGetValue(mac, out var list))
                {
                    socketConnection = list.Values[0];
                    list.Remove(socketConnection.ConnectionId.Value);

                    if (list.Count == 0)
                        _dict.Remove(mac);
                    
                    return true;
                }

                socketConnection = null;
                return false;
            }
        }
    }
}
