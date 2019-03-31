using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace SimpleDoc.Labels
{
    [XmlRoot("label")]
    public class Label
    {
        public Label()
        {
            Content = new List<FieldBase>();
        }


        [XmlArray("content")]
        [XmlArrayItem("text", typeof(Text))]
        [XmlArrayItem("barcode", typeof(Barcode))]
        public List<FieldBase> Content { get; set; }


        public string Serialize()
        {
            var serializer = new XmlSerializer(typeof(Label));
            using (var writer = new Utf8StringWriter())
            {
                serializer.Serialize(writer, this);
                return writer.ToString();
            }
        }

        public static Label Deserialize(string xml)
        {
            var serializer = new XmlSerializer(typeof(Label));
            using (var reader = new StringReader(xml))
            {
                return (Label)serializer.Deserialize(reader);
            }
        }


        public string ToZPL(PrintInfo printInfo)
        {
            var zpl = new StringBuilder();
            zpl.AppendLine("^XA");

            foreach(var field in Content)
            {
                zpl.Append(field.Emit(printInfo));
            }

            zpl.AppendLine("^XZ");

            return zpl.ToString().Trim();
        }


        class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }
    }
}
