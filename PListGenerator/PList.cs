using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace PListFormatter
{
    public class PList
    {
        public PList()
        {
            this.RootDictionary = new PListDictionary();
        }

        public PList(Stream stream)
        {
            this.RootDictionary = new PListDictionary();
            Load(stream, false);
        }

        public PList(string xml)
        {
            this.RootDictionary = new PListDictionary();
            Load(Encoding.UTF8.GetBytes(xml), false);
        }

        public PList(Stream stream, bool isBinary)
        {
            this.RootDictionary = new PListDictionary();
            Load(stream, isBinary);
        }

        public PList(byte[] contents)
        {
            this.RootDictionary = new PListDictionary();
            Load(contents, false);
        }

        public PList(byte[] contents, bool isBinary)
        {
            this.RootDictionary = new PListDictionary();
            Load(contents, isBinary);
        }

        private void Load(byte[] contents, bool isBinary)
        {
            using (var stream = new MemoryStream(contents, false))
            {
                Load(stream, isBinary);
            }
        }

        private void Load(Stream stream, bool isBinary)
        {
            if (isBinary)
            {
                ParseBinary(this.RootDictionary, stream);
            }
            else
            {
                XDocument doc = XDocument.Load(stream);
                XElement plist = doc.Element("plist");
                XElement dict = plist.Element("dict");

                var dictElements = dict.Elements();
                ParseDictionary(this.RootDictionary, dictElements);
            }
        }

        public PListDictionary RootDictionary { get; private set; }

        public dynamic this[string index]
        {
            get
            {
                dynamic value = RootDictionary[index];
                return value;
            }
        }

        public bool ContainsKey(string key)
        {
            return RootDictionary.ContainsKey(key);
        }

        private void ParseDictionary(PListDictionary dict, IEnumerable<XElement> elements)
        {
            for (int i = 0; i < elements.Count(); i += 2)
            {
                XElement key = elements.ElementAt(i);
                XElement val = elements.ElementAt(i + 1);

                PListElement element = CreateElement(key, val);

                if (element == null)
                {
                    throw new InvalidOperationException("Key " + key.Value + " has no value");
                }

                dict.Elements.Add(key.Value, element);
            }
        }
        private void ParseBinary(PListDictionary dict, Stream stream)
        {
            using (BinaryReader reader = new BinaryReader(stream))
            {
                stream.Position = 0;
                int bpli = reader.ReadInt32();
                int version = reader.ReadInt32();

                // Read the trailer.
                // The first six bytes of the first eight-byte block are unused, so offset by 26 instead of 32.
                stream.Position = stream.Length - 26;
                //this.offsetIntSize = 
                reader.ReadByte();
                //this.objectRefSize = 
                reader.ReadByte();
                int objectCount = (int)reader.ReadInt64();
                //this.topLevelObjectOffset = (int)reader.ReadInt64().ToBigEndianConditional();
                //this.offsetTableOffset = (int)reader.ReadInt64().ToBigEndianConditional();
                //int offsetTableSize = this.offsetIntSize * this.objectCount;

            }
        }

        private void ParseArray(PListArray array, IEnumerable<XElement> elements)
        {
            for (int i = 0; i < elements.Count(); i++)
            {
                XElement key = elements.ElementAt(i);

                array.Elements.Add(CreateElement(null, key));
            }
        }

        private PListElement CreateElement(XElement key, XElement val)
        {
            PListElement element = null;

            switch (val.Name.ToString())
            {
                case "date":
                    if (key == null)
                    {
                        element = new PListDateElement(DateTime.Parse(val.Value));
                    }
                    else
                    {
                        element = new PListDateElement(key.Value, DateTime.Parse(val.Value));
                    }
                    break;
                case "data":
                    if (key == null)
                    {
                        element = new PListDataElement(val.Value);
                    }
                    else
                    {
                        element = new PListDataElement(key.Value, val.Value);
                    }
                    break;
                case "string":
                    if (key == null)
                    {
                        element = new PListStringElement(val.Value);
                    }
                    else
                    {
                        element = new PListStringElement(key.Value, val.Value);
                    }
                    break;
                case "integer":
                    if (key == null)
                    {
                        element = new PListIntegerElement(long.Parse(val.Value));
                    }
                    else
                    {
                        element = new PListIntegerElement(key.Value, long.Parse(val.Value));
                    }
                    break;
                case "real":
                    element = new PListRealElement(key.Value, float.Parse(val.Value));
                    break;
                case "true":
                    element = new PListBoolElement(key.Value, true);
                    break;
                case "false":
                    element = new PListBoolElement(key.Value, false);
                    break;
                case "dict":
                    if (key == null)
                    {
                        element = new PListDictionary();
                    }
                    else
                    {
                        element = new PListDictionary(key.Value);
                    }
                    ParseDictionary((PListDictionary)element, val.Elements());
                    break;
                case "array":
                    element = new PListArray(key.Value);
                    ParseArray((PListArray)element, val.Elements());
                    break;
            }

            return element;
        }

        public byte[] GetXml()
        {
            return GetXml(false);
        }

        public byte[] GetXml(bool indent)
        {
            XDocument xmlDoc = null;

            XDocumentType docType = new XDocumentType("plist", "-//Apple//DTD PLIST 1.0//EN", "http://www.apple.com/DTDs/PropertyList-1.0.dtd", null);
            xmlDoc = new XDocument(docType);

            XElement rootElement = new XElement("plist");
            rootElement.Add(new XAttribute("version", "1.0"));

            xmlDoc.Add(rootElement);

            this.RootDictionary.AppendToXml(rootElement);

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

        public string GetString()
        {
            return Encoding.UTF8.GetString(this.GetXml());
        }

        public static string NewUUID()
        {
            return Guid.NewGuid().ToString().ToUpper();
        }
    }
}
