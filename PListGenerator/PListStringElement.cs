using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PListFormatter
{
    public class PListStringElement : PListElement
    {
        public PListStringElement(string value)
            : base(null, "string", value)
        { }

        public PListStringElement(string key, string value)
            : base(key, "string", value)
        { }

        internal override void AppendToXml(System.Xml.Linq.XElement parentElement)
        {
            if (key == null)
            {
                AddValueElement(parentElement);
            }
            else
            {
                base.AppendToXml(parentElement);
            }
        }
    }
}
