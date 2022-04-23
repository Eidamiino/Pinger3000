using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Collections.Generic;
using System.Threading;

namespace Pinger3000
{
	internal class Program
	{
		public static List<string> pingResults = new List<string>();
		public static List<long> replyTimes = new List<long>();
		public const int maxLinesOnScreen = 5;
		public const string ipAdd = "8.8.8.8";
		static void Main(string[] args)
		{
			Ping pingSender = new Ping();
			PingOptions options = new PingOptions();

			options.DontFragment = true;

			// Create a buffer of 32 bytes of data to be transmitted.
			string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
			byte[] buffer = Encoding.ASCII.GetBytes(data);
			int timeout = 120;
			var currentCursTop = 1;
			while(true)
			{
				{
					PingReply reply = pingSender.Send(ipAdd, timeout, buffer, options);

					if (reply.Status == IPStatus.Success)
					{
						Console.CursorTop = currentCursTop;
						string replyInfo= 
							$"Reply from {reply.Address.ToString()}: bytes={reply.Buffer.Length} time={reply.RoundtripTime}ms TTL={reply.Options.Ttl}";
						pingResults.Add(replyInfo);
						if (pingResults.Count > maxLinesOnScreen)
						{
							pingResults.RemoveAt(0); 
						}
						Console.Clear();
						for (int j=0;j<pingResults.Count;j++)
						{
							Console.WriteLine(pingResults[j]);
						}
						Thread.Sleep(500);

					}

				}
			}
		}

			private static void PrintPingInfo(PingReply reply)
		{
			Console.WriteLine(
				$"Reply from {reply.Address.ToString()}: bytes={reply.Buffer.Length} time={reply.RoundtripTime}ms TTL={reply.Options.Ttl}");
		}

		private static void PrintLastPingTime(PingReply reply)
		{
			Console.SetCursorPosition(15, 0);
			if (reply.RoundtripTime < 10)
			{
				Console.WriteLine($"0{reply.RoundtripTime}");
			}
			else
			{
				Console.WriteLine(reply.RoundtripTime);
			}

			Thread.Sleep(200);
		}
	}
}
