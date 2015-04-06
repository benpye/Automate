using System;

namespace DukSharp
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ScriptMethodAttribute : Attribute
    {
        public string Name { get; }
        public ScriptMethodAttribute(string name = null)
        {
            Name = name;
        }
    }
}

