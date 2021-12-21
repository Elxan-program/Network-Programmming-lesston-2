using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Network_Programmming_lesston_2
{
	class Program
	{
		static void Main(string[] args)
		{
			var ip = IPAddress.Parse("10.1.18.38");
			var port = 27001;
			using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
			{
				var ep = new IPEndPoint(ip, port);
				socket.Bind(ep);

				socket.Listen(10);
				Console.WriteLine($"Listening on {socket.LocalEndPoint}");

				while (true)
				{
					var client = socket.Accept();
					Task.Run(() => { 
					Console.WriteLine($"{client.RemoteEndPoint} Connected . . . .");

					var length = 0;
					var bytes = new byte[1024];

					do
					{
						length = client.Receive(bytes);
						var msg = Encoding.UTF8.GetString(bytes, 0, length);
						Console.WriteLine($"Client : {client.RemoteEndPoint} : {msg}");
							if (msg=="ok")
							{
								client.Shutdown(SocketShutdown.Both);
								client.Dispose();
								break;
							}
					} while (true);
					});
				}
			}
		}
	}
}
