using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortTunneling
{
    public static class MethodExtensions
    {
        public static void CreateParameters(this SQLiteCommand com, IList<object> args)
        {
            if (args == null)
                return;

            for (int index = 0; index < args.Count; ++index)
                com.Parameters.AddWithValue(index.ToString(), args[index]);
        }
    }
}
