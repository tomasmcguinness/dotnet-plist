using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PListGenerator
{
    public class PListArray : PListElement<PListArray>
    {
        public PListArray(System.Xml.Linq.XElement parentElement)
        {
            this.rootElement = new XElement("array");
            parentElement.Add(this.rootElement);
        }
    }
}
