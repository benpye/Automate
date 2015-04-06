using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DukSharp.Interop;
using DukSharp.Interop.SafeHandles;

using static DukSharp.Interop.duktape;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace DukSharp
{
    public class ScriptEngine
    {
        private static duk_fatal_function fatalErrorHandler =
            (IntPtr ctx, ErrorCode code, string msg) =>
            {
                throw new DuktapeException($"Fatal Duktape error ({code}): {msg}");
            };

        private SafeContextHandle context;
        private List<Delegate> delegateCache = new List<Delegate>();

        private static HashSet<Type> numericTypes = new HashSet<Type>
            {
                typeof(sbyte), typeof(byte), typeof(short), typeof(ushort), typeof(int),
                typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double),
                typeof(decimal)
            };

        public ScriptEngine()
        {
            context = duk_create_heap(null, null, null, IntPtr.Zero, fatalErrorHandler);

            if (context.IsInvalid)
                throw new DuktapeException("Failed to create Duktape context");
        }

        private object MarshalFromJS(Type t, int i)
        {
            if (duk_is_null_or_undefined(context, i))
                return null;
            else if (t == typeof(bool))
                return duk_require_boolean(context, i);
            else if (t == typeof(string))
                return duk_require_string(context, i);
            else if (numericTypes.Contains(t))
                return Convert.ChangeType(duk_require_number(context, i), t);
            else
            {
                var s = duk_json_encode(context, i);
                return JsonConvert.DeserializeObject(s, t);
            }
        }

        private object MarshalFromJSCoerce(Type t, int i)
        {
            if (duk_is_null_or_undefined(context, i))
                return null;
            else if (t == typeof(bool))
                return duk_to_boolean(context, i);
            else if (t == typeof(string))
                return duk_to_string(context, i);
            else if (numericTypes.Contains(t))
                return Convert.ChangeType(duk_to_number(context, i), t);
            else
            {
                var s = duk_json_encode(context, i);
                return JsonConvert.DeserializeObject(s, t);
            }
        }

        private int MarshalToJS(object o)
        {
            if (o == null)
                return 0;

            Type t = o.GetType();

            if (t == typeof(bool))
                duk_push_boolean(context, (bool)o);
            else if (t == typeof(string))
                duk_push_string(context, (string)o);
            else if (numericTypes.Contains(t))
                duk_push_number(context, (double)o);
            else
            {
                duk_push_string(context, JsonConvert.SerializeObject(o));
                duk_json_decode(context, -1);
            }

            return 1;
        }

        private duk_c_function WrapFunction(Delegate function)
        {
            var f = new duk_c_function((IntPtr ctx) =>
                {
                    var @params = function.Method.GetParameters();

                    object[] args = new object[@params.Length];

                    for(int i = 0; i < @params.Length; i++)
                    {
                        if(@params[i].GetCustomAttributes(typeof(CoerceAttribute), false).Length != 0)
                            args[i] = MarshalFromJSCoerce(@params[i].ParameterType, i);
                        else
                            args[i] = MarshalFromJS(@params[i].ParameterType, i);
                    }

                    return MarshalToJS(function.DynamicInvoke(args));
                });

            delegateCache.Add(f);

            return f;
        }

        public void AddModule(string name, Dictionary<string, Delegate> functions)
        {
            duk_function_list_entry[] fs = new duk_function_list_entry[functions.Count + 1];

            int i = 0;
            foreach(var function in functions)
            {
                fs[i].key = MarshalHelper.StringToUTF8(function.Key);
                fs[i].value = WrapFunction(function.Value);
                fs[i].nargs = (function.Value.Method.GetParameters().Length);
                i++;
            }

            fs[i].key = IntPtr.Zero;
            fs[i].value = null;
            fs[i].nargs = 0;

            duk_push_global_object(context);
            duk_push_object(context);
            duk_put_function_list(context, -1, fs);
            duk_put_prop_string(context, -2, name);
            duk_pop(context);

            for (i = 0; i < functions.Count; i++)
                Marshal.FreeHGlobal(fs[i].key);
        }

        public void EvalString(string code, string filename = "Unspecified")
        {
            duk_push_string(context, filename);
            IntPtr c = MarshalHelper.StringToUTF8(code);
            int err = duk_eval_raw(context, c, UIntPtr.Zero, CompileFlag.Eval | CompileFlag.StrLen | CompileFlag.Safe | CompileFlag.NoSource);
            Marshal.FreeHGlobal(c);
            string msg = "";
            if (err != 0)
            {
                IntPtr str = duk_safe_to_lstring(context, -1, UIntPtr.Zero);
                msg = MarshalHelper.StringFromUTF8(str);
            }
            duk_pop(context);

            if (err != 0)
                throw new DuktapeException($"Evaluation failed: {msg}");
        }

        private T ExecFunctionInternal<T>(string name, object[] args, bool returns)
        {
            duk_push_global_object(context);
            duk_get_prop_string(context, -1, name);

            ReturnCode rc;

            foreach (var arg in args)
                MarshalToJS(arg);
            
            rc = duk_pcall(context, args.Length);

            if (rc == ReturnCode.Error)
            {
                IntPtr str = duk_safe_to_lstring(context, -1, UIntPtr.Zero);
                string msg = MarshalHelper.StringFromUTF8(str);
                duk_pop(context);
                throw new DuktapeException($"Execution failed: {msg}");
            }

            T ret = default(T);

            if(returns)
                ret = (T)MarshalFromJS(typeof(T), -1);

            duk_pop(context);

            return ret;
        }

        public T ExecFunction<T>(string name, params object[] args)
        {
            return ExecFunctionInternal<T>(name, args, true);
        }

        public void ExecFunction(string name, params object[] args)
        {
            ExecFunctionInternal<object>(name, args, false);
        }
    }
}
