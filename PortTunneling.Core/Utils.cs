using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortTunneling
{
    public static class Utils
    {
        public static bool IsHex(IEnumerable<char> chars)
        {
            foreach (char c in chars)
            {
                if (!IsHex(c))
                    return false;
            }
            return true;
        }

        public static bool IsHex(char c)
        {
            return (c >= '0' && c <= '9') ||
                         (c >= 'a' && c <= 'f') ||
                         (c >= 'A' && c <= 'F');
        }
    }
}
