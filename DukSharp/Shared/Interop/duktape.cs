using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using DukSharp.Interop.SafeHandles;

namespace DukSharp.Interop
{
    // We assume that duktape's duk_int_t and duk_small_int_t are both 32 bits
    // 32 vs 64 bit can be managed by using IntPtr and UIntPtr

    public enum JSType : int
    {
        None = 0,
        Undefined = 1,
        Null = 2,
        Boolean = 3,
        Number = 4,
        String = 5,
        Object = 6,
        Buffer = 7,
        Pointer = 8,
        LightFunc = 9
    }

    public enum TypeMask : uint
    {
        None = 1,
        Undefined = 2,
        Null = 4,
        Boolean = 8,
        Number = 16,
        String = 32,
        Object = 64,
        Buffer = 128,
        Pointer = 256,
        Throw = 1024
    }

    public enum CoercionHint : int
    {
        None = 0,
        String = 1,
        Number = 2
    }

    [Flags]
    public enum EnumFlag : uint
    {
        IncludeNonEnumerable = 1,
        IncludeInternal = 2,
        OwnPropertiesOnly = 4,
        ArrayIndicesOnly = 8,
        SortArrayIndices = 16,
        NoProxyBehaviour = 32
    }

    [Flags]
    public enum CompileFlag : uint
    {
        Eval = 1,
        Function = 2,
        String = 4,
        Safe = 8,
        NoResult = 16,
        NoSource = 32,
        StrLen = 64
    }

    [Flags]
    public enum PropertyFlags : int
    {
        Writable = 1,
        Enumerable = 2,
        Configurable = 4,
        HaveWritable = 8,
        HaveEnumerable = 16,
        HaveConfigurable = 32,
        HaveValue = 64,
        HaveGetter = 128,
        HaveSetter = 256,
        Force = 512
    }

    [Flags]
    public enum ThreadFlag : uint
    {
        NewGlobalEnv = 1
    }

    [Flags]
    public enum StringPushFlag : uint
    {
        Safe = 1
    }

    public enum ErrorCode : int
    {
        None = 0,
        Unimplemented = 50,
        Unsupported = 51,
        Interal = 52,
        Alloc = 53,
        Assertion = 54,
        Api = 55,
        Uncaught = 56,
        Error = 100,
        Eval = 101,
        Range = 102,
        Reference = 103,
        Syntax = 104,
        Type = 105,
        Uri = 106
    }

    public enum ReturnCode : int
    {
        Success = 0,
        Error = 1
    }

    public enum LogLevel : int
    {
        Trace = 0,
        Debug = 1,
        Info = 2,
        Warn = 3,
        Error = 4,
        Fatal = 5
    }

    internal static class duktape
    {
        // These methods don't exactly cover the internal API, for some methods
        // there is no way they can be marshalled, for others the header
        // uses a macro to map the API to another function. Where this is done
        // the function here is typically suffixed _raw and is the function
        // used for several variants of a function, we have to refer to
        // the header to see how this is used

        private const string DLLNAME = "duktape";

        internal enum BufferFlags : uint
        {
            Dynamic = 1,
            NoZero = 2
        }

        [Flags]
        internal enum BufferMode : uint
        {
            Fixed = 0,
            Dynamic = 1,
            DontCare = 2
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int duk_c_function(IntPtr ctx);

        // Memory allocation functions
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr duk_alloc_function(IntPtr udata, UIntPtr size);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr duk_realloc_function(IntPtr udata, IntPtr ptr, UIntPtr size);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void duk_free_function(IntPtr udata, IntPtr ptr);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void duk_fatal_function(IntPtr ctx, ErrorCode code, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string msg);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void duk_decode_chat_function(IntPtr udata, int codepoint);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int duk_map_char_function(IntPtr udata, int codepoint);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int duk_safe_call_function(IntPtr ctx);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate UIntPtr duk_debug_read_function(IntPtr udata, IntPtr Buffer, UIntPtr length);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate UIntPtr duk_debug_write_function(IntPtr udata, IntPtr Buffer, UIntPtr length);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate UIntPtr duk_debug_peek_function(IntPtr udata);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void duk_debug_read_flush_function(IntPtr udata);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void duk_debug_write_flush_function(IntPtr udata);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void duk_debug_detached_function(IntPtr udata);

        [StructLayout(LayoutKind.Sequential)]
        internal struct duk_memory_functions
        {
            internal duk_alloc_function alloc_func;
            internal duk_realloc_function realloc_func;
            internal duk_free_function free_func;
            internal IntPtr udata;
        }

