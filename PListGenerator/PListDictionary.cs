using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PListFormatter
{
    public class PListDictionary : PListElement
    {
        public PListDictionary()
        {
            Init();
        }

        public PListDictionary(string key)
            : base(key)
        {
            Init();
        }

        private void Init()
        {
            this.Elements = new List<PListElement>();
        }

        public List<PListElement> Elements { get; private set; }

        protected override void AddElement(PListElement element)
        {
            Elements.Add(element);
        }

        internal override void AppendToXml(XElement parentElement)
        {
            if (Key != null)
            {
                XElement keyElement = new XElement("key", Key);
                parentElement.Add(keyElement);
            }

            XElement dictElement = new XElement("dict");

            foreach (var element in Elements)
            {
                element.AppendToXml(dictElement);
            }

            parentElement.Add(dictElement);
        }
    }
}
