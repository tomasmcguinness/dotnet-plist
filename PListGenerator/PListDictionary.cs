﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PListGenerator
{
    public class PListDictionary : PListElement<PListDictionary>
    {
        public PListDictionary(XElement parentElement)
        {
            this.rootElement = new XElement("dict");
            parentElement.Add(this.rootElement);
        }

        public void AddData(string p1, string p2)
        {
            throw new NotImplementedException();
        }
    }
}
