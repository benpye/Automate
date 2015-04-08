using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DukSharp.BindingGen
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo di = new DirectoryInfo("D:/GitHub/duktape/website/api");
            var fis = di.GetFiles("*.yaml");

            var fileContents = fis.Select(fi => File.ReadAllText(fi.FullName));
            var deserializer = new Deserializer(namingConvention: new CamelCaseNamingConvention());
            var protos = fileContents.Select(f => (deserializer.Deserialize<ExpandoObject>(new StringReader(f)) as IDictionary<string, object>)["proto"].ToString().Replace("{", "").Replace("}", ""));

            PInvokeMethodProcessor pro = new PInvokeMethodProcessor();
            CWrapperMethodProcessor cw = new CWrapperMethodProcessor();

            PrototypeParser gen = new PrototypeParser();

            pro.Class = "duktape";
            pro.Namespace = "DukSharp";
            pro.LibraryName = "duktape";
            gen.Protoypes.AddRange(protos);

            pro.TypeMap.Add("const char *", "IntPtr");
            pro.TypeMap.Add("char *", "IntPtr");
            pro.TypeMap.Add("void *", "IntPtr");
            pro.TypeMap.Add("void", "void");
            pro.TypeMap.Add("duk_size_t", "UIntPtr");
            pro.TypeMap.Add("duk_context *", "SafeContextHandle");
            pro.TypeMap.Add("duk_idx_t", "int");
            pro.TypeMap.Add("duk_bool_t", "bool");
            pro.TypeMap.Add("duk_int_t", "int");
            pro.TypeMap.Add("duk_uint_t", "uint");
            pro.TypeMap.Add("duk_uint32_t", "uint");
            pro.TypeMap.Add("duk_uint16_t", "ushort");
            pro.TypeMap.Add("duk_int32_t", "int");
            pro.TypeMap.Add("duk_int16_t", "short");
            pro.TypeMap.Add("duk_size_t *", "IntPtr");
            pro.TypeMap.Add("duk_codepoint_t", "int");
            pro.TypeMap.Add("duk_uarridx_t", "uint");
            pro.TypeMap.Add("duk_errcode_t", "ErrorCode");
            pro.TypeMap.Add("duk_double_t", "double");
            pro.TypeMap.Add("duk_alloc_function", "duk_alloc_function");
            pro.TypeMap.Add("duk_realloc_function", "duk_realloc_function");
            pro.TypeMap.Add("duk_free_function", "duk_free_function");
            pro.TypeMap.Add("duk_fatal_function", "duk_fatal_function");
            pro.TypeMap.Add("duk_debug_read_function", "duk_debug_read_function");
            pro.TypeMap.Add("duk_debug_write_function", "duk_debug_write_function");
            pro.TypeMap.Add("duk_debug_peek_function", "duk_debug_peek_function");
            pro.TypeMap.Add("duk_debug_read_flush_function", "duk_debug_read_flush_function");
            pro.TypeMap.Add("duk_debug_write_flush_function", "duk_debug_write_flush_function");
            pro.TypeMap.Add("duk_debug_detached_function", "duk_debug_detached_function");
            pro.TypeMap.Add("duk_decode_char_function", "duk_decode_char_function");
            pro.TypeMap.Add("duk_map_char_function", "duk_map_char_function");
            pro.TypeMap.Add("duk_c_function", "duk_c_function");
            pro.TypeMap.Add("duk_safe_call_function", "duk_safe_call_function");
            pro.TypeMap.Add("duk_memory_functions *", "out duk_memory_functions");
            pro.TypeMap.Add("const duk_function_list_entry *", "duk_function_list_entry[]");
            pro.TypeMap.Add("const duk_number_list_entry *", "duk_number_list_entry[]");

            pro.TypeOverride.Add("duk_get_type", new Tuple<string, string>(null, "JSType"));
            pro.TypeOverride.Add("duk_check_type", new Tuple<string, string>("type", "JSType"));
            pro.TypeOverride.Add("duk_get_type_mask", new Tuple<string, string>(null, "TypeMask"));
            pro.TypeOverride.Add("duk_check_type_mask", new Tuple<string, string>("mask", "TypeMask"));
            pro.TypeOverride.Add("duk_to_defaultvalue", new Tuple<string, string>("hint", "CoercionHint"));
            pro.TypeOverride.Add("duk_to_primitive", new Tuple<string, string>("hint", "CoercionHint"));
            pro.TypeOverride.Add("duk_def_prop", new Tuple<string, string>("flags", "PropertyFlags"));
            pro.TypeOverride.Add("duk_enum", new Tuple<string, string>("enum_flags", "EnumFlag"));
            pro.TypeOverride.Add("duk_pcall", new Tuple<string, string>(null, "ReturnCode"));
            pro.TypeOverride.Add("duk_pcall_method", new Tuple<string, string>(null, "ReturnCode"));
            pro.TypeOverride.Add("duk_pcall_prop", new Tuple<string, string>(null, "ReturnCode"));
            pro.TypeOverride.Add("duk_safe_call", new Tuple<string, string>(null, "ReturnCode"));
            pro.TypeOverride.Add("duk_destroy_heap", new Tuple<string, string>("ctx", "IntPtr"));

            pro.ModifyPublicName = a => a.Substring("public_".Length);
            
            cw.CLines.Add("#include \"duktape.h\"\n");
            cw.Prefix = "public_";

            gen.Processors.Add(cw);
            gen.Processors.Add(pro);
            gen.GenerateBinding();
            string csharp = pro.GetOutput();
            string c = cw.GetOutput();
        }
    }
}
