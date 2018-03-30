using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrintZpl
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				new Program().Run();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			finally
			{
				Console.WriteLine("key");
				Console.ReadKey(true);
			}
		}

		private readonly string _address = "127.0.0.1";
		private readonly int _port = 9100;

		private void Run()
		{
			Print($"Test-data 123456");
		}


		private void Print(string text)
		{
			Console.WriteLine(">> " + text);

			string zpl = @"
					^XA

					^FX test lines, various point sizes
					^CFA,15
					^FO50,10^FD" + text + @"^FS
					^CFA,30
					^FO50,30^FD" + text + @"^FS
					^CFA,60
					^FO50,60^FD" + text + @"^FS

					^FX line
					^FO50,130^GB1000,1,3^FS

					^FX barcode
					^BY5,2,300
					^FO100,150^BC^FD" + text + @"^FS

					^XZ
			";

			SendData(zpl);
		}


		/// <summary>
		/// Opening a direct connection to a printer and pushing ZPL commands to it
		/// </summary>
		/// <param name="zpl"></param>
		private void SendData(string zpl)
		{
			var printerIP = new IPEndPoint(IPAddress.Parse(_address), _port);

			using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
			{
				socket.Connect(printerIP);

				using (var ns = new NetworkStream(socket))
				{
					byte[] toSend = Encoding.ASCII.GetBytes(zpl);
					ns.Write(toSend, 0, toSend.Length);
				}
			}
		}
	}
}
