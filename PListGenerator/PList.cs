using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace PListFormatter
{
    public class PList
    {
        //PListElement rootElement = new PListElement();

        public PList()
        {
            this.RootElement = new PListDictionary();
        }

        public static PList New()
        {
            PList generator = new PList();
            return generator;
        }

        public PListElement RootElement { get; private set; }

        public byte[] GetXml()
        {
            XDocument xmlDoc = null;

            XDocumentType docType = new XDocumentType("plist", "-//Apple//DTD PLIST 1.0//EN", "http://www.apple.com/DTDs/PropertyList-1.0.dtd", null);
            xmlDoc = new XDocument(docType);

            XElement rootElement = new XElement("plist");
            rootElement.Add(new XAttribute("version", "1.0"));

            xmlDoc.Add(rootElement);

            this.RootElement.AppendToXml(rootElement);

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
