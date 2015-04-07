using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DukSharp.BindingGen
{
    public class Argument
    {
        public Argument(string name, string typeString, CType ctype, string netType)
        {
            Name = name;
            TypeString = typeString;
            CType = ctype;
            NetType = netType;
        }

        public string Name { get; }
        public string TypeString { get; }
        public CType CType { get; }
        public string NetType { get; }
    }
}
