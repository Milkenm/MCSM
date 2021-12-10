using System.Collections.Generic;

namespace MCSM_Core.Extensions
{
	public static partial class BufferExtensions
	{
		public static byte ReadByte(this byte[] buffer, ref int offset)
		{
			byte b = buffer[offset];
			offset += 1;
			return b;
		}

		public static byte ReadByte(this List<byte> buffer, ref int offset)
		{
			return buffer.ToArray().ReadByte(ref offset);
		}
	}
}
