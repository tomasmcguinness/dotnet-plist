using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PListGenerator
{
    public abstract class PListElement<T> where T : class
    {
        protected XElement rootElement;

        public PListDictionary AddDictionary(string key)
        {
            AddKey(key);
            PListDictionary dictionary = new PListDictionary(rootElement);
            return dictionary;
        }

        public PListDictionary AddDictionary()
        {
            PListDictionary dictionary = new PListDictionary(rootElement);
            return dictionary;
        }

        public PListArray AddArray(string key)
        {
            AddKey(key);
            PListArray array = new PListArray(rootElement);
            return array;
        }

        public T AddString(string value)
        {
            AddValue("string", value);

            return this as T;
        }

        public T AddString(string key, string value)
        {
            AddKey(key);
            AddValue("string", value);

            return this as T;
        }

        public T AddBool(string key)
        {
            return AddBool(key, false);
        }

        public T AddBool(string key, bool value)
        {
            AddKey(key);

            XElement boolElement = new XElement(value.ToString().ToLower());
            rootElement.Add(boolElement);

            return this as T;
        }

        public T AddInteger(string key, int value)
        {
            AddKey(key);
            AddValue("integer", value);

            return this as T;
        }

        public T AddData(string key, object value)
        {
            AddKey(key);
            AddValue("data", "\n" + Encoding.ASCII.GetString((byte[])value) + "\n");

            return this as T;
        }

        private void AddKey(string key)
        {
            XElement keyElement = new XElement("key", key);
            rootElement.Add(keyElement);
        }

        private void AddValue(string name, object value)
        {
            XElement valueElement = new XElement(name, value);
            rootElement.Add(valueElement);
        }
    }
}