        // IntPtr key is a UTF8 string, use MarshalHelper.StringToUTF8

        [StructLayout(LayoutKind.Sequential)]
        internal struct duk_function_list_entry
        {
            internal IntPtr key;
            internal duk_c_function value;
            internal int nargs;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct duk_number_list_entry
        {
            internal IntPtr key;
            internal double value;
        }

        // Context management

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern SafeContextHandle duk_create_heap(
            duk_alloc_function alloc_func,
            duk_realloc_function realloc_func,
            duk_free_function free_func,
            IntPtr heap_udata,
            duk_fatal_function fatal_handler);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_destroy_heap(IntPtr ctx);

        // Memory management

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr duk_alloc_raw(SafeContextHandle ctx, UIntPtr size);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_free_raw(SafeContextHandle ctx, IntPtr ptr);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr duk_realloc_raw(SafeContextHandle ctx, IntPtr ptr, UIntPtr size);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr duk_alloc(SafeContextHandle ctx, UIntPtr size);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_free(SafeContextHandle ctx, UIntPtr size);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr duk_realloc(SafeContextHandle ctx, IntPtr ptr, UIntPtr size);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_get_memory_functions(SafeContextHandle ctx, out duk_memory_functions out_funcs);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_gc(SafeContextHandle ctx, uint flags);

        // Error handling

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_throw(SafeContextHandle ctx);

        // Nasty undocumented hacks to call a vararg
        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_error_raw(SafeContextHandle ctx, ErrorCode err_code, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string filename, int line, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string fmt, __arglist);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_fatal(SafeContextHandle ctx, ErrorCode err_code, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string err_msg);

        // Other state related functions

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_is_strict_call(SafeContextHandle ctx);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_is_constructor_call(SafeContextHandle ctx);

