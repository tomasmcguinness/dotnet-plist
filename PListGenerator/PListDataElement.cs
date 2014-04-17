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
        private string p;

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
                valueElement = new XElement("data", "\n" + Value + "\n");
            }
            else
            {
                valueElement = new XElement("data", "\n" + Encoding.ASCII.GetString((byte[])value) + "\n");
            }

            parentElement.Add(valueElement);
        }
    }
}
