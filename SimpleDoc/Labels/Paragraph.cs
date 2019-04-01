using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Serialization;
using System;

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

            zpl.Append($"^CF{Font},{printInfo.ToDotY(FontSize)}");

            int width = (int)printInfo.LabelWidthDots - GetFieldX(printInfo);
            int maxLines = Content.Count();
            int kerning = 0;
            string justify = GetJustification(HorizontalAlignment);
            int hangingIndent = 0;

            // Field Block
            zpl.Append($"^FB{width},{maxLines},{kerning},{justify},{hangingIndent}");

            // Field Data
            zpl.Append($"^FD");
            zpl.Append(string.Join("\\&", Content.Select(c => c.Content)));

            return zpl.ToString().Trim();
        }


        protected override int GetFieldX(PrintInfo printInfo)
        {
            return printInfo.ToDotX(MarginLeft);
        }


        protected override int GetFieldY(PrintInfo printInfo)
        {
            int y;
            var fontSize = printInfo.ToDotY(FontSize);
            var kerning = 0;
            var textBlockHeight = Content.Count() * (fontSize + kerning);

            switch (VerticalAlignment)
            {
                case VerticalAlignment.Bottom:
                    y = (int)printInfo.LabelHeightDots - printInfo.ToDotY(MarginBottom) - textBlockHeight;
                    break;

                case VerticalAlignment.Center:
                    y = ((((int)printInfo.LabelHeightDots - printInfo.ToDotY(MarginBottom)) / 2) + printInfo.ToDotY(MarginTop)) - (textBlockHeight/2);
                    break;

                // Default to .Top
                default:
                    y = printInfo.ToDotY(MarginTop);
                    break;
            }

            return y;
        }


        private string GetJustification(HorizontalAlignment horizontalAlignment)
        {
            string align = "L";
            switch (horizontalAlignment)
            {
                case HorizontalAlignment.Right:
                    align = "R";
                    break;

                case HorizontalAlignment.Center:
                    align = "C";
                    break;
            }
            return align;
        }
    }


    public class Text
    {
        [XmlText]
        public string Content { get; set; }
    }
}