        // Stack management

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int duk_normalize_index(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int duk_require_normalize_index(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_is_valid_index(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_require_valid_index(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int duk_get_top(SafeContextHandle ctx);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_set_top(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int duk_get_top_index(SafeContextHandle ctx);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int duk_require_top_index(SafeContextHandle ctx);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_check_stack(SafeContextHandle ctx, int extra);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_require_stack(SafeContextHandle ctx, int extra);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_check_stack_top(SafeContextHandle ctx, int top);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_require_stack_top(SafeContextHandle ctx, int top);

        // Stack manipulation

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_swap(SafeContextHandle ctx, int index1, int index2);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_swap_top(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_dup(SafeContextHandle ctx, int from_index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_dup_top(SafeContextHandle ctx);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_insert(SafeContextHandle ctx, int to_index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_replace(SafeContextHandle ctx, int to_index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_copy(SafeContextHandle ctx, int from_index, int to_index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_remove(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_xcopymove_raw(IntPtr to_ctx, IntPtr from_ctx, int count, bool is_copy);

        // Push operations

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_push_undefined(SafeContextHandle ctx);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_push_null(SafeContextHandle ctx);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_push_boolean(SafeContextHandle ctx, bool val);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_push_true(SafeContextHandle ctx);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_push_false(SafeContextHandle ctx);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_push_number(SafeContextHandle ctx, double val);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_push_nan(SafeContextHandle ctx);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_push_int(SafeContextHandle ctx, int val);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_push_uint(SafeContextHandle ctx, uint val);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8ReturnMarshaler))]
        internal static extern string duk_push_string(SafeContextHandle ctx, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string str);

        // TODO: Better way to marshal lstring type methods, do we need them>
        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr duk_push_lstring(SafeContextHandle ctx, IntPtr str, UIntPtr size);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_push_pointer(SafeContextHandle ctx, IntPtr p);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8ReturnMarshaler))]
        internal static extern string duk_push_sprintf(SafeContextHandle ctx, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string fmt, __arglist);

        // Unimplentable, last arg is va_list
        //[DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        //internal static extern IntPtr duk_push_vsprintf(SafeContextHandle ctx, IntPtr fmt, )

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8ReturnMarshaler))]
        internal static extern string duk_push_string_file_raw(SafeContextHandle ctx, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string path, StringPushFlag flags);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_push_this(SafeContextHandle ctx);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_push_current_function(SafeContextHandle ctx);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_push_current_thread(SafeContextHandle ctx);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_push_global_object(SafeContextHandle ctx);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_push_heap_stash(SafeContextHandle ctx);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_push_global_stash(SafeContextHandle ctx);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_push_thread_stash(SafeContextHandle ctx, IntPtr target_ctx);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int duk_push_object(SafeContextHandle ctx);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int duk_push_array(SafeContextHandle ctx);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int duk_push_c_function(SafeContextHandle ctx, duk_c_function func, int nargs);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int duk_push_c_lightfunc(SafeContextHandle ctx, duk_c_function func, int nargs, int length, int magic);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int duk_push_thread_raw(SafeContextHandle ctx, ThreadFlag flags);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int duk_push_error_object_raw(SafeContextHandle ctx, ErrorCode err_code, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string filename, int line, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string fmt, __arglist);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr duk_push_buffer_raw(SafeContextHandle ctx, UIntPtr size, BufferFlags flags);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int duk_push_heapptr(SafeContextHandle ctx, IntPtr ptr);

        // Pop operations

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_pop(SafeContextHandle ctx);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_push_n(SafeContextHandle ctx, int count);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_pop_2(SafeContextHandle ctx);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_pop_3(SafeContextHandle ctx);

        // Type checks

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern JSType duk_get_type(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_check_type(SafeContextHandle ctx, int index, JSType type);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern TypeMask duk_get_type_mask(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_check_type_mask(SafeContextHandle ctx, int index, TypeMask mask);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_is_undefined(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_is_null(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_is_null_or_undefined(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_is_boolean(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_is_number(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_is_nan(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_is_string(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_is_object(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_is_buffer(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_is_pointer(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_is_array(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_is_function(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_is_lightfunc(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_is_c_function(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_is_ecmascript_function(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_is_bound_function(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_is_thread(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_is_callable(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_is_dynamic_buffer(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_is_fixed_buffer(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_is_primitive(SafeContextHandle ctx, int index);

        // Get operations

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern ErrorCode duk_get_error_code(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_get_boolean(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern double duk_get_number(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int duk_get_int(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint duk_get_uint(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8ReturnMarshaler))]
        internal static extern string duk_get_string(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr duk_get_lstring(SafeContextHandle ctx, int index, UIntPtr out_len);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr duk_get_buffer(SafeContextHandle ctx, int index, UIntPtr out_size);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr duk_get_pointer(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern duk_c_function duk_get_c_function(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr duk_get_context(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr duk_get_heapptr(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern UIntPtr duk_get_length(SafeContextHandle ctx, int index);

        // Require operations

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_require_undefined(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_require_null(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_require_boolean(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern double duk_require_number(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int duk_require_int(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint duk_require_uint(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8ReturnMarshaler))]
        internal static extern string duk_require_string(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr duk_require_lstring(SafeContextHandle ctx, int index, UIntPtr out_len);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr duk_require_buffer(SafeContextHandle ctx, int index, UIntPtr out_size);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr duk_require_pointer(SafeContextHandle ctxc, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern duk_c_function duk_require_c_function(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr duk_require_context(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr duk_require_heapptr(SafeContextHandle ctx, int index);

        // Coercion operations

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_to_undefined(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_to_null(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_to_boolean(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern double duk_to_number(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int duk_to_int(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint duk_to_uint(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int duk_to_int32(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint duk_to_uint32(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern UInt16 duk_to_uint16(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8ReturnMarshaler))]
        internal static extern string duk_to_string(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr duk_to_lstring(SafeContextHandle ctx, int index, UIntPtr out_len);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr duk_to_buffer_raw(SafeContextHandle ctx, int index, UIntPtr out_size, BufferMode flags);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr duk_to_pointer(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_to_object(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_to_defaultvalue(SafeContextHandle ctx, int index, CoercionHint hint);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_to_primitive(SafeContextHandle ctx, int index, CoercionHint hint);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr duk_safe_to_lstring(SafeContextHandle ctx, int index, UIntPtr out_len);

        // Misc conversion

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8ReturnMarshaler))]
        internal static extern string duk_base64_encode(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_base64_decode(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8ReturnMarshaler))]
        internal static extern string duk_hex_encode(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_hex_decode(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8ReturnMarshaler))]
        internal static extern string duk_json_encode(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_json_decode(SafeContextHandle ctx, int index);

        // Buffer

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr duk_resize_buffer(SafeContextHandle ctx, int index, UIntPtr new_size);

        // Property access

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_get_prop(SafeContextHandle ctx, int obj_index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_get_prop_string(SafeContextHandle ctx, int obj_index, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string key);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_put_prop(SafeContextHandle ctx, int obj_index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_put_prop_string(SafeContextHandle ctx, int obj_index, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string key);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_put_prop_index(SafeContextHandle ctx, int obj_index, uint arr_index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_del_prop(SafeContextHandle ctx, int obj_index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_del_prop_string(SafeContextHandle ctx, int obj_index, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string key);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_del_prop_index(SafeContextHandle ctx, int obj_index, uint arr_index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_has_prop(SafeContextHandle ctx, int obj_index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_has_prop_string(SafeContextHandle ctx, int obj_index, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string key);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_has_prop_index(SafeContextHandle ctx, int obj_index, uint arr_index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_def_prop(SafeContextHandle ctx, int obj_index, PropertyFlags flags);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_get_global_string(SafeContextHandle ctx, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string key);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_put_global_string(SafeContextHandle ctx, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string key);

        // Object prototype

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_get_prototype(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_set_prototype(SafeContextHandle ctx, int index);

        // Object finalizer

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_get_finalizer(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_put_finalizer(SafeContextHandle ctx, int index);

        // Global object

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_set_global_object(SafeContextHandle ctx);

        // Duktape/C function magic value

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int duk_get_magic(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_set_magic(SafeContextHandle ctx, int index, int magic);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int duk_get_current_magic(SafeContextHandle ctx);

        // Module helpers: put multiple function or constant properties

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_put_function_list(SafeContextHandle ctx, int obj_index, duk_function_list_entry[] funcs);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_put_number_list(SafeContextHandle ctx, int obj_index, duk_number_list_entry[] numbers);

        // Variable access

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_get_var(SafeContextHandle ctx);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_put_var(SafeContextHandle ctx);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_del_var(SafeContextHandle ctx);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_has_var(SafeContextHandle ctx);

        // Object operations

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_compact(SafeContextHandle ctx, int obj_index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_enum(SafeContextHandle ctx, int obj_index, EnumFlag enum_flags);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_next(SafeContextHandle ctx, int enum_index, bool get_value);

        // String manipulation

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_concat(SafeContextHandle ctx, int count);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_join(SafeContextHandle ctx, int count);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_decode_string(SafeContextHandle ctx, int index, duk_decode_chat_function callback, IntPtr udata);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_map_string(SafeContextHandle ctx, int index, duk_map_char_function callback, IntPtr udata);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_substring(SafeContextHandle ctx, int index, UIntPtr start_char_offset, UIntPtr end_char_offset);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_trim(SafeContextHandle ctx, int index);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int duk_char_code_at(SafeContextHandle ctx, int index, UIntPtr char_offset);

        // Ecmascript operators

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_equals(SafeContextHandle ctx, int index1, int index2);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool duk_strict_equals(SafeContextHandle ctx, int index1, int index2);

        // Function (method) calls

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_call(SafeContextHandle ctx, int nargs);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_call_method(SafeContextHandle ctx, int nargs);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_call_prop(SafeContextHandle ctx, int obj_index, int nargs);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern ReturnCode duk_pcall(SafeContextHandle ctx, int nargs);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern ReturnCode duk_pcall_method(SafeContextHandle ctx, int nrags);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern ReturnCode duk_pcall_prop(SafeContextHandle ctx, int obj_index, int nargs);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_new(SafeContextHandle ctx, int nargs);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern ReturnCode duk_safe_call(SafeContextHandle ctx, duk_safe_call_function func, int nargs, int nrets);

        // Compilations and evaluation

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int duk_eval_raw(SafeContextHandle ctx, IntPtr src_buffer, UIntPtr src_length, CompileFlag flags);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int duk_compile_raw(SafeContextHandle ctx, IntPtr src_buffer, UIntPtr src_length, CompileFlag flags);

        // Logging

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_log(SafeContextHandle ctx, LogLevel level, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string fmt, __arglist);

        // Debugging

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_push_context_dump(SafeContextHandle ctx);

        // Debugger (debug protocol)

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_debugger_attach(SafeContextHandle ctx, duk_debug_read_function read_cb, duk_debug_write_function write_cb, duk_debug_peek_function peek_cb, duk_debug_read_flush_function read_flush_cb, duk_debug_write_flush_function write_flush_cb, duk_debug_detached_function detached_cb, IntPtr udata);
        
        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_debugger_detatch(SafeContextHandle ctx);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void duk_debugger_cooperate(SafeContextHandle ctx);
    }
}
