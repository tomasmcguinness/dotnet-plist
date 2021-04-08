using System;
using System.Text;
using System.Xml.Linq;

namespace PListFormatter
{
    public class PListDateElement : PListElement
    {
        public PListDateElement(string key, DateTime value)
            : base(key, "date", value)
        { }

        public PListDateElement(DateTime value) :
            base(null, "date", value)
        {
        }

        internal override void AppendToXml(XElement parentElement)
        {
            AddKeyElement(parentElement);

            // Fix the date output.
            //
            string formattedDate = ((DateTime)value).ToString("yyyy-MM-dd'T'hh:mm:ss'Z'");
            XElement dateElement = new XElement("date", formattedDate);
            parentElement.Add(dateElement);
        }
    }
}
