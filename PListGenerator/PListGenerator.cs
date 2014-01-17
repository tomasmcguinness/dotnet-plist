using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace PListGenerator
{
    public class PListGenerator : PListElement<PListGenerator>
    {
        XDocument xmlDoc = null;

        public PListGenerator()
        {
            XDocumentType docType = new XDocumentType("plist", "-//Apple//DTD PLIST 1.0//EN", "http://www.apple.com/DTDs/PropertyList-1.0.dtd", null);
            xmlDoc = new XDocument(docType);

            this.rootElement = new XElement("plist");
            this.rootElement.Add(new XAttribute("version", "1.0"));

            xmlDoc.Add(this.rootElement);
        }

        public static PListGenerator New()
        {
            PListGenerator generator = new PListGenerator();
            return generator;
        }

        public byte[] GetXml()
        {
            // Corrects an issue with the XML generation where a [] is being inserted into the DOCTYPE.
            //
            if (xmlDoc.DocumentType != null)
            {
                xmlDoc.DocumentType.InternalSubset = null;
            }

            XmlWriterSettings xws = new XmlWriterSettings();
            xws.OmitXmlDeclaration = false;
            xws.IndentChars = "\t";
            //xws.NewLineChars = "\n";
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
    }
}
