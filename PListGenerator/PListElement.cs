using System;
using System.IO;
using System.Text;
using System.Xml;
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
            set
            {
                key = value;
            }
        }

        public virtual object Value
        {
            get
            {
                return value;
            }
        }

        public void Add(PListElement element)
        {
            this.AddElement(element);
        }

        public PListDictionary AddDictionary(string key)
        {
            PListDictionary dictionary = new PListDictionary(key);
            AddElement(dictionary);
            return dictionary;
        }

        public PListDictionary AddDictionary(string key, PListDictionary dictionary)
        {
            dictionary.Key = key;
            AddElement(dictionary);
            return dictionary;
        }

        public PListDictionary AddDictionary()
        {
            PListDictionary dictionary = new PListDictionary();
            AddElement(dictionary);
            return dictionary;
        }

        public PListArray AddArray()
        {
            PListArray array = new PListArray();
            AddElement(array);
            return array;
        }

        public PListArray AddArray(string key)
        {
            PListArray array = new PListArray(key);
            AddElement(array);
            return array;
        }

        public PListElement AddString(string value)
        {
            PListStringElement element = new PListStringElement(value);
            AddElement(element);
            return this;
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
            return AddData(key, value, true);
        }

        public PListElement AddData(string key, object value, bool encode)
        {
            PListDataElement element = new PListDataElement(key, value);
            element.Encode = encode;
            AddElement(element);
            return this;
        }

        public PListElement AddData(string key, string value)
        {
            return AddData(key, value, true);
        }

        public PListElement AddData(string key, string value, bool encode)
        {
            PListDataElement element = new PListDataElement(key, value);
            element.Encode = encode;
            AddElement(element);
            return this;
        }

        public bool AsBool()
        {
            return (bool)this.value;
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

        public byte[] GetXml()
        {
            XDocument xmlDoc = null;

            XDocumentType docType = new XDocumentType("plist", "-//Apple//DTD PLIST 1.0//EN", "http://www.apple.com/DTDs/PropertyList-1.0.dtd", null);
            xmlDoc = new XDocument(docType);

            XElement rootElement = new XElement("plist");
            rootElement.Add(new XAttribute("version", "1.0"));

            xmlDoc.Add(rootElement);

            this.AppendToXml(rootElement);

            // Corrects an issue with the XML generation where a [] is being inserted into the DOCTYPE.
            //
            if (xmlDoc.DocumentType != null)
            {
                xmlDoc.DocumentType.InternalSubset = null;
            }

            XmlWriterSettings xws = new XmlWriterSettings();
            xws.OmitXmlDeclaration = false;
            xws.NewLineHandling = NewLineHandling.None;
            xws.Indent = true;
            xws.Encoding = new System.Text.UTF8Encoding(false);
            xws.ConformanceLevel = ConformanceLevel.Document;

            using (MemoryStream ms = new MemoryStream())
            {
                using (XmlWriter writer = XmlWriter.Create(ms, xws))
                {
                    xmlDoc.Save(writer);
                }

                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return Encoding.UTF8.GetString(this.GetXml());
        }
    }
}
