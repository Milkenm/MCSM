using System.Collections.Generic;
using System.Text;

namespace MCSM_Core.Extensions
{
	public static partial class BufferExtensions
	{
		public static void WriteString(this List<byte> buffer, string data)
		{
			byte[] stringBuffer = Encoding.UTF8.GetBytes(data);
			buffer.WriteInt(stringBuffer.Length);
			buffer.AddRange(stringBuffer);
		}
	}
}
