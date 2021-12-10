using MCSM_Core.Schems;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

using static MCSM_Core.Extensions.BufferExtensions;
#if DEBUG
#endif

// https://gist.github.com/csh/2480d14fbbb33b4bbae3

namespace MCSM_Core
{
	public static class ServerPing
	{
		private static readonly Dictionary<char, ConsoleColor> Colours = new Dictionary<char, ConsoleColor>
		{
			 { '0', ConsoleColor.Black       },
			 { '1', ConsoleColor.DarkBlue    },
			 { '2', ConsoleColor.DarkGreen   },
			 { '3', ConsoleColor.DarkCyan    },
			 { '4', ConsoleColor.DarkRed     },
			 { '5', ConsoleColor.DarkMagenta },
			 { '6', ConsoleColor.Yellow      },
			 { '7', ConsoleColor.Gray        },
			 { '8', ConsoleColor.DarkGray    },
			 { '9', ConsoleColor.Blue        },
			 { 'a', ConsoleColor.Green       },
			 { 'b', ConsoleColor.Cyan        },
			 { 'c', ConsoleColor.Red         },
			 { 'd', ConsoleColor.Magenta     },
			 { 'e', ConsoleColor.Yellow      },
			 { 'f', ConsoleColor.White       },
			 { 'k', Console.ForegroundColor  },
			 { 'l', Console.ForegroundColor  },
			 { 'm', Console.ForegroundColor  },
			 { 'n', Console.ForegroundColor  },
			 { 'o', Console.ForegroundColor  },
			 { 'r', ConsoleColor.White       }
		};

		private static NetworkStream _stream;
		private static List<byte> _buffer;
		private static int _offset;

		public static void Ping(string hostname, short port = 25565)
		{
			TcpClient client = new TcpClient();
			Task task = client.ConnectAsync(hostname, port);
			Console.WriteLine("Connecting to Minecraft server..");

			while (!task.IsCompleted)
			{
#if DEBUG
				Debug.WriteLine("Connecting..");
#endif
				Thread.Sleep(250);
			}

			if (!client.Connected)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Unable to connect to the server");
				Console.ResetColor();
			}

			_buffer = new List<byte>();
			_stream = client.GetStream();
			Console.WriteLine("Sending status request");


			/*
             * Send a "Handshake" packet
             * http://wiki.vg/Server_List_Ping#Ping_Process
             */
			_buffer.WriteInt(47);
			_buffer.WriteString(hostname);
			_buffer.WriteShort(port);
			_buffer.WriteInt(1);
			_buffer.Flush(_stream, 0);

			/*
             * Send a "Status Request" packet
             * http://wiki.vg/Server_List_Ping#Ping_Process
             */
			_buffer.Flush(_stream, 0);

			/*
             * If you are using a modded server then use a larger buffer to account, 
             * see link for explanation and a motd to HTML snippet
             * https://gist.github.com/csh/2480d14fbbb33b4bbae3#gistcomment-2672658
             */
			byte[] buffer = new byte[short.MaxValue ];
			// var buffer = new byte[4096];
			_stream.Read(buffer, 0, buffer.Length);

			try
			{
				int length = buffer.ReadInt(ref _offset);
				int packet = buffer.ReadInt(ref _offset);
				int jsonLength = buffer.ReadInt(ref _offset);
#if DEBUG
				Console.WriteLine("Received packet 0x{0} with a length of {1}", packet.ToString("X2"), length);
#endif
				string json = buffer.ReadString(jsonLength, ref _offset);
				Console.WriteLine("\n\n\n" + json + "\n\n\n");
				Ping ping = JsonConvert.DeserializeObject<Ping>(json);

				if (ping == null)
				{
					Console.WriteLine("Ping is null.");
					return;
				}
				Console.WriteLine("Software: {0}", ping.Version.Name);
				Console.WriteLine("Protocol: {0}", ping.Version.Protocol);
				Console.WriteLine("Players Online: {0}/{1}", ping.Players.Online, ping.Players.Max);
				WriteMotd(ping);
			}
			catch (IOException ex)
			{
				/*
                 * If an IOException is thrown then the server didn't 
                 * send us a VarInt or sent us an invalid one.
                 */
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Unable to read packet length from server,");
				Console.WriteLine("are you sure it's a Minecraft server?");
#if DEBUG
				Console.WriteLine("Here are the details:");
				Console.WriteLine(ex.ToString());
#endif
				Console.ResetColor();
			}
		}

		private static void WriteMotd(Ping ping)
		{
			Console.Write("Motd: ");
			char[] chars = ping.Motd.ToCharArray();
			for (int i = 0; i < ping.Motd.Length; i++)
			{
				try
				{
					if (chars[i] == '\u00A7' && Colours.ContainsKey(chars[i + 1]))
					{
						Console.ForegroundColor = Colours[chars[i + 1]];
						continue;
					}
					if (chars[i - 1] == '\u00A7' && Colours.ContainsKey(chars[i]))
					{
						continue;
					}
				}
				catch (IndexOutOfRangeException)
				{
					// End of string
				}
				Console.Write(chars[i]);
			}
			Console.WriteLine();
			Console.ResetColor();
		}
	}
}