﻿using System.Xml.Linq;

namespace PListFormatter
{
    public class PListBoolElement : PListElement
    {
        public PListBoolElement(string key, bool value)
            : base(key, value)
        { }

        internal override void AppendToXml(XElement parentElement)
        {
            AddKeyElement(parentElement);
            XElement boolElement = new XElement(value.ToString().ToLower());
            parentElement.Add(boolElement);
        }
    }
}
