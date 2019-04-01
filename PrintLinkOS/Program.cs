using SimpleDoc;
using SimpleDoc.Labels;
using System;
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
                string ip = "10.40.32.212";
                int port = 9100;

                //CheckPrinterStatus(ip, port);

                //string zpl = "";

                //zpl = TestSimpleDoc(new PrintInfo()
                //{
                //    Name = "Zebra",
                //    DPI = 203,
                //    LabelWidthInches = 4,
                //    LabelHeightInches = 3
                //});
                //SendZplOverTcp(ip, port, zpl);


                var label = Label.Deserialize(File.ReadAllText("data\\template-03.xml"));

                SendZplOverTcp(ip, port, label.ToZPL(new PrintInfo()
                {
                    Name = "Zebra",
                    DPI = 203,
                    LabelWidthInches = 4,
                    LabelHeightInches = 3
                }));


                ////SendZplOverTcp(ip, port, GenerateZpl("test"));
                //SendZplOverTcp(ip, port, zpl_tutorial());
                ////PrintImage(ip, port);
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
                connection.Write(Encoding.UTF8.GetBytes(zpl.Trim()));
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
                DisplaySettings(connection);
                connection.Close();
                //ZebraPrinter printer = ZebraPrinterFactory.GetInstance(PrinterLanguage.ZPL, connection);

                //PrinterStatus printerStatus = printer.GetCurrentStatus();
                //if (printerStatus.isReadyToPrint)
                //{
                //    Console.WriteLine("Ready To Print");
                //}
                //else if (printerStatus.isPaused)
                //{
                //    Console.WriteLine("Cannot Print because the printer is paused.");
                //}
                //else if (printerStatus.isHeadOpen)
                //{
                //    Console.WriteLine("Cannot Print because the printer head is open.");
                //}
                //else if (printerStatus.isPaperOut)
                //{
                //    Console.WriteLine("Cannot Print because the paper is out.");
                //}
                //else
                //{
                //    Console.WriteLine("Cannot Print.");
                //}

                //Console.WriteLine();
                //Console.WriteLine($"isHeadCold: {printerStatus.isHeadCold}");
                //Console.WriteLine($"isHeadOpen: {printerStatus.isHeadOpen}");
                //Console.WriteLine($"isHeadTooHot: {printerStatus.isHeadTooHot}");
                //Console.WriteLine($"isPaperOut: {printerStatus.isPaperOut}");
                //Console.WriteLine($"isPartialFormatInProgress: {printerStatus.isPartialFormatInProgress}");
                //Console.WriteLine($"isPaused: {printerStatus.isPaused}");
                //Console.WriteLine($"isReadyToPrint: {printerStatus.isReadyToPrint}");
                //Console.WriteLine($"isReceiveBufferFull: {printerStatus.isReceiveBufferFull}");
                //Console.WriteLine($"isRibbonOut: {printerStatus.isRibbonOut}");
                //Console.WriteLine($"labelLengthInDots: {printerStatus.labelLengthInDots}");
                //Console.WriteLine($"labelsRemainingInBatch: {printerStatus.labelsRemainingInBatch}");
                //Console.WriteLine($"numberOfFormatsInReceiveBuffer: {printerStatus.numberOfFormatsInReceiveBuffer}");
                //Console.WriteLine($"printMode: {printerStatus.printMode}");
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


        /// <summary>
        /// 
        /// head.resolution.in_dpi:203
        /// zpl.label_length:590
        /// odometer.label_dot_length:590
        /// media.width_sense.in_mm:104.1
        /// media.width_sense.in_cm:10.41
        /// media.width_sense.in_dots:832
        /// media.width_sense.in_inches:4.099
        /// sensor.width.cur:209
        /// sensor.width.in_dots:832
        /// 
        /// </summary>
        /// <param name="c"></param>
        private static void DisplaySettings(Connection c)
        {
            ZebraPrinter genericPrinter = ZebraPrinterFactory.GetInstance(c);
            ZebraPrinterLinkOs linkOsPrinter = ZebraPrinterFactory.CreateLinkOsPrinter(genericPrinter);

            if (linkOsPrinter != null)
            {
                Console.WriteLine("Available Settings for myDevice");
                HashSet<string> availableSettings = linkOsPrinter.GetAvailableSettings();
                foreach (string setting in availableSettings)
                {
                    Console.WriteLine($"{setting}: Range = ({linkOsPrinter.GetSettingRange(setting)})");
                }

                Console.WriteLine("\nCurrent Setting Values for myDevice");
                Dictionary<string, string> allSettingValues = linkOsPrinter.GetAllSettingValues();
                foreach (string settingName in allSettingValues.Keys)
                {
                    Console.WriteLine($"{settingName}:{allSettingValues[settingName]}");
                }

                string darknessSettingId = "print.tone";
                string newDarknessValue = "10.0";
                if (availableSettings.Contains(darknessSettingId) &&
                    linkOsPrinter.IsSettingValid(darknessSettingId, newDarknessValue) &&
                    linkOsPrinter.IsSettingReadOnly(darknessSettingId) == false)
                {
                    linkOsPrinter.SetSetting(darknessSettingId, newDarknessValue);
                }

                Console.WriteLine($"\nNew {darknessSettingId} Value = {linkOsPrinter.GetSettingValue(darknessSettingId)}");
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


        private static string TestSimpleDoc(PrintInfo printerInfo)
        {
            var label = new Label()
            {
                Font = "0",
                FontSize = "12pt"
            };

            label.Content.Add(new Barcode("Some test barcode")
            {
                X = "60%",
                Y = "30%",
                Size = "10",
                Type = BarcodeType.QR,
                Content = "ABC-123456789"
            });

            label.Content.Add(new Box() {
                MarginTop = "10pt",
                MarginBottom = "10pt",
                MarginLeft = "10pt",
                MarginRight = "51%",
                BorderColor = BorderColor.Black,
                Thickness = "1pt",
                BorderRounding = 0
            });

            label.Content.Add(new Paragraph()
            {
                MarginTop = "20pt",
                MarginBottom = "20pt",
                MarginLeft = "20pt",
                MarginRight = "50%",
                Content = new List<Text>()
                {
                    new Text() { Content ="ABC-123456789" }
                }
            });

            var xml = label.Serialize();
            Console.WriteLine("____________________________________________________________________________");
            Console.WriteLine(xml);
            Console.WriteLine("____________________________________________________________________________");
            var zpl = label.ToZPL(printerInfo);
            Console.WriteLine(zpl);
            Console.WriteLine("____________________________________________________________________________");

            return zpl;
        }
    }
}
