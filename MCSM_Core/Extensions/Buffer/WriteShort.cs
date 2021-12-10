using System;
using System.Collections.Generic;

namespace MCSM_Core.Extensions
{
	public static partial class BufferExtensions
	{
		public static void WriteShort(this List<byte> buffer, short data)
		{
			buffer.AddRange(BitConverter.GetBytes(data));
		}
	}
}
