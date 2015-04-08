using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using DukSharp.Interop;
using DukSharp.Interop.SafeHandles;

using static DukSharp.Interop.Duktape;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace DukSharp
{
    public class ScriptEngine
    {
        private static duk_fatal_function fatalErrorHandler =
            (ctx, code, msg) =>
            {
                throw new DuktapeException($"Fatal Duktape error ({code}): {MarshalHelper.StringFromUTF8(msg)}");
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
                    var @params = function.GetMethodInfo().GetParameters();

                    object[] args = new object[@params.Length];

                    for(int i = 0; i < @params.Length; i++)
                    {
                        if(@params[i].GetCustomAttribute<CoerceAttribute>() != null)
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
                fs[i].nargs = (function.Value.GetMethodInfo().GetParameters().Length);
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

        public void AddModule(Type module)
        {
            TypeInfo ti = module.GetTypeInfo();
            if (!ti.IsAbstract || !ti.IsSealed)
                throw new ArgumentException($"{nameof(module)} must be a static class type", nameof(module));

            var modAttr = ti.GetCustomAttribute<ScriptModuleAttribute>();
                
            string moduleName = modAttr?.Name;

            if (moduleName == null)
            {
                moduleName = module.Name;

                if (moduleName.EndsWith("Module"))
                    moduleName = moduleName.Substring(0, moduleName.Length - 6);
            }

            IEnumerable<MethodInfo> methods = module.GetRuntimeMethods();

            Dictionary<string, Delegate> scriptFuncs = new Dictionary<string, Delegate>();

            foreach (var method in methods)
            {
                if (!method.IsStatic)
                    continue;

                var scriptAttr = method.GetCustomAttribute<ScriptMethodAttribute>();

                string name = scriptAttr?.Name ?? method.Name;

                List<Type> typeArgs = new List<Type>();
                typeArgs.AddRange(method.GetParameters().Select(p => p.ParameterType));
                typeArgs.Add(method.ReturnType);

                Type delegateType = Expression.GetDelegateType(typeArgs.ToArray());

                scriptFuncs.Add(name, method.CreateDelegate(delegateType));
            }

            AddModule(moduleName, scriptFuncs);
        }

        private void SafeCall(int nargs)
        {
            ReturnCode ret = duk_pcall(context, nargs);

            if (ret == ReturnCode.Error)
            {
                string msg = duk_safe_to_string(context, -1);
                duk_pop(context);
                throw new ScriptExecutionException(msg);
            }
        }

        public void EvalString(string code, string filename = "Unknown")
        {
            duk_push_string(context, code);
            duk_push_string(context, filename);

            int err = duk_pcompile(context, 0);
            
            string msg = "";
            if (err != 0)
            {
                msg = duk_safe_to_string(context, -1);
                duk_pop(context);
                throw new ScriptCompilationError(msg, filename);
            }

            SafeCall(0);
            duk_pop(context);
        }

        private T ExecFunction<T>(string name, object[] args, bool returns)
        {
            duk_push_global_object(context);
            duk_get_prop_string(context, -1, name);

            foreach (var arg in args)
                MarshalToJS(arg);

            SafeCall(args.Length);

            T ret = default(T);

            if(returns)
                ret = (T)MarshalFromJS(typeof(T), -1);

            duk_pop(context);

            return ret;
        }

        public T ExecFunction<T>(string name, params object[] args)
        {
            return ExecFunction<T>(name, args, true);
        }

        public void ExecFunction(string name, params object[] args)
        {
            ExecFunction<object>(name, args, false);
        }
    }
}
