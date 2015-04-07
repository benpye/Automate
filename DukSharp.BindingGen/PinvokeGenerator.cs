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
    class PinvokeGenerator
    {
        public string Namespace { get; set; }

        public string Class { get; set; }

        public string LibraryName { get; set; }

        public List<string> Protoypes { get; } = new List<string>();

        public List<string> CLines { get; } = new List<string>();

        public Dictionary<string, string> TypeMap { get; } = new Dictionary<string, string>();

        public Dictionary<Tuple<string, string>, string> TypeOverride { get; } = new Dictionary<Tuple<string, string>, string>();

        public bool CWrapper { get; set; }

        private Dictionary<CType, string> internalTypeMap;

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

            string netType;

            var ctype = new CType(type.Replace("*", "").Replace("const", "").Trim(), type.Where(c => c == '*').Count(), type.Contains("const"));

            if (!internalTypeMap.TryGetValue(ctype, out netType))
            {
                Console.WriteLine($"Unknown type \"{type}\" (Argument \"{arg}\")");
                netType = "UnknownType";
                return null;
            }

            return new Argument(name, type, ctype, netType);
        }

        private void GenerateInternalTypeMap()
        {
            internalTypeMap = new Dictionary<CType, string>();

            foreach (var kv in TypeMap)
            {
                var type = kv.Key;
                internalTypeMap.Add(new CType(type.Replace("*", "").Replace("const", "").Trim(), type.Where(c => c == '*').Count(), type.Contains("const")), kv.Value);
            }
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

        private string GetWrapperForFunction(Method method)
        {
            var sb = new StringBuilder();

            sb.Append($"WRAPPED_EXPORT_CALL {method.Return.TypeString} wrapped_{method.Return.Name}(");

            for (int i = 0; i < method.Args.Length; i++)
            {
                var arg = method.Args[i];
                sb.Append(arg.TypeString);
                sb.Append(' ');
                sb.Append(arg.Name);

                if (i + 1 != method.Args.Length)
                    sb.Append(", ");
            }

            sb.Append(")\n{\n\t");

            if (!(method.Return.CType.Name == "void" && method.Return.CType.PointerLevel == 0))
                sb.Append("return ");

            sb.Append(method.Return.Name);
            sb.Append('(');
            for (int i = 0; i < method.Args.Length; i++)
            {
                var arg = method.Args[i];
                sb.Append(arg.Name);
                if (i + 1 != method.Args.Length)
                    sb.Append(", ");
            }
            sb.Append(");\n}\n");

            return sb.ToString();
        }

        private string GetCPreamble()
        {
            var sb = new StringBuilder();
            sb.Append(string.Join("\n", CLines));
            sb.Append(@"// AUTOGENERATED CODE: DO NOT EDIT. YOUR CHANGES WILL BE LOST.
#ifdef _WIN32
#define WRAPPED_EXPORT_CALL __declspec(dllexport)
#else
#define WRAPPED_EXPORT_CALL
#endif

");
            return sb.ToString();
        }

        public string GetMethodName(string method)
        {
            if (CWrapper)
                return "wrap_" + method;
            else
                return method;
        }

        public void GenerateBinding(out string csharpstring, out string cstring)
        {
            GenerateInternalTypeMap();

            var cBuilder = new StringBuilder();

            if (CWrapper)
                cBuilder.Append(GetCPreamble());

            CompilationUnitSyntax cu = Syntax.CompilationUnit()
                .AddUsings(Syntax.UsingDirective(Syntax.IdentifierName("System")))
                .AddUsings(Syntax.UsingDirective(Syntax.IdentifierName("System.Runtime.InteropServices")));

            NamespaceDeclarationSyntax ns = Syntax.NamespaceDeclaration(Syntax.IdentifierName(Namespace));

            ClassDeclarationSyntax rootClass = Syntax.ClassDeclaration(Class)
                .AddModifiers(Syntax.Token(SyntaxKind.PublicKeyword))
                .AddModifiers(Syntax.Token(SyntaxKind.StaticKeyword))
                .AddModifiers(Syntax.Token(SyntaxKind.PartialKeyword));

            ClassDeclarationSyntax nativeMethods = Syntax.ClassDeclaration("NativeMethods")
                .AddModifiers(Syntax.Token(SyntaxKind.PrivateKeyword))
                .AddModifiers(Syntax.Token(SyntaxKind.StaticKeyword));

            List<Method> methods = new List<Method>();

            foreach (var prototype in Protoypes)
            {
                Method method = GetMethodForPrototype(prototype);

                if (method.Return == null || method.Args.Contains(null))
                {
                    Console.WriteLine($"Skipping \"{prototype.Trim()}\" due to unknown type");
                    continue;
                }

                if (CWrapper)
                {
                    cBuilder.Append(GetWrapperForFunction(method));
                    cBuilder.Append('\n');
                }

                methods.Add(method);
            }

            foreach(var over in TypeOverride)
            {
                int midx = methods.FindIndex(m => m.Return.Name == over.Key.Item1);
                if (midx == -1) continue;

                if(over.Key.Item2 != null)
                {
                    var args = methods[midx].Args.ToArray();
                    for (int i = 0; i < args.Length; i++)
                    {
                        var arg = args[i];
                        if (arg.Name == over.Key.Item2)
                        {
                            args[i] = new Argument(arg.Name, arg.TypeString, arg.CType, over.Value);
                        }
                    }

                    methods[midx] = new Method(methods[midx].Return, ImmutableArray.Create(args));
                }
                else
                {
                    var ret = methods[midx].Return;
                    methods[midx] = new Method(new Argument(ret.Name, ret.TypeString, ret.CType, over.Value), methods[midx].Args);
                }
            }

            foreach(var method in methods)
            {
                MethodDeclarationSyntax ms = Syntax.MethodDeclaration(Syntax.ParseTypeName(method.Return.NetType), GetMethodName(method.Return.Name))
                    .AddModifiers(Syntax.Token(SyntaxKind.StaticKeyword))
                    .AddModifiers(Syntax.Token(SyntaxKind.PrivateKeyword))
                    .AddModifiers(Syntax.Token(SyntaxKind.ExternKeyword))
                    .AddAttributeLists(
                        Syntax.AttributeList(
                            Syntax.SingletonSeparatedList(
                                Syntax.Attribute(Syntax.ParseName("DllImport"))
                                .AddArgumentListArguments(
                                    Syntax.AttributeArgument(Syntax.LiteralExpression(SyntaxKind.StringLiteralExpression, Syntax.Literal(LibraryName))),
                                    Syntax.AttributeArgument(Syntax.NameEquals("CallingConvention"), default(NameColonSyntax),
                                        Syntax.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, Syntax.IdentifierName("CallingConvention"), Syntax.IdentifierName("Cdecl"))),
                                    Syntax.AttributeArgument(Syntax.NameEquals("EntryPoint"), default(NameColonSyntax),
                                        Syntax.LiteralExpression(SyntaxKind.StringLiteralExpression, Syntax.Literal(GetMethodName(method.Return.Name))))
                                )
                            )
                        )
                    )
                    .AddParameterListParameters(method.Args.Select(a => Syntax.Parameter(Syntax.Identifier(a.Name)).WithType(Syntax.ParseTypeName(a.NetType))).ToArray())
                    .WithSemicolonToken(Syntax.Token(SyntaxKind.SemicolonToken));

                nativeMethods = nativeMethods.AddMembers(ms);

                var args = method.Args.ToList();

                List<StatementSyntax> beforeFunc = new List<StatementSyntax>();
                List<StatementSyntax> afterFunc = new List<StatementSyntax>();

                bool isVoid = method.Return.CType.Name == "void" && method.Return.CType.PointerLevel == 0;

                if (args.Where(a => (a.CType.Name == "char" && a.CType.PointerLevel == 1)).Count() > 0)
                {
                    for (int i = 0; i < args.Count; i++)
                    {
                        var arg = args[i];
                        if ((arg.CType.Name == "char" && arg.CType.PointerLevel == 1))
                        {
                            beforeFunc.Add(Syntax.LocalDeclarationStatement(
                                    Syntax.VariableDeclaration(Syntax.IdentifierName("var"))
                                    .WithVariables(Syntax.SingletonSeparatedList<VariableDeclaratorSyntax>(
                                        Syntax.VariableDeclarator(Syntax.Identifier("internalString" + arg.Name))
                                        .WithInitializer(
                                            Syntax.EqualsValueClause(
                                                Syntax.InvocationExpression(
                                                    Syntax.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                                                        Syntax.IdentifierName("MarshalHelper"), Syntax.IdentifierName("StringToUTF8")),
                                                    Syntax.ArgumentList(Syntax.SingletonSeparatedList<ArgumentSyntax>(Syntax.Argument(Syntax.IdentifierName(arg.Name)))
                                                    )
                                                )
                                            )
                                        )
                                    )
                                )));

                            afterFunc.Add(
                                Syntax.ExpressionStatement(
                                    Syntax.InvocationExpression(
                                        Syntax.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                                            Syntax.IdentifierName("Marshal"), Syntax.IdentifierName("FreeHGlobal")),
                                        Syntax.ArgumentList(Syntax.SingletonSeparatedList<ArgumentSyntax>(Syntax.Argument(Syntax.IdentifierName("internalString" + arg.Name))))
                                    )
                                ));

                            args[i] = new Argument("internalString" + arg.Name, arg.TypeString, arg.CType, "string");
                        }
                    }
                }

                if (method.Return.CType.Name == "char" && method.Return.CType.PointerLevel == 1)
                {
                    List<StatementSyntax> statements = new List<StatementSyntax>();
                    statements.AddRange(beforeFunc);
                    statements.Add(
                        Syntax.LocalDeclarationStatement(
                            Syntax.VariableDeclaration(Syntax.IdentifierName("var"))
                            .WithVariables(Syntax.SingletonSeparatedList<VariableDeclaratorSyntax>(
                                Syntax.VariableDeclarator(Syntax.Identifier("internalReturnValue"))
                                .WithInitializer(
                                    Syntax.EqualsValueClause(
                                        Syntax.InvocationExpression(
                                            Syntax.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                                                Syntax.IdentifierName("NativeMethods"), Syntax.IdentifierName(GetMethodName(method.Return.Name))),
                                            Syntax.ArgumentList(Syntax.SeparatedList<ArgumentSyntax>(args.Select(a => Syntax.Argument(Syntax.IdentifierName(a.Name))))))
                                        )
                                    )
                                )
                            )
                        ));
                    statements.AddRange(afterFunc);
                    statements.Add(
                        Syntax.LocalDeclarationStatement(
                            Syntax.VariableDeclaration(Syntax.IdentifierName("var"))
                            .WithVariables(Syntax.SingletonSeparatedList<VariableDeclaratorSyntax>(
                                Syntax.VariableDeclarator(Syntax.Identifier("returnValue"))
                                .WithInitializer(
                                    Syntax.EqualsValueClause(
                                        Syntax.InvocationExpression(
                                            Syntax.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                                                Syntax.IdentifierName("MarshalHelper"), Syntax.IdentifierName("StringFromUTF8")),
                                            Syntax.ArgumentList(Syntax.SingletonSeparatedList<ArgumentSyntax>(Syntax.Argument(Syntax.IdentifierName("internalReturnValue")))))
                                        )
                                    )
                                )
                            )
                        ));

                    statements.Add(
                        Syntax.ReturnStatement(
                            Syntax.IdentifierName("returnValue")
                        ));

                    ms = Syntax.MethodDeclaration(Syntax.ParseTypeName("string"), method.Return.Name)
                        .AddModifiers(Syntax.Token(SyntaxKind.StaticKeyword))
                        .AddModifiers(Syntax.Token(SyntaxKind.PublicKeyword))
                        .AddParameterListParameters(method.Args.Select(a => Syntax.Parameter(Syntax.Identifier(a.Name)).WithType(Syntax.ParseTypeName(a.NetType))).ToArray())
                        .WithBody(Syntax.Block(statements));
                }
                else
                {
                    List<StatementSyntax> statements = new List<StatementSyntax>();
                    statements.AddRange(beforeFunc);
                    if (!isVoid)
                    {
                        statements.Add(
                            Syntax.LocalDeclarationStatement(
                                Syntax.VariableDeclaration(Syntax.IdentifierName("var"))
                                .WithVariables(Syntax.SingletonSeparatedList<VariableDeclaratorSyntax>(
                                    Syntax.VariableDeclarator(Syntax.Identifier("returnValue"))
                                    .WithInitializer(
                                        Syntax.EqualsValueClause(
                                            Syntax.InvocationExpression(
                                                Syntax.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                                                    Syntax.IdentifierName("NativeMethods"), Syntax.IdentifierName(GetMethodName(method.Return.Name))),
                                                Syntax.ArgumentList(Syntax.SeparatedList<ArgumentSyntax>(args.Select(a => Syntax.Argument(Syntax.IdentifierName(a.Name))))))
                                            )
                                        )
                                    )
                                )
                            ));
                    }
                    else
                    {
                        statements.Add(
                            Syntax.ExpressionStatement(
                                Syntax.InvocationExpression(
                                    Syntax.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                                        Syntax.IdentifierName("NativeMethods"), Syntax.IdentifierName(GetMethodName(method.Return.Name))),
                                    Syntax.ArgumentList(Syntax.SeparatedList<ArgumentSyntax>(args.Select(a => Syntax.Argument(Syntax.IdentifierName(a.Name)))))
                                )
                            ));
                    }
                    statements.AddRange(afterFunc);
                    if (!isVoid)
                    {
                        statements.Add(
                            Syntax.ReturnStatement(
                                Syntax.IdentifierName("returnValue")
                            ));
                    }



                    ms = Syntax.MethodDeclaration(Syntax.ParseTypeName(method.Return.NetType), method.Return.Name)
                        .AddModifiers(Syntax.Token(SyntaxKind.StaticKeyword))
                        .AddModifiers(Syntax.Token(SyntaxKind.PublicKeyword))
                        .AddParameterListParameters(method.Args.Select(a => Syntax.Parameter(Syntax.Identifier(a.Name)).WithType(Syntax.ParseTypeName(a.NetType))).ToArray())
                        .WithBody(Syntax.Block(statements));
                }

                rootClass = rootClass.AddMembers(ms);
            }

            rootClass = rootClass.AddMembers(nativeMethods);
            ns = ns.AddMembers(rootClass);
            cu = cu.AddMembers(ns);
            cu = cu.NormalizeWhitespace();

            var sb = new StringBuilder();
            var ws = new AdhocWorkspace();
            SyntaxNode formatted = Formatter.Format(cu, ws);

            csharpstring = formatted.ToFullString();

            if (CWrapper)
                cstring = cBuilder.ToString();
            else
                cstring = "";
        }
    }
}
