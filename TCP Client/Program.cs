using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCP_Client
{
	class Program
	{
		static void Send()
		{
			var soket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			var ip = IPAddress.Parse("10.1.18.38");
			var port = 27001;
			var ep = new IPEndPoint(ip, port);
			try
			{
				soket.Connect(ep);
				if (soket.Connected)
				{
					Console.WriteLine("Connected to the server . . . . .");
					while (true)
					{
						var msg = Console.ReadLine();
						var bytes = Encoding.UTF8.GetBytes(msg);
						soket.Send(bytes);
					}
				}
				else
				{
					Console.WriteLine("Can not connected to the server . . . . .");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Can not connected to the server . . . . .");
				Console.WriteLine(ex.Message);
			}
		}
		static void Recieve()
		{

		} 
		static void Main(string[] args)
		{
			while (true)
			{
				Task.Run(() => {
					Send();
				});
			}
		}
	}
}
