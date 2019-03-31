using System.Xml.Serialization;

namespace SimpleDoc.Labels
{
    public class Text : FieldBase
    {
        [XmlAttribute(AttributeName = "horizontal-alignment")]
        public HorizontalAlignment HorizontalAlignment { get; set; }

        [XmlAttribute(AttributeName = "vertical-alignment")]
        public VerticalAlignment VerticalAlignment { get; set; }

        [XmlAttribute(AttributeName = "margin")]
        public string Margin { get; set; }

        /// <summary>
        /// In postscript/css points (ie, 1pt == 1/72 inch or 0.3527 mm
        /// </summary>
        [XmlAttribute(AttributeName = "margin-left")]
        public string MarginLeft { get; set; }

        [XmlAttribute(AttributeName = "margin-right")]
        public string MarginRight { get; set; }

        [XmlAttribute(AttributeName = "margin-top")]
        public string MarginTop { get; set; }

        [XmlAttribute(AttributeName = "margin-bottom")]
        public string MarginBottom { get; set; }

        [XmlAttribute(AttributeName = "font")]
        public int Font { get; set; }


        protected override string GetFieldContent(PrintInfo printInfo)
        {
            return "@todo: text formats";
        }

        protected override int GetFieldX(PrintInfo printInfo)
        {
            return printInfo.ToDotX(MarginLeft);
        }

        protected override int GetFieldY(PrintInfo printInfo)
        {
            return printInfo.ToDotX(MarginTop);
        }
    }
}
