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

        internal override void AppendToXml(XElement parentElement)
        {
            AddKeyElement(parentElement);

            XElement valueElement =null;

            if(value as string != null)
            {
                valueElement = new XElement("data", "\n" + Value + "\n");
            }
            else
            {
                valueElement= new XElement("data", "\n" + Encoding.ASCII.GetString((byte[])value) + "\n");
            }
            
            parentElement.Add(valueElement);
        }
    }
}
