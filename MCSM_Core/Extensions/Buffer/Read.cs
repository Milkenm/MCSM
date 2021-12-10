using System;
using System.Collections.Generic;

namespace MCSM_Core.Extensions
{
	public static partial class BufferExtensions
	{
		public static byte[] Read(this byte[] buffer, int length, ref int offset)
		{
			byte[] data = new byte[length];
			Array.Copy(buffer, offset, data, 0, length);
			offset += length;
			return data;
		}

		public static byte[] Read(this List<byte> buffer, int length, ref int offset)
		{
			return Read(buffer.ToArray(), length, ref offset);
		}
	}
}
