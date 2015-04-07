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

            PinvokeGenerator gen = new PinvokeGenerator();
            gen.Class = "duktape";
            gen.Namespace = "DukSharp";
            gen.LibraryName = "duktape";
            gen.Protoypes.AddRange(protos);

            gen.TypeMap.Add("const char *", "IntPtr");
            gen.TypeMap.Add("char *", "IntPtr");
            gen.TypeMap.Add("void *", "IntPtr");
            gen.TypeMap.Add("void", "void");
            gen.TypeMap.Add("duk_size_t", "UIntPtr");
            gen.TypeMap.Add("duk_context *", "SafeContextHandle");
            gen.TypeMap.Add("duk_idx_t", "int");
            gen.TypeMap.Add("duk_bool_t", "bool");
            gen.TypeMap.Add("duk_int_t", "int");
            gen.TypeMap.Add("duk_uint_t", "uint");
            gen.TypeMap.Add("duk_uint32_t", "uint");
            gen.TypeMap.Add("duk_uint16_t", "ushort");
            gen.TypeMap.Add("duk_int32_t", "int");
            gen.TypeMap.Add("duk_int16_t", "short");
            gen.TypeMap.Add("duk_size_t *", "IntPtr");
            gen.TypeMap.Add("duk_codepoint_t", "int");
            gen.TypeMap.Add("duk_uarridx_t", "uint");
            gen.TypeMap.Add("duk_errcode_t", "ErrorCode");
            gen.TypeMap.Add("duk_double_t", "double");
            gen.TypeMap.Add("duk_alloc_function", "duk_alloc_function");
            gen.TypeMap.Add("duk_realloc_function", "duk_realloc_function");
            gen.TypeMap.Add("duk_free_function", "duk_free_function");
            gen.TypeMap.Add("duk_fatal_function", "duk_fatal_function");
            gen.TypeMap.Add("duk_debug_read_function", "duk_debug_read_function");
            gen.TypeMap.Add("duk_debug_write_function", "duk_debug_write_function");
            gen.TypeMap.Add("duk_debug_peek_function", "duk_debug_peek_function");
            gen.TypeMap.Add("duk_debug_read_flush_function", "duk_debug_read_flush_function");
            gen.TypeMap.Add("duk_debug_write_flush_function", "duk_debug_write_flush_function");
            gen.TypeMap.Add("duk_debug_detached_function", "duk_debug_detached_function");
            gen.TypeMap.Add("duk_decode_char_function", "duk_decode_char_function");
            gen.TypeMap.Add("duk_map_char_function", "duk_map_char_function");
            gen.TypeMap.Add("duk_c_function", "duk_c_function");
            gen.TypeMap.Add("duk_safe_call_function", "duk_safe_call_function");
            gen.TypeMap.Add("duk_memory_functions *", "out duk_memory_functions");
            gen.TypeMap.Add("const duk_function_list_entry *", "duk_function_list_entry[]");
            gen.TypeMap.Add("const duk_number_list_entry *", "duk_number_list_entry[]");

            gen.TypeOverride.Add(new Tuple<string, string>("duk_get_type", null), "JSType");
            gen.TypeOverride.Add(new Tuple<string, string>("duk_check_type", "type"), "JSType");
            gen.TypeOverride.Add(new Tuple<string, string>("duk_get_type_mask", null), "TypeMask");
            gen.TypeOverride.Add(new Tuple<string, string>("duk_check_type_mask", "mask"), "TypeMask");
            gen.TypeOverride.Add(new Tuple<string, string>("duk_to_defaultvalue", "hint"), "CoercionHint");
            gen.TypeOverride.Add(new Tuple<string, string>("duk_to_primitive", "hint"), "CoercionHint");
            gen.TypeOverride.Add(new Tuple<string, string>("duk_def_prop", "flags"), "PropertyFlags");
            gen.TypeOverride.Add(new Tuple<string, string>("duk_enum", "enum_flags"), "EnumFlag");
            gen.TypeOverride.Add(new Tuple<string, string>("duk_pcall", null), "ReturnCode");
            gen.TypeOverride.Add(new Tuple<string, string>("duk_pcall_method", null), "ReturnCode");
            gen.TypeOverride.Add(new Tuple<string, string>("duk_pcall_prop", null), "ReturnCode");
            gen.TypeOverride.Add(new Tuple<string, string>("duk_safe_call", null), "ReturnCode");
            
            gen.CWrapper = true;

            string c;
            string cs;

            gen.GenerateBinding(out cs, out c);
        }
    }
}
