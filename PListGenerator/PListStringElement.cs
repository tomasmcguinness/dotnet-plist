using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PListFormatter
{
    public class PListStringElement : PListElement
    {
        public PListStringElement(string key, string value)
            : base(key, "string", value)
        { }
    }
}
