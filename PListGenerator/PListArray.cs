using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PListFormatter
{
    public class PListArray : PListElement, IEnumerable<PListElement>
    {
        public PListArray(string key)
            : base(key)
        {
            this.Elements = new List<PListElement>();
        }

        public List<PListElement> Elements { get; private set; }

        public IEnumerator<PListElement> GetEnumerator()
        {
            return Elements.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Elements.GetEnumerator();
        }
    }
}
