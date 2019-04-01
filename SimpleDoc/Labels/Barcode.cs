using System;
using System.Text;
using System.Xml.Serialization;

namespace SimpleDoc.Labels
{
    public enum BarcodeType
    {
        CODE_128,
        QR
    }

    public class Barcode : FieldBase
    {
        public Barcode()
        {

        }

        public Barcode(string name)
            : base(name)
        {

        }

        [XmlAttribute(AttributeName = "type")]
        public BarcodeType Type { get; set; }

        [XmlAttribute("x")]
        public string X { get; set; }

        [XmlAttribute("y")]
        public string Y { get; set; }

        /// <summary>
        /// Size == Magnification level for QR types
        /// </summary>
        [XmlAttribute("size")]
        public string Size { get; set; }

        [XmlText]
        public string Content { get; set; }

        protected override string GetFieldContent(Label label, PrintInfo printInfo)
        {
            var zpl = new StringBuilder();

            switch (Type)
            {
                case BarcodeType.CODE_128:
                    zpl.Append(Generate_CODE_128(printInfo));
                    break;

                case BarcodeType.QR:
                    zpl.Append(Generate_QR(printInfo));
                    break;
            }

            return zpl.ToString().Trim();
        }


        private string Generate_QR(PrintInfo printInfo)
        {
            var a = "N";    // Orientation
            var b = "2";    // Model (1:original, 2:enhanced)
            var c = 10;   // Magnification factor
            var errorCorrection = "QR";

            if(int.TryParse(Size, out int mag))
            {
                c = mag;
            }

            return $"^BQ{a},{b},{c}^FD{errorCorrection},{Content}";
        }


        private string Generate_CODE_128(PrintInfo printInfo)
        {
            var o = "N";    // Orientation
            var h = printInfo.ToDotY(Size);   // Height / dots
            var f = "Y";    // Print interpretation line
            var g = "N";    // Print interpretation line above code
            var e = "N";    // UCC Check digit
            var m = "N";    // Mode

            return $"^BC{o},{h},{f},{g},{e},{m}^FD{Content}";
        }

        protected override int GetFieldX(Label label, PrintInfo printInfo)
        {
            return printInfo.ToDotX(X);
        }

        protected override int GetFieldY(Label label, PrintInfo printInfo)
        {
            return printInfo.ToDotY(Y);
        }
    }
}
