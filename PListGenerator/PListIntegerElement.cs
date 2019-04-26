using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PListFormatter
{
    public class PListIntegerElement : PListElement
    {
        public PListIntegerElement(long value)
            : base(null, "integer", value)
        { }

        public PListIntegerElement(string key, long value)
            : base(key, "integer", value)
        { }
    }
}
