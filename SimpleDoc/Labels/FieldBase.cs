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

        protected abstract int GetFieldX(PrintInfo printInfo);
        protected abstract int GetFieldY(PrintInfo printInfo);
        protected abstract string GetFieldContent(PrintInfo printInfo);

        internal string Emit(PrintInfo printInfo)
        {
            var zpl = new StringBuilder();

            if (!string.IsNullOrEmpty(Name))
            {
                zpl.AppendLine($"^FX {Name}");
            }

            zpl.Append($"^FO{GetFieldX(printInfo)},{GetFieldY(printInfo)}");

            zpl.Append(GetFieldContent(printInfo));

            zpl.Append($"^FS");

            zpl.AppendLine();
            zpl.AppendLine();

            return zpl.ToString();
        }
    }
}
