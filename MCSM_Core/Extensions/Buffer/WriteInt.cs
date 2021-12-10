using System.Collections.Generic;

namespace MCSM_Core.Extensions
{
	public static partial class BufferExtensions
	{
		public static void WriteInt(this List<byte> buffer, int value)
		{
			while ((value & 128) != 0)
			{
				buffer.Add((byte)(value & 127 | 128));
				value = (int)((uint)value) >> 7;
			}
			buffer.Add((byte)value);
		}
	}
}
