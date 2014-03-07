using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PListFormatter
{
    public class PListArray : PListElement, IEnumerable<PListElement>
    {
        public PListArray(string key)
            : base(key)
        {
            this.Elements = new List<PListElement>();
        }

        public List<PListElement> Elements { get; private set; }

        public IEnumerator<PListElement> GetEnumerator()
        {
            return Elements.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Elements.GetEnumerator();
        }

        protected override void AddElement(PListElement element)
        {
            Elements.Add(element);
        }

        internal override void AppendToXml(XElement parentElement)
        {
            if (key != null)
            {
                XElement keyElement = new XElement("key", key);
                parentElement.Add(keyElement);
            }

            XElement arrayElement = new XElement("array");

            foreach (var element in Elements)
            {
                element.AppendToXml(arrayElement);
            }

            parentElement.Add(arrayElement);
        }
    }
}
