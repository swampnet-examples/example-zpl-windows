using System.Text;
using System.Xml.Serialization;

namespace SimpleDoc.Labels
{
    /// <summary>
    /// Roughly maps to a ZPL ^FO .... ^FS structure (with the '...' bit supplied by the implemening class)
    /// </summary>
    public abstract class FieldBase
    {
        public FieldBase()
        {

        }

        public FieldBase(string name)
            : this()
        {
            Name = name;
        }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        protected abstract int GetFieldX(Label label, PrintInfo printInfo);
        protected abstract int GetFieldY(Label label, PrintInfo printInfo);
        protected abstract string GetFieldContent(Label label, PrintInfo printInfo);

        internal string Emit(Label label, PrintInfo printInfo)
        {
            var zpl = new StringBuilder();

            if (!string.IsNullOrEmpty(Name))
            {
                zpl.AppendLine($"^FX {Name}");
            }

            zpl.Append($"^FO{GetFieldX(label, printInfo)},{GetFieldY(label, printInfo)}");

            zpl.Append(GetFieldContent(label, printInfo));

            zpl.Append($"^FS");

            zpl.AppendLine();
            zpl.AppendLine();

            return zpl.ToString();
        }
    }
}
