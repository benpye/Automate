using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DukSharp.BindingGen
{
    public class Argument
    {
        public Argument(string name, CType ctype)
        {
            Name = name;
            Type = ctype;
        }

        public string Name { get; }
        public CType Type { get; }
    }
}
