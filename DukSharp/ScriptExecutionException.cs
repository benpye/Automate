using System;

namespace DukSharp
{
    public class ScriptExecutionException : Exception
    {
        public ScriptExecutionException(string message) : base(message)
        {
        }
    }
}

