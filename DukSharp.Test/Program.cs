using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DukSharp.Test
{
    public static class TestModule
    {
        [ScriptMethod("Print")]
        public static void HelloWorld([Coerce] string test)
        {
            Console.WriteLine(test);
        }

        public static void TestNumbers(sbyte sb, byte b, short s, ushort us, int i, uint ui, long l, ulong ul, float f, double d, decimal de)
        {
            Console.WriteLine($"{sb} {b} {s} {us} {i} {ui} {l} {ul} {f} {d} {de}");
        }

        [ScriptMethod("TestObject")]
        public static void HelloWorld2(List<string> test)
        {
            Console.WriteLine(test.ToString());
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            ScriptEngine se = new ScriptEngine();
            se.AddModule(typeof(TestModule));
            se.EvalString(@"
            Test.Print('Duktape version is: ' + Duktape.version);

            Test.TestNumbers(42.5, 42.5, 42.5, 42.5, 42.5, 42.5, 42.5, 42.5, 42.5, 42.5);

            function testFunc(s1, s2, obj) {
                Test.Print(s1);
                Test.Print(s2);
                Test.Print(obj);
            }

            function testFunc2() {
                return ['This', 'Is', 'A', 'Test'];
            }
            ");
            se.ExecFunction("testFunc", "Hello", "World", new List<String> { "This", "Is", "An", "Object" });
            var test = se.ExecFunction<string[]>("testFunc2");
            Console.ReadLine();
        }
    }
}
