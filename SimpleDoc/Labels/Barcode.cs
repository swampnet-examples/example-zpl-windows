using System.Xml.Serialization;

namespace SimpleDoc.Labels
{
    public class Barcode : FieldBase
    {
        [XmlAttribute(AttributeName = "type")]
        public int Type { get; set; }

        [XmlAttribute("x")]
        public string X { get; set; }

        [XmlAttribute("y")]
        public string Y { get; set; }

        [XmlText]
        public string Content { get; set; }

        protected override string GetFieldContent(PrintInfo printInfo)
        {
            return "@todo: barcode formats";
        }

        protected override int GetFieldX(PrintInfo printInfo)
        {
            return printInfo.ToDotX(X);
        }

        protected override int GetFieldY(PrintInfo printInfo)
        {
            return printInfo.ToDotX(Y);
        }
    }
}
