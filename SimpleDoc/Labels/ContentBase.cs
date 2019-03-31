using System.Xml.Serialization;

namespace SimpleDoc.Labels
{
    public abstract class ContentBase
    {
        [XmlAttribute(AttributeName = "w")]
        public int Width { get; set; }

        [XmlAttribute(AttributeName = "h")]
        public int Height { get; set; }

        [XmlText]
        public string Content { get; set; }

        public abstract string Emit(Section section);
    }
}
