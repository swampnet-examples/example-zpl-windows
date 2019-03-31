using System.Xml.Serialization;

namespace SimpleDoc.Labels
{
    public class Barcode : ContentBase
    {
        [XmlAttribute(AttributeName = "type")]
        public int Type { get; set; }

        public override string Emit(Section section)
        {
            return $"[barcode: {Content}]";
        }
    }
}
