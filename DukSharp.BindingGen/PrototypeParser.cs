using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.Options;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syntax = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace DukSharp.BindingGen
{
    internal class PrototypeParser
    {
        public List<string> Protoypes { get; } = new List<string>();

        public List<IMethodProcessor> Processors { get; } = new List<IMethodProcessor>();

        private Argument ParseArgument(string arg)
        {
            string name = "";
            string type = "";

            bool atType = false;

            for (int i = arg.Length - 1; i >= 0; i--)
            {
                char c = arg[i];

                if (atType)
                {
                    type = c + type;
                }
                else
                {
                    if (char.IsWhiteSpace(c))
                        atType = true;
                    else if (c == '*')
                    {
                        type = c + type;
                        atType = true;
                    }
                    else
                    {
                        name = c + name;
                    }
                }
            }

            var ctype = new CType(type);

            return new Argument(name, ctype);
        }

        public Method GetMethodForPrototype(string prototype)
        {
            string proto = prototype.Trim();

            if (proto.EndsWith(");"))
                proto = proto.Substring(0, proto.Length - 2);
            else if (proto.EndsWith(")"))
                proto = proto.Substring(0, proto.Length - 1);

            string argString = "";
            string methodString = "";

            for (int i = proto.Length - 1; i >= 0; i--)
            {
                char c = proto[i];

                if (c != '(')
                    argString = c + argString;
                else
                {
                    methodString = proto.Substring(0, i);
                    break;
                }
            }

            string[] argStrings = argString.Split(',');

            var args = argStrings.Where(a => a.Trim() != "void").Select(a => ParseArgument(a.Trim()));
            var ret = ParseArgument(methodString.Trim());

            return new Method(ret, args.ToImmutableArray());
        }

        public void GenerateBinding()
        {
            foreach (var processor in Processors)
                processor.Begin();

            List<Method> methods = new List<Method>();

            foreach (var prototype in Protoypes)
            {
                Method method = GetMethodForPrototype(prototype);

                if (method.Return == null || method.Args.Contains(null))
                {
                    Console.WriteLine($"Skipping \"{prototype.Trim()}\" due to unknown type");
                    continue;
                }

                methods.Add(method);
            }

            for (int i = 0; i < methods.Count; i++)
            {
                foreach (var processor in Processors)
                {
                    if (methods[i] != null)
                        methods[i] = processor.ProcessMethod(methods[i]);
                }
            }
        }
    }
}
