using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SimpleDoc
{
    public enum HorizontalAlignment
    {
        [XmlEnum("left")]
        Left,

        [XmlEnum("right")]
        Right,

        [XmlEnum("center")]
        Center
    }


    public enum VerticalAlignment
    {
        [XmlEnum("top")]
        Top,

        [XmlEnum("bottom")]
        Bottom,

        [XmlEnum("center")]
        Center
    }
}
