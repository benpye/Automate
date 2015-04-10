using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DukSharp
{
    internal class DuktapeException : Exception
    {
        public DuktapeException(string message) : base(message)
        {
        }
    }
}
