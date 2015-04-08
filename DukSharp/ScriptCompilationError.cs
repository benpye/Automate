using System;

namespace DukSharp
{
    public class ScriptCompilationError : Exception
    {
        public string Filename { get; }
        public ScriptCompilationError(string message, string filename) : base(message)
        {
            Filename = filename;
        }

        public override string ToString()
        {
            return $"[ScriptCompilationError][{Filename}] {Message}";
        }
    }
}

