using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SimpleDoc.Labels
{
    [XmlRoot("section")]
    public class Section
    {
        public Section()
        {
            Content = new List<ContentBase>();
        }

        public Section(string name)
            : this()
        {
            Name = name;        
        }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

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

        [XmlArray("content")]
        [XmlArrayItem("br", typeof(Linebreak))]
        [XmlArrayItem("text", typeof(Text))]
        [XmlArrayItem("barcode", typeof(Barcode))]
        public List<ContentBase> Content { get; set; }

        [XmlIgnore]
        public int X { get; set; }

        [XmlIgnore]
        public int Y { get; set; }


        internal string Emit(PrintInfo printInfo)
        {
            var zpl = new StringBuilder();

            zpl.AppendLine($"^FX {Name}");

            // @TODO: Figure out starting cursor position if specified.
            X = printInfo.ToDotX(MarginLeft);
            Y = printInfo.ToDotY(MarginTop);

            zpl.Append($"^FO{X},{Y}");

            // This is all wrong now we know about ^FB...
            foreach (var item in Content)
            {
                zpl.Append(item.Emit(this));
            }

            zpl.Append($"^FS");

            zpl.AppendLine();
            zpl.AppendLine();

            return zpl.ToString();
        }

    }
}
