using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PortTunneling
{
	public static class ExtensionMethods
	{
		public static async Task ReceiveAsyncN(this Socket socket, byte[] buffer, int offset, int count)
		{
			int n;
			while ((count -= n = await Task.Factory.FromAsync(socket.BeginReceive(buffer, offset, count, SocketFlags.None, null, socket), socket.EndReceive).ConfigureAwait(false)) > 0)
            {
                if (n != 0)
                    offset += n;
                else
                    throw new SocketException((int)SocketError.Shutdown);
            }
        }

		public static Task<int> SendTaskAsync(this Socket socket, byte[] buffer, int offset, int count)
		{
			return Task.Factory.FromAsync(socket.BeginSend(buffer, offset, count, SocketFlags.None, null, socket), socket.EndSend);
		}

        public static string ToString(this PhysicalAddress mac, string separator)
        {
            return string.Join(separator, mac.GetAddressBytes().Select(x => x.ToString("X2")));
        }

        public static T Value<T>(this DbDataReader reader, string columnName)
        {
            var obj = reader[columnName];
            return (T)ChangeType(obj, typeof(T), obj.GetType(), columnName);
        }

        private static object ChangeType(object value, Type propertyType, Type columnType, string columnName)
        {
            if (value == DBNull.Value)
                return null;

            bool isAssignable = propertyType.IsAssignableFrom(columnType);
            if (!isAssignable || value == null)
            {
                Type nullableType = Nullable.GetUnderlyingType(propertyType);
                if (nullableType == null)
                {
                    if (!propertyType.IsValueType || value != null)
                    {
                        if (propertyType != columnType)
                        {
                            value = ChangeType(value, propertyType, columnName);
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException($"Error converting value {{null}} to type '{propertyType.FullName}'. Column name '{columnName}'.");
                    }
                }
                else
                {
                    if (value != null && nullableType != columnType)
                    {
                        value = ChangeType(value, nullableType, columnName);
                    }
                }
            }
            return value;
        }

        private static object ChangeType(object value, Type conversionType, string columnName)
        {
            try
            {
                if (!conversionType.IsEnum)
                {
                    return Convert.ChangeType(value, conversionType);
                }
                else
                {
                    if (value is string sValue)
                    {
                        return Enum.Parse(conversionType, sValue);
                    }
                    else
                    {
                        return Enum.ToObject(conversionType, value);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error converting value {value} to type '{conversionType.FullName}'. Column name '{columnName}'.", ex);
            }
        }
    }
}

namespace System.Net.Sockets
{
	public static class SocketExtensions
	{
		public static int SetKeepAlive(this Socket socket, TimeSpan time, TimeSpan interval)
		{
			if (time == TimeSpan.Zero || interval == TimeSpan.Zero)
				return -1;

			var input = new uint[]
            {
             	1,
 				(uint)time.TotalMilliseconds,
 				(uint)interval.TotalMilliseconds,
			};

			byte[] inValue = new byte[3 * 4];
			
			for (int i = 0; i < input.Length; i++)
			{
				inValue[i * 4] = (byte)(input[i]);
				inValue[i * 4 + 1] = (byte)(input[i] >> 8);
				inValue[i * 4 + 2] = (byte)(input[i] >> 16);
				inValue[i * 4 + 3] = (byte)(input[i] >> 24);
			}

			return socket.IOControl(IOControlCode.KeepAliveValues, inValue, null);
		}
	}
}
