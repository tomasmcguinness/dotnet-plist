using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PListFormatter
{
    public class PListBoolElement : PListElement
    {
        public PListBoolElement(string key, bool value)
            : base(key, value)
        { }
    }
}
