using System;

namespace DukSharp
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ScriptModuleAttribute : Attribute
    {
        public string Name { get; }
        public ScriptModuleAttribute(string name = null)
        {
            Name = name;
        }
    }
}

