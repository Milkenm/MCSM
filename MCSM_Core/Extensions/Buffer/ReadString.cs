using System.Collections.Generic;
using System.Text;

namespace MCSM_Core.Extensions
{
	public static partial class BufferExtensions
	{
		public static string ReadString(this byte[] buffer, int length, ref int offset)
		{
			byte[] data = buffer.Read(length, ref offset);
			return Encoding.UTF8.GetString(data);
		}

		public static string ReadString(this List<byte> buffer, int length, ref int offset)
		{
			return buffer.ToArray().ReadString(length, ref offset);
		}
	}
}
