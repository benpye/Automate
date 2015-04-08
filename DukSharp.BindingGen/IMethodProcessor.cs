using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DukSharp.BindingGen
{
    interface IMethodProcessor
    {
        void Begin();
        Method ProcessMethod(Method method);
    }
}
