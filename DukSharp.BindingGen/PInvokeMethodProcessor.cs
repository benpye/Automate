using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syntax = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace DukSharp.BindingGen
{
    public class PInvokeMethodProcessor : IMethodProcessor
    {
        public Dictionary<string, string> TypeMap { get; } = new Dictionary<string, string>();

        public Dictionary<string, Tuple<string, string>> TypeOverride { get; } = new Dictionary<string, Tuple<string, string>>();

        private Dictionary<CType, string> internalTypeMap;

        public List<string> Usings { get; } = new List<string>();

        public string Class { get; set; }
        public string Namespace { get; set; }
        public string LibraryName { get; set; }

        public Func<string, string> ModifyPublicName { get; set; } = new Func<string, string>(a => a);

        private ClassDeclarationSyntax nativeMethodClass;
        private ClassDeclarationSyntax rootClass;

        private void GenerateInternalTypeMap()
        {
            internalTypeMap = new Dictionary<CType, string>();

            foreach (var kv in TypeMap)
            {
                var type = kv.Key;
                internalTypeMap.Add(new CType(type), kv.Value);
            }
        }

        public void Begin()
        {
            GenerateInternalTypeMap();

            rootClass = Syntax.ClassDeclaration(Class)
                .AddModifiers(Syntax.Token(SyntaxKind.PublicKeyword))
                .AddModifiers(Syntax.Token(SyntaxKind.StaticKeyword))
                .AddModifiers(Syntax.Token(SyntaxKind.PartialKeyword));

            nativeMethodClass = Syntax.ClassDeclaration("NativeMethods")
                .AddModifiers(Syntax.Token(SyntaxKind.PrivateKeyword))
                .AddModifiers(Syntax.Token(SyntaxKind.StaticKeyword));
        }

        public Method ProcessMethod(Method method)
        {
            string returnType;
            Dictionary<Argument, string> argTypes = new Dictionary<Argument, string>();

            if(!internalTypeMap.TryGetValue(method.Return.Type, out returnType))
            {
                Console.WriteLine($"Skipping method \"{method.Return.Name}\" due to unknown type \"{method.Return.Type.TypeString}\"");
                return null;
            }

            foreach(var arg in method.Args)
            {
                string type;
                if (!internalTypeMap.TryGetValue(arg.Type, out type))
                {
                    Console.WriteLine($"Skipping method \"{method.Return.Name}\" due to unknown type \"{arg.Type.TypeString}\"");
                    return null;
                }

                argTypes[arg] = type;
            }
            
            var overrides = TypeOverride.Where(o => o.Key == method.Return.Name);

            foreach(var o in overrides)
            {
                if (o.Value.Item1 == null)
                    returnType = o.Value.Item2;
                else
                {
                    for(int i = 0; i < method.Args.Length; i++)
                    {
                        if (method.Args[i].Name == o.Value.Item1)
                            argTypes[method.Args[i]] = o.Value.Item2;
                    }
                }
            }

            var args = method.Args.Select(a => new Tuple<Argument, Tuple<string, string>>(a, new Tuple<string, string>(argTypes[a], a.Name))).ToList();

            MethodDeclarationSyntax ms = Syntax.MethodDeclaration(Syntax.ParseTypeName(returnType), method.Return.Name)
                    .AddModifiers(Syntax.Token(SyntaxKind.StaticKeyword))
                    .AddModifiers(Syntax.Token(SyntaxKind.PublicKeyword))
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
                                        Syntax.LiteralExpression(SyntaxKind.StringLiteralExpression, Syntax.Literal(method.Return.Name)))
                                )
                            )
                        )
                    )
                    .AddParameterListParameters(args.Select(a => Syntax.Parameter(Syntax.Identifier(a.Item1.Name)).WithType(Syntax.ParseTypeName(a.Item2.Item1))).ToArray())
                    .WithSemicolonToken(Syntax.Token(SyntaxKind.SemicolonToken));

            nativeMethodClass = nativeMethodClass.AddMembers(ms);

            List<StatementSyntax> beforeFunc = new List<StatementSyntax>();
            List<StatementSyntax> afterFunc = new List<StatementSyntax>();

            bool isVoid = method.Return.Type.Name == "void" && method.Return.Type.PointerLevel == 0;

            if (method.Args.Where(a => (a.Type.Name == "char" && a.Type.PointerLevel == 1)).Count() > 0)
            {
                for (int i = 0; i < method.Args.Length; i++)
                {
                    var arg = args[i].Item1;
                    if ((arg.Type.Name == "char" && arg.Type.PointerLevel == 1))
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

                        args[i] = new Tuple<Argument, Tuple<string, string>>(arg, new Tuple<string, string>("string", "internalString" + arg.Name));
                    }
                }
            }

            List<StatementSyntax> statements = new List<StatementSyntax>();

            statements.AddRange(beforeFunc);

            if (method.Return.Type.Name == "char" && method.Return.Type.PointerLevel == 1)
            {
                statements.Add(
                    Syntax.LocalDeclarationStatement(
                        Syntax.VariableDeclaration(Syntax.IdentifierName("var"))
                        .WithVariables(Syntax.SingletonSeparatedList<VariableDeclaratorSyntax>(
                            Syntax.VariableDeclarator(Syntax.Identifier("internalReturnValue"))
                            .WithInitializer(
                                Syntax.EqualsValueClause(
                                    Syntax.InvocationExpression(
                                        Syntax.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                                            Syntax.IdentifierName("NativeMethods"), Syntax.IdentifierName(method.Return.Name)),
                                        Syntax.ArgumentList(Syntax.SeparatedList<ArgumentSyntax>(args.Select(a => Syntax.Argument(Syntax.IdentifierName(a.Item2.Item2))))))
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

                returnType = "string";
            }
            else
            {
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
                                                Syntax.IdentifierName("NativeMethods"), Syntax.IdentifierName(method.Return.Name)),
                                            Syntax.ArgumentList(Syntax.SeparatedList<ArgumentSyntax>(args.Select(a => Syntax.Argument(Syntax.IdentifierName(a.Item2.Item2))))))
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
                                    Syntax.IdentifierName("NativeMethods"), Syntax.IdentifierName(method.Return.Name)),
                                Syntax.ArgumentList(Syntax.SeparatedList<ArgumentSyntax>(args.Select(a => Syntax.Argument(Syntax.IdentifierName(a.Item2.Item2)))))
                            )
                        ));
                }
                statements.AddRange(afterFunc);
            }

            if (!isVoid)
            {
                statements.Add(
                    Syntax.ReturnStatement(
                        Syntax.IdentifierName("returnValue")
                    ));
            }

            ms = Syntax.MethodDeclaration(Syntax.ParseTypeName(returnType), ModifyPublicName(method.Return.Name))
                .AddModifiers(Syntax.Token(SyntaxKind.StaticKeyword))
                .AddModifiers(Syntax.Token(SyntaxKind.PublicKeyword))
                .AddParameterListParameters(args.Select(a => Syntax.Parameter(Syntax.Identifier(a.Item1.Name)).WithType(Syntax.ParseTypeName(a.Item2.Item1))).ToArray())
                .AddAttributeLists(
                    Syntax.AttributeList(
                        Syntax.SingletonSeparatedList(
                            Syntax.Attribute(Syntax.ParseName("MethodImpl"))
                            .AddArgumentListArguments(
                                Syntax.AttributeArgument(Syntax.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, Syntax.IdentifierName("MethodImplOptions"), Syntax.IdentifierName("AggressiveInlining")))
                            )
                        )
                    )
                )
                .WithBody(Syntax.Block(statements));

            rootClass = rootClass.AddMembers(ms);

            return method;
        }

        public string GetOutput()
        {
            CompilationUnitSyntax cu = Syntax.CompilationUnit()
                .AddUsings(Syntax.UsingDirective(Syntax.IdentifierName("System")))
                .AddUsings(Syntax.UsingDirective(Syntax.IdentifierName("System.CodeDom.Compiler")))
                .AddUsings(Syntax.UsingDirective(Syntax.IdentifierName("System.Runtime.CompilerServices")))
                .AddUsings(Syntax.UsingDirective(Syntax.IdentifierName("System.Runtime.InteropServices")));

            foreach(var use in Usings)
            {
                cu = cu.AddUsings(Syntax.UsingDirective(Syntax.IdentifierName(use)));
            }

            NamespaceDeclarationSyntax ns = Syntax.NamespaceDeclaration(Syntax.IdentifierName(Namespace));

            rootClass = rootClass.AddAttributeLists(Syntax.AttributeList(Syntax.SingletonSeparatedList<AttributeSyntax>(Syntax.Attribute(Syntax.ParseName("GeneratedCode"))
                                .AddArgumentListArguments(
                                    Syntax.AttributeArgument(Syntax.LiteralExpression(SyntaxKind.StringLiteralExpression, Syntax.Literal("DukSharp.BindingGen"))),
                                    Syntax.AttributeArgument(Syntax.LiteralExpression(SyntaxKind.StringLiteralExpression, Syntax.Literal("1.0")))
                                ))));

            rootClass = rootClass.AddMembers(nativeMethodClass);
            ns = ns.AddMembers(rootClass);
            cu = cu.AddMembers(ns);
            cu = cu.NormalizeWhitespace();

            var sb = new StringBuilder();
            var ws = new AdhocWorkspace();
            SyntaxNode formatted = Formatter.Format(cu, ws);

            return formatted.ToFullString();
        }
    }
}
