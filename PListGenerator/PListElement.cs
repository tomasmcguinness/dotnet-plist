using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PListFormatter
{
    public abstract class PListElement
    {
        protected string key;
        private string ValueTypeName;
        protected object value;

        public PListElement()
        { }

        public PListElement(string key)
        {
            this.key = key;
        }

        public PListElement(string key, object value)
        {
            this.key = key;
            this.value = value;
        }

        public PListElement(string key, string valueTypeName, object value)
        {
            this.key = key;
            this.ValueTypeName = valueTypeName;
            this.value = value;
        }

        public string Key
        {
            get
            {
                return key;
            }
        }

        public object Value
        {
            get
            {
                return value;
            }
        }

        public PListDictionary AddDictionary(string key)
        {
            PListDictionary dictionary = new PListDictionary(key);
            AddElement(dictionary);
            return dictionary;
        }

        public PListDictionary AddDictionary()
        {
            PListDictionary dictionary = new PListDictionary();
            AddElement(dictionary);
            return dictionary;
        }

        public PListArray AddArray(string key)
        {
            PListArray array = new PListArray(key);
            AddElement(array);
            return array;
        }

        public PListElement AddString(string key, string value)
        {
            PListStringElement element = new PListStringElement(key, value);
            AddElement(element);
            return this;
        }

        public PListElement AddBool(string key)
        {
            return AddBool(key, false);
        }

        public PListElement AddBool(string key, bool value)
        {
            PListBoolElement element = new PListBoolElement(key, value);
            AddElement(element);
            return this;
        }

        public PListElement AddInteger(string key, int value)
        {
            PListIntegerElement element = new PListIntegerElement(key, value);
            AddElement(element);
            return this;
        }

        public PListElement AddData(string key, object value)
        {
            PListDataElement element = new PListDataElement(key, value);
            AddElement(element);
            return this;
        }

        public PListElement AddData(string key, string value)
        {
            PListDataElement element = new PListDataElement(key, value);
            AddElement(element);
            return this;
        }

        protected virtual void AddElement(PListElement element)
        {
            throw new InvalidOperationException("You cannot add a child element");
        }

        protected virtual void AddKeyElement(XElement parentElement)
        {
            XElement keyElement = new XElement("key", key);
            parentElement.Add(keyElement);
        }

        protected virtual void AddValueElement(XElement parentElement)
        {
            XElement valueElement = new XElement(ValueTypeName, Value);
            parentElement.Add(valueElement);
        }

        internal virtual void AppendToXml(XElement parentElement)
        {
            AddKeyElement(parentElement);
            AddValueElement(parentElement);
        }
    }
}
