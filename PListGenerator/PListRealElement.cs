using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PListFormatter
{
    public class PListRealElement : PListElement
    {
        public PListRealElement(string key, float value)
            : base(key, "real", value)
        { }
    }
}
