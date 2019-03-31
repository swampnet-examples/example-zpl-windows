using System.Xml.Serialization;

namespace SimpleDoc.Labels
{
    public class Text : ContentBase
    {
        [XmlAttribute(AttributeName = "font")]
        public int Font { get; set; }

        public override string Emit(Section section)
        {
            return $"[text: {Content}]";
        }
    }
}
