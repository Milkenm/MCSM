using System.Collections.Generic;
using System.Net.Sockets;

namespace MCSM_Core.Extensions
{
	public static partial class BufferExtensions
	{
		public static void Flush(this List<byte> buffer, NetworkStream netStream, int id = -1)
		{
			byte[] flushBuffer = buffer.ToArray();
			buffer.Clear();

			int add = 0;
			byte[] packetData = new[] { (byte)0x00 };
			if (id >= 0)
			{
				buffer.WriteInt(id);
				packetData = buffer.ToArray();
				add = packetData.Length;
				buffer.Clear();
			}

			buffer.WriteInt(flushBuffer.Length + add);
			byte[] bufferLength = buffer.ToArray();
			buffer.Clear();

			netStream.Write(bufferLength, 0, bufferLength.Length);
			netStream.Write(packetData, 0, packetData.Length);
			netStream.Write(flushBuffer, 0, flushBuffer.Length);
		}
	}
}
