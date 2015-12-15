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
            this.Elements = new Dictionary<string, PListElement>();
        }

        public Dictionary<string, PListElement> Elements { get; private set; }

        public dynamic this[string index]
        {
            get
            {
                var value = Elements[index];

                if (value.GetType() == typeof(PListDictionary) || value.GetType() == typeof(PListArray))
                {
                    return Elements[index];
                }
                else
                {
                    return Elements[index].Value;
                }
            }
        }

        protected override void AddElement(PListElement element)
        {
            Elements.Add(element.Key, element);
        }

        internal override void AppendToXml(XElement parentElement)
        {
            if (key != null)
            {
                XElement keyElement = new XElement("key", key);
                parentElement.Add(keyElement);
            }

            XElement dictElement = new XElement("dict");

            foreach (var element in Elements)
            {
                element.Value.AppendToXml(dictElement);
            }

            parentElement.Add(dictElement);
        }

        public bool ContainsKey(string key)
        {
            return Elements.ContainsKey(key);
        }


    }
}
