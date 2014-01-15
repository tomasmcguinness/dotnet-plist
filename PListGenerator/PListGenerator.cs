using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PListGenerator
{
    public class PListGenerator : PListElement<PListGenerator>
    {
        XDocument xmlDoc = null;

        public PListGenerator()
        {
            XDocumentType docType = new XDocumentType("plist", "-//Apple Computer//DTD PLIST 1.0//EN", "http://www.apple.com/DTDs/PropertyList-1.0.dtd", null);
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
            using (MemoryStream ms = new MemoryStream())
            {
                xmlDoc.Save(ms, SaveOptions.DisableFormatting);
                return ms.ToArray();
            }
        }
    }
}
