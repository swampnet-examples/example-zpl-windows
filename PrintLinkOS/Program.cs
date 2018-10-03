﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;

namespace PrintLinkOS
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Not on my emulator?
                //CheckPrinterStatus("127.0.0.1", 9100);
                //PrintImage("127.0.0.1", 9100);

                SendZplOverTcp("127.0.0.1", 9100, GenerateZpl("test")); //TcpConnection.DEFAULT_ZPL_TCP_PORT
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


        private static void CheckPrinterStatus(string address, int port)
        {
            Connection connection = new TcpConnection(address, port);
            try
            {
                connection.Open();
                ZebraPrinter printer = ZebraPrinterFactory.GetInstance(PrinterLanguage.ZPL, connection);

                PrinterStatus printerStatus = printer.GetCurrentStatus();
                if (printerStatus.isReadyToPrint)
                {
                    Console.WriteLine("Ready To Print");
                }
                else if (printerStatus.isPaused)
                {
                    Console.WriteLine("Cannot Print because the printer is paused.");
                }
                else if (printerStatus.isHeadOpen)
                {
                    Console.WriteLine("Cannot Print because the printer head is open.");
                }
                else if (printerStatus.isPaperOut)
                {
                    Console.WriteLine("Cannot Print because the paper is out.");
                }
                else
                {
                    Console.WriteLine("Cannot Print.");
                }
            }
            catch (ConnectionException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (ZebraPrinterLanguageUnknownException e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                connection.Close();
            }
        }


        private static void PrintImage(string address, int port)
        {
            Connection connection = new TcpConnection(address, port);
            try
            {
                connection.Open();
                ZebraPrinter printer = ZebraPrinterFactory.GetInstance(PrinterLanguage.ZPL, connection);

                int x = 0;
                int y = 0;
                printer.PrintImage("data/mooncake.png", x, y);
            }
            catch (ConnectionException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (ZebraPrinterLanguageUnknownException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                connection.Close();
            }
        }


        private static string GenerateZpl(string text)
        {
            return 
                @"
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