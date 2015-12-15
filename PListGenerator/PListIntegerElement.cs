using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PListFormatter
{
    public class PListIntegerElement : PListElement
    {
        public PListIntegerElement(int value)
            : base(null, "integer", value)
        { }

        public PListIntegerElement(string key, int value)
            : base(key, "integer", value)
        { }
    }
}
