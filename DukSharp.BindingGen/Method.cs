using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DukSharp.BindingGen
{
    public class Method
    {
        public Argument Return { get; }
        public ImmutableArray<Argument> Args { get; }

        public Method(Argument ret, ImmutableArray<Argument> args)
        {
            Return = ret;
            Args = args;
        }
    }
}
