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

    public static partial class Duktape
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int duk_c_function(IntPtr ctx);

        // Memory allocation functions
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr duk_alloc_function(IntPtr udata, UIntPtr size);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr duk_realloc_function(IntPtr udata, IntPtr ptr, UIntPtr size);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void duk_free_function(IntPtr udata, IntPtr ptr);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void duk_fatal_function(IntPtr ctx, ErrorCode code, IntPtr msg);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void duk_decode_char_function(IntPtr udata, int codepoint);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int duk_map_char_function(IntPtr udata, int codepoint);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int duk_safe_call_function(IntPtr ctx);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate UIntPtr duk_debug_read_function(IntPtr udata, IntPtr Buffer, UIntPtr length);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate UIntPtr duk_debug_write_function(IntPtr udata, IntPtr Buffer, UIntPtr length);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate UIntPtr duk_debug_peek_function(IntPtr udata);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void duk_debug_read_flush_function(IntPtr udata);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void duk_debug_write_flush_function(IntPtr udata);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void duk_debug_detached_function(IntPtr udata);

        [StructLayout(LayoutKind.Sequential)]
        public struct duk_memory_functions
        {
            public duk_alloc_function alloc_func;
            public duk_realloc_function realloc_func;
            public duk_free_function free_func;
            public IntPtr udata;
        }

        // IntPtr key is a UTF8 string, use MarshalHelper.StringToUTF8

        [StructLayout(LayoutKind.Sequential)]
        public struct duk_function_list_entry
        {
            public IntPtr key;
            public duk_c_function value;
            public int nargs;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct duk_number_list_entry
        {
            public IntPtr key;
            public double value;
        }
    }
}
