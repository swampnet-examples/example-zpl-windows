using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SimpleDoc.Labels
{
    public class Paragraph : FieldBase
    {
        public Paragraph()
        {
            Content = new List<Text>();
        }

        public Paragraph(string name)
            : this()
        {
            Name = name;
        }

        [XmlAttribute("horizontal-alignment")]
        public HorizontalAlignment HorizontalAlignment { get; set; }

        [XmlAttribute("vertical-alignment")]
        public VerticalAlignment VerticalAlignment { get; set; }

        //[XmlAttribute(AttributeName = "margin")]
        //public string Margin { get; set; }

        [XmlAttribute("margin-left")]
        public string MarginLeft { get; set; }

        [XmlAttribute("margin-right")]
        public string MarginRight { get; set; }

        [XmlAttribute("margin-top")]
        public string MarginTop { get; set; }

        [XmlAttribute("margin-bottom")]
        public string MarginBottom { get; set; }

        [XmlAttribute("font-type")]
        public int Font { get; set; }

        [XmlAttribute("font-size")]
        public string FontSize { get; set; }


        [XmlElement("text")]
        public List<Text> Content { get; set; }

        protected override string GetFieldContent(PrintInfo printInfo)
        {
            var zpl = new StringBuilder();

            foreach(var t in Content)
            {

            }

            return zpl.ToString().Trim();
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


    public class Text
    {
        [XmlText]
        public string Content { get; set; }
    }
}
