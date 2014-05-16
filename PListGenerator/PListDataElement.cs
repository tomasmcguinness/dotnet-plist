using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PListFormatter
{
    public class PListDataElement : PListElement
    {
        public PListDataElement(string key, object value)
            : base(key, "data", value)
        { }

        public PListDataElement(object value)
        {
            this.value = value;
        }

        internal override void AppendToXml(XElement parentElement)
        {
            AddKeyElement(parentElement);

            XElement valueElement = null;

            if (value as string != null)
            {
                byte[] asciiString = Encoding.ASCII.GetBytes((string)value);
                string elementValue = this.Encode ? System.Convert.ToBase64String(asciiString) : (string)value;

                valueElement = new XElement("data", elementValue);
            }
            else
            {
                string asciiString = Encoding.ASCII.GetString((byte[])value);
                string elementValue = this.Encode ? System.Convert.ToBase64String((byte[])value) : asciiString;

                valueElement = new XElement("data", elementValue);
            }

            parentElement.Add(valueElement);
        }

        public bool Encode { get; set; }
    }
}
