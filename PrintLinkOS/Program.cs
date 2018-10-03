using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zebra.Sdk.Comm;

namespace PrintLinkOS
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var zpl = GenerateZpl("test");

                SendZplOverTcp("127.0.0.1", 9100, zpl); //TcpConnection.DEFAULT_ZPL_TCP_PORT
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

        private static void SendZplOverTcp(string address, int port, string zpl)
        {
            // Instantiate connection for ZPL TCP port at given address
            Connection connection = new TcpConnection(address, port);

            try
            {
                // Open the connection - physical connection is established here.
                connection.Open();

                // Send the data to printer as a byte array.
                connection.Write(Encoding.UTF8.GetBytes(zpl));
            }
            catch (ConnectionException e)
            {
                // Handle communications error here.
                Console.WriteLine(e.ToString());
            }
            finally
            {
                // Close the connection to release resources.
                connection.Close();
            }
        }


        private static string GenerateZpl(string text)
        {
            return @"
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
        }
    }
}
