using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SimpleDoc.Labels
{
    public enum BorderColor
    {
        Black,
        White
    }

    public class Box : FieldBase
    {
        [XmlAttribute("margin-left")]
        public string MarginLeft { get; set; }

        [XmlAttribute("margin-right")]
        public string MarginRight { get; set; }

        [XmlAttribute("margin-top")]
        public string MarginTop { get; set; }

        [XmlAttribute("margin-bottom")]
        public string MarginBottom { get; set; }

        [XmlAttribute("border-thickness")]
        public string Thickness { get; set; }

        [XmlAttribute("border-color")]
        public BorderColor BorderColor { get; set; }

        [XmlAttribute("border-rounding")]
        public int BorderRounding { get; set; }

        protected override string GetFieldContent(Label label, PrintInfo printInfo)
        {
            var width = printInfo.LabelWidthDots - printInfo.ToDotX(MarginRight) - printInfo.ToDotX(MarginLeft);
            var height = printInfo.LabelHeightDots - printInfo.ToDotY(MarginTop) - printInfo.ToDotX(MarginBottom);
            var thickness = printInfo.PointToDot(Convert.ToDouble(Thickness.Replace("pt", "")));
            var zpl = new StringBuilder();
            var c = BorderColor == BorderColor.Black ? "B" : "W";

            zpl.Append($"^GB{width},{height},{thickness},{c},{BorderRounding}");

            return zpl.ToString().Trim();
        }


        protected override int GetFieldX(Label label, PrintInfo printInfo)
        {
            return printInfo.ToDotX(MarginLeft);
        }


        protected override int GetFieldY(Label label, PrintInfo printInfo)
        {
            return printInfo.ToDotY(MarginTop);
        }
    }
}
