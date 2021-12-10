using System.Collections.Generic;
using System.IO;

namespace MCSM_Core.Extensions
{
	public static partial class BufferExtensions
	{
		public static int ReadInt(this byte[] buffer, ref int offset)
		{
			int value = 0;
			int size = 0;
			int b;
			while (((b = buffer.ReadByte(ref offset)) & 0x80) == 0x80)
			{
				value |= (b & 0x7F) << (size++ * 7);
				if (size > 5)
				{
					throw new IOException($"Invalid Int with size '{size}'.");
				}
			}
			return value | ((b & 0x7F) << (size * 7));
		}

		public static int ReadInt(this List<byte> buffer, ref int offset)
		{
			return buffer.ToArray().ReadInt(ref offset);
		}
	}
}
