using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SimpleDoc.Labels
{
    /// <summary>
    ///  Raw ZPL
    /// </summary>
    public class Zpl : FieldBase
    {
        [XmlAttribute("x")]
        public string X { get; set; }

        [XmlAttribute("y")]
        public string Y { get; set; }

        [XmlText]
        public string Content { get; set; }


        protected override string GetFieldContent(Label label, PrintInfo printInfo)
        {
            return Content;
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
