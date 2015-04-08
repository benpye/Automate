using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using DukSharp.Interop.SafeHandles;

namespace DukSharp.Interop
{
    [GeneratedCode("DukSharp.BindingGen", "1.0")]
    public static partial class Duktape
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public IntPtr duk_alloc(SafeContextHandle ctx, UIntPtr size)
        {
            var returnValue = NativeMethods.public_duk_alloc(ctx, size);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public IntPtr duk_alloc_raw(SafeContextHandle ctx, UIntPtr size)
        {
            var returnValue = NativeMethods.public_duk_alloc_raw(ctx, size);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_base64_decode(SafeContextHandle ctx, int index)
        {
            NativeMethods.public_duk_base64_decode(ctx, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public string duk_base64_encode(SafeContextHandle ctx, int index)
        {
            var internalReturnValue = NativeMethods.public_duk_base64_encode(ctx, index);
            var returnValue = MarshalHelper.StringFromUTF8(internalReturnValue);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_call(SafeContextHandle ctx, int nargs)
        {
            NativeMethods.public_duk_call(ctx, nargs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_call_method(SafeContextHandle ctx, int nargs)
        {
            NativeMethods.public_duk_call_method(ctx, nargs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_call_prop(SafeContextHandle ctx, int obj_index, int nargs)
        {
            NativeMethods.public_duk_call_prop(ctx, obj_index, nargs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_char_code_at(SafeContextHandle ctx, int index, UIntPtr char_offset)
        {
            var returnValue = NativeMethods.public_duk_char_code_at(ctx, index, char_offset);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_check_stack(SafeContextHandle ctx, int extra)
        {
            var returnValue = NativeMethods.public_duk_check_stack(ctx, extra);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_check_stack_top(SafeContextHandle ctx, int top)
        {
            var returnValue = NativeMethods.public_duk_check_stack_top(ctx, top);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_check_type(SafeContextHandle ctx, int index, JSType type)
        {
            var returnValue = NativeMethods.public_duk_check_type(ctx, index, type);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_check_type_mask(SafeContextHandle ctx, int index, TypeMask mask)
        {
            var returnValue = NativeMethods.public_duk_check_type_mask(ctx, index, mask);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_compact(SafeContextHandle ctx, int obj_index)
        {
            NativeMethods.public_duk_compact(ctx, obj_index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_compile(SafeContextHandle ctx, uint flags)
        {
            NativeMethods.public_duk_compile(ctx, flags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_compile_file(SafeContextHandle ctx, uint flags, string path)
        {
            var internalStringpath = MarshalHelper.StringToUTF8(path);
            NativeMethods.public_duk_compile_file(ctx, flags, internalStringpath);
            Marshal.FreeHGlobal(internalStringpath);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_compile_lstring(SafeContextHandle ctx, uint flags, string src, UIntPtr len)
        {
            var internalStringsrc = MarshalHelper.StringToUTF8(src);
            NativeMethods.public_duk_compile_lstring(ctx, flags, internalStringsrc, len);
            Marshal.FreeHGlobal(internalStringsrc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_compile_lstring_filename(SafeContextHandle ctx, uint flags, string src, UIntPtr len)
        {
            var internalStringsrc = MarshalHelper.StringToUTF8(src);
            NativeMethods.public_duk_compile_lstring_filename(ctx, flags, internalStringsrc, len);
            Marshal.FreeHGlobal(internalStringsrc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_compile_string(SafeContextHandle ctx, uint flags, string src)
        {
            var internalStringsrc = MarshalHelper.StringToUTF8(src);
            NativeMethods.public_duk_compile_string(ctx, flags, internalStringsrc);
            Marshal.FreeHGlobal(internalStringsrc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_compile_string_filename(SafeContextHandle ctx, uint flags, string src)
        {
            var internalStringsrc = MarshalHelper.StringToUTF8(src);
            NativeMethods.public_duk_compile_string_filename(ctx, flags, internalStringsrc);
            Marshal.FreeHGlobal(internalStringsrc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_concat(SafeContextHandle ctx, int count)
        {
            NativeMethods.public_duk_concat(ctx, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_copy(SafeContextHandle ctx, int from_index, int to_index)
        {
            NativeMethods.public_duk_copy(ctx, from_index, to_index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public SafeContextHandle duk_create_heap(duk_alloc_function alloc_func, duk_realloc_function realloc_func, duk_free_function free_func, IntPtr heap_udata, duk_fatal_function fatal_handler)
        {
            var returnValue = NativeMethods.public_duk_create_heap(alloc_func, realloc_func, free_func, heap_udata, fatal_handler);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public SafeContextHandle duk_create_heap_default()
        {
            var returnValue = NativeMethods.public_duk_create_heap_default();
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_debugger_attach(SafeContextHandle ctx, duk_debug_read_function read_cb, duk_debug_write_function write_cb, duk_debug_peek_function peek_cb, duk_debug_read_flush_function read_flush_cb, duk_debug_write_flush_function write_flush_cb, duk_debug_detached_function detached_cb, IntPtr udata)
        {
            NativeMethods.public_duk_debugger_attach(ctx, read_cb, write_cb, peek_cb, read_flush_cb, write_flush_cb, detached_cb, udata);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_debugger_cooperate(SafeContextHandle ctx)
        {
            NativeMethods.public_duk_debugger_cooperate(ctx);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_debugger_detach(SafeContextHandle ctx)
        {
            NativeMethods.public_duk_debugger_detach(ctx);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_decode_string(SafeContextHandle ctx, int index, duk_decode_char_function callback, IntPtr udata)
        {
            NativeMethods.public_duk_decode_string(ctx, index, callback, udata);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_def_prop(SafeContextHandle ctx, int obj_index, PropertyFlags flags)
        {
            NativeMethods.public_duk_def_prop(ctx, obj_index, flags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_del_prop(SafeContextHandle ctx, int obj_index)
        {
            var returnValue = NativeMethods.public_duk_del_prop(ctx, obj_index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_del_prop_index(SafeContextHandle ctx, int obj_index, uint arr_index)
        {
            var returnValue = NativeMethods.public_duk_del_prop_index(ctx, obj_index, arr_index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_del_prop_string(SafeContextHandle ctx, int obj_index, string key)
        {
            var internalStringkey = MarshalHelper.StringToUTF8(key);
            var returnValue = NativeMethods.public_duk_del_prop_string(ctx, obj_index, internalStringkey);
            Marshal.FreeHGlobal(internalStringkey);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_del_var(SafeContextHandle ctx)
        {
            var returnValue = NativeMethods.public_duk_del_var(ctx);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_destroy_heap(IntPtr ctx)
        {
            NativeMethods.public_duk_destroy_heap(ctx);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_dump_context_stderr(SafeContextHandle ctx)
        {
            NativeMethods.public_duk_dump_context_stderr(ctx);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_dump_context_stdout(SafeContextHandle ctx)
        {
            NativeMethods.public_duk_dump_context_stdout(ctx);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_dup(SafeContextHandle ctx, int from_index)
        {
            NativeMethods.public_duk_dup(ctx, from_index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_dup_top(SafeContextHandle ctx)
        {
            NativeMethods.public_duk_dup_top(ctx);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_enum(SafeContextHandle ctx, int obj_index, EnumFlag enum_flags)
        {
            NativeMethods.public_duk_enum(ctx, obj_index, enum_flags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_equals(SafeContextHandle ctx, int index1, int index2)
        {
            var returnValue = NativeMethods.public_duk_equals(ctx, index1, index2);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_eval(SafeContextHandle ctx)
        {
            NativeMethods.public_duk_eval(ctx);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_eval_file(SafeContextHandle ctx, string path)
        {
            var internalStringpath = MarshalHelper.StringToUTF8(path);
            NativeMethods.public_duk_eval_file(ctx, internalStringpath);
            Marshal.FreeHGlobal(internalStringpath);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_eval_file_noresult(SafeContextHandle ctx, string path)
        {
            var internalStringpath = MarshalHelper.StringToUTF8(path);
            NativeMethods.public_duk_eval_file_noresult(ctx, internalStringpath);
            Marshal.FreeHGlobal(internalStringpath);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_eval_lstring(SafeContextHandle ctx, string src, UIntPtr len)
        {
            var internalStringsrc = MarshalHelper.StringToUTF8(src);
            NativeMethods.public_duk_eval_lstring(ctx, internalStringsrc, len);
            Marshal.FreeHGlobal(internalStringsrc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_eval_lstring_noresult(SafeContextHandle ctx, string src, UIntPtr len)
        {
            var internalStringsrc = MarshalHelper.StringToUTF8(src);
            NativeMethods.public_duk_eval_lstring_noresult(ctx, internalStringsrc, len);
            Marshal.FreeHGlobal(internalStringsrc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_eval_noresult(SafeContextHandle ctx)
        {
            NativeMethods.public_duk_eval_noresult(ctx);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_eval_string(SafeContextHandle ctx, string src)
        {
            var internalStringsrc = MarshalHelper.StringToUTF8(src);
            NativeMethods.public_duk_eval_string(ctx, internalStringsrc);
            Marshal.FreeHGlobal(internalStringsrc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_eval_string_noresult(SafeContextHandle ctx, string src)
        {
            var internalStringsrc = MarshalHelper.StringToUTF8(src);
            NativeMethods.public_duk_eval_string_noresult(ctx, internalStringsrc);
            Marshal.FreeHGlobal(internalStringsrc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_fatal(SafeContextHandle ctx, ErrorCode err_code, string err_msg)
        {
            var internalStringerr_msg = MarshalHelper.StringToUTF8(err_msg);
            NativeMethods.public_duk_fatal(ctx, err_code, internalStringerr_msg);
            Marshal.FreeHGlobal(internalStringerr_msg);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_free(SafeContextHandle ctx, IntPtr ptr)
        {
            NativeMethods.public_duk_free(ctx, ptr);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_free_raw(SafeContextHandle ctx, IntPtr ptr)
        {
            NativeMethods.public_duk_free_raw(ctx, ptr);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_gc(SafeContextHandle ctx, uint flags)
        {
            NativeMethods.public_duk_gc(ctx, flags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_get_boolean(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_get_boolean(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public IntPtr duk_get_buffer(SafeContextHandle ctx, int index, IntPtr out_size)
        {
            var returnValue = NativeMethods.public_duk_get_buffer(ctx, index, out_size);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public SafeContextHandle duk_get_context(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_get_context(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_get_current_magic(SafeContextHandle ctx)
        {
            var returnValue = NativeMethods.public_duk_get_current_magic(ctx);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public duk_c_function duk_get_c_function(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_get_c_function(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public ErrorCode duk_get_error_code(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_get_error_code(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_get_finalizer(SafeContextHandle ctx, int index)
        {
            NativeMethods.public_duk_get_finalizer(ctx, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_get_global_string(SafeContextHandle ctx, string key)
        {
            var internalStringkey = MarshalHelper.StringToUTF8(key);
            var returnValue = NativeMethods.public_duk_get_global_string(ctx, internalStringkey);
            Marshal.FreeHGlobal(internalStringkey);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public IntPtr duk_get_heapptr(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_get_heapptr(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_get_int(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_get_int(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public UIntPtr duk_get_length(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_get_length(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public string duk_get_lstring(SafeContextHandle ctx, int index, IntPtr out_len)
        {
            var internalReturnValue = NativeMethods.public_duk_get_lstring(ctx, index, out_len);
            var returnValue = MarshalHelper.StringFromUTF8(internalReturnValue);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_get_magic(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_get_magic(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_get_memory_functions(SafeContextHandle ctx, out duk_memory_functions out_funcs)
        {
            NativeMethods.public_duk_get_memory_functions(ctx, out out_funcs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public double duk_get_number(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_get_number(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public IntPtr duk_get_pointer(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_get_pointer(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_get_prop(SafeContextHandle ctx, int obj_index)
        {
            var returnValue = NativeMethods.public_duk_get_prop(ctx, obj_index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_get_prop_index(SafeContextHandle ctx, int obj_index, uint arr_index)
        {
            var returnValue = NativeMethods.public_duk_get_prop_index(ctx, obj_index, arr_index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_get_prop_string(SafeContextHandle ctx, int obj_index, string key)
        {
            var internalStringkey = MarshalHelper.StringToUTF8(key);
            var returnValue = NativeMethods.public_duk_get_prop_string(ctx, obj_index, internalStringkey);
            Marshal.FreeHGlobal(internalStringkey);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_get_prototype(SafeContextHandle ctx, int index)
        {
            NativeMethods.public_duk_get_prototype(ctx, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public string duk_get_string(SafeContextHandle ctx, int index)
        {
            var internalReturnValue = NativeMethods.public_duk_get_string(ctx, index);
            var returnValue = MarshalHelper.StringFromUTF8(internalReturnValue);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_get_top(SafeContextHandle ctx)
        {
            var returnValue = NativeMethods.public_duk_get_top(ctx);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_get_top_index(SafeContextHandle ctx)
        {
            var returnValue = NativeMethods.public_duk_get_top_index(ctx);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public JSType duk_get_type(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_get_type(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public TypeMask duk_get_type_mask(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_get_type_mask(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public uint duk_get_uint(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_get_uint(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_get_var(SafeContextHandle ctx)
        {
            NativeMethods.public_duk_get_var(ctx);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_has_prop(SafeContextHandle ctx, int obj_index)
        {
            var returnValue = NativeMethods.public_duk_has_prop(ctx, obj_index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_has_prop_index(SafeContextHandle ctx, int obj_index, uint arr_index)
        {
            var returnValue = NativeMethods.public_duk_has_prop_index(ctx, obj_index, arr_index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_has_prop_string(SafeContextHandle ctx, int obj_index, string key)
        {
            var internalStringkey = MarshalHelper.StringToUTF8(key);
            var returnValue = NativeMethods.public_duk_has_prop_string(ctx, obj_index, internalStringkey);
            Marshal.FreeHGlobal(internalStringkey);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_has_var(SafeContextHandle ctx)
        {
            var returnValue = NativeMethods.public_duk_has_var(ctx);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_hex_decode(SafeContextHandle ctx, int index)
        {
            NativeMethods.public_duk_hex_decode(ctx, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public string duk_hex_encode(SafeContextHandle ctx, int index)
        {
            var internalReturnValue = NativeMethods.public_duk_hex_encode(ctx, index);
            var returnValue = MarshalHelper.StringFromUTF8(internalReturnValue);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_insert(SafeContextHandle ctx, int to_index)
        {
            NativeMethods.public_duk_insert(ctx, to_index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_is_array(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_is_array(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_is_boolean(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_is_boolean(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_is_bound_function(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_is_bound_function(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_is_buffer(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_is_buffer(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_is_callable(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_is_callable(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_is_constructor_call(SafeContextHandle ctx)
        {
            var returnValue = NativeMethods.public_duk_is_constructor_call(ctx);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_is_c_function(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_is_c_function(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_is_dynamic_buffer(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_is_dynamic_buffer(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_is_ecmascript_function(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_is_ecmascript_function(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_is_error(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_is_error(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_is_fixed_buffer(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_is_fixed_buffer(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_is_function(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_is_function(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_is_lightfunc(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_is_lightfunc(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_is_nan(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_is_nan(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_is_null(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_is_null(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_is_null_or_undefined(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_is_null_or_undefined(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_is_number(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_is_number(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_is_object(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_is_object(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_is_object_coercible(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_is_object_coercible(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_is_pointer(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_is_pointer(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_is_primitive(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_is_primitive(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_is_strict_call(SafeContextHandle ctx)
        {
            var returnValue = NativeMethods.public_duk_is_strict_call(ctx);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_is_string(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_is_string(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_is_thread(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_is_thread(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_is_undefined(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_is_undefined(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_is_valid_index(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_is_valid_index(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_join(SafeContextHandle ctx, int count)
        {
            NativeMethods.public_duk_join(ctx, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_json_decode(SafeContextHandle ctx, int index)
        {
            NativeMethods.public_duk_json_decode(ctx, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public string duk_json_encode(SafeContextHandle ctx, int index)
        {
            var internalReturnValue = NativeMethods.public_duk_json_encode(ctx, index);
            var returnValue = MarshalHelper.StringFromUTF8(internalReturnValue);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_map_string(SafeContextHandle ctx, int index, duk_map_char_function callback, IntPtr udata)
        {
            NativeMethods.public_duk_map_string(ctx, index, callback, udata);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_new(SafeContextHandle ctx, int nargs)
        {
            NativeMethods.public_duk_new(ctx, nargs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_next(SafeContextHandle ctx, int enum_index, bool get_value)
        {
            var returnValue = NativeMethods.public_duk_next(ctx, enum_index, get_value);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_normalize_index(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_normalize_index(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public ReturnCode duk_pcall(SafeContextHandle ctx, int nargs)
        {
            var returnValue = NativeMethods.public_duk_pcall(ctx, nargs);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public ReturnCode duk_pcall_method(SafeContextHandle ctx, int nargs)
        {
            var returnValue = NativeMethods.public_duk_pcall_method(ctx, nargs);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public ReturnCode duk_pcall_prop(SafeContextHandle ctx, int obj_index, int nargs)
        {
            var returnValue = NativeMethods.public_duk_pcall_prop(ctx, obj_index, nargs);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_pcompile(SafeContextHandle ctx, uint flags)
        {
            var returnValue = NativeMethods.public_duk_pcompile(ctx, flags);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_pcompile_file(SafeContextHandle ctx, uint flags, string path)
        {
            var internalStringpath = MarshalHelper.StringToUTF8(path);
            var returnValue = NativeMethods.public_duk_pcompile_file(ctx, flags, internalStringpath);
            Marshal.FreeHGlobal(internalStringpath);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_pcompile_lstring(SafeContextHandle ctx, uint flags, string src, UIntPtr len)
        {
            var internalStringsrc = MarshalHelper.StringToUTF8(src);
            var returnValue = NativeMethods.public_duk_pcompile_lstring(ctx, flags, internalStringsrc, len);
            Marshal.FreeHGlobal(internalStringsrc);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_pcompile_lstring_filename(SafeContextHandle ctx, uint flags, string src, UIntPtr len)
        {
            var internalStringsrc = MarshalHelper.StringToUTF8(src);
            var returnValue = NativeMethods.public_duk_pcompile_lstring_filename(ctx, flags, internalStringsrc, len);
            Marshal.FreeHGlobal(internalStringsrc);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_pcompile_string(SafeContextHandle ctx, uint flags, string src)
        {
            var internalStringsrc = MarshalHelper.StringToUTF8(src);
            var returnValue = NativeMethods.public_duk_pcompile_string(ctx, flags, internalStringsrc);
            Marshal.FreeHGlobal(internalStringsrc);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_pcompile_string_filename(SafeContextHandle ctx, uint flags, string src)
        {
            var internalStringsrc = MarshalHelper.StringToUTF8(src);
            var returnValue = NativeMethods.public_duk_pcompile_string_filename(ctx, flags, internalStringsrc);
            Marshal.FreeHGlobal(internalStringsrc);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_peval(SafeContextHandle ctx)
        {
            var returnValue = NativeMethods.public_duk_peval(ctx);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_peval_file(SafeContextHandle ctx, string path)
        {
            var internalStringpath = MarshalHelper.StringToUTF8(path);
            var returnValue = NativeMethods.public_duk_peval_file(ctx, internalStringpath);
            Marshal.FreeHGlobal(internalStringpath);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_peval_file_noresult(SafeContextHandle ctx, string path)
        {
            var internalStringpath = MarshalHelper.StringToUTF8(path);
            var returnValue = NativeMethods.public_duk_peval_file_noresult(ctx, internalStringpath);
            Marshal.FreeHGlobal(internalStringpath);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_peval_lstring(SafeContextHandle ctx, string src, UIntPtr len)
        {
            var internalStringsrc = MarshalHelper.StringToUTF8(src);
            var returnValue = NativeMethods.public_duk_peval_lstring(ctx, internalStringsrc, len);
            Marshal.FreeHGlobal(internalStringsrc);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_peval_lstring_noresult(SafeContextHandle ctx, string src, UIntPtr len)
        {
            var internalStringsrc = MarshalHelper.StringToUTF8(src);
            var returnValue = NativeMethods.public_duk_peval_lstring_noresult(ctx, internalStringsrc, len);
            Marshal.FreeHGlobal(internalStringsrc);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_peval_noresult(SafeContextHandle ctx)
        {
            var returnValue = NativeMethods.public_duk_peval_noresult(ctx);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_peval_string(SafeContextHandle ctx, string src)
        {
            var internalStringsrc = MarshalHelper.StringToUTF8(src);
            var returnValue = NativeMethods.public_duk_peval_string(ctx, internalStringsrc);
            Marshal.FreeHGlobal(internalStringsrc);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_peval_string_noresult(SafeContextHandle ctx, string src)
        {
            var internalStringsrc = MarshalHelper.StringToUTF8(src);
            var returnValue = NativeMethods.public_duk_peval_string_noresult(ctx, internalStringsrc);
            Marshal.FreeHGlobal(internalStringsrc);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_pop(SafeContextHandle ctx)
        {
            NativeMethods.public_duk_pop(ctx);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_pop_2(SafeContextHandle ctx)
        {
            NativeMethods.public_duk_pop_2(ctx);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_pop_3(SafeContextHandle ctx)
        {
            NativeMethods.public_duk_pop_3(ctx);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_pop_n(SafeContextHandle ctx, int count)
        {
            NativeMethods.public_duk_pop_n(ctx, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_push_array(SafeContextHandle ctx)
        {
            var returnValue = NativeMethods.public_duk_push_array(ctx);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_push_boolean(SafeContextHandle ctx, bool val)
        {
            NativeMethods.public_duk_push_boolean(ctx, val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public IntPtr duk_push_buffer(SafeContextHandle ctx, UIntPtr size, bool dynamic)
        {
            var returnValue = NativeMethods.public_duk_push_buffer(ctx, size, dynamic);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_push_context_dump(SafeContextHandle ctx)
        {
            NativeMethods.public_duk_push_context_dump(ctx);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_push_current_function(SafeContextHandle ctx)
        {
            NativeMethods.public_duk_push_current_function(ctx);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_push_current_thread(SafeContextHandle ctx)
        {
            NativeMethods.public_duk_push_current_thread(ctx);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_push_c_function(SafeContextHandle ctx, duk_c_function func, int nargs)
        {
            var returnValue = NativeMethods.public_duk_push_c_function(ctx, func, nargs);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_push_c_lightfunc(SafeContextHandle ctx, duk_c_function func, int nargs, int length, int magic)
        {
            var returnValue = NativeMethods.public_duk_push_c_lightfunc(ctx, func, nargs, length, magic);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public IntPtr duk_push_dynamic_buffer(SafeContextHandle ctx, UIntPtr size)
        {
            var returnValue = NativeMethods.public_duk_push_dynamic_buffer(ctx, size);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_push_false(SafeContextHandle ctx)
        {
            NativeMethods.public_duk_push_false(ctx);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public IntPtr duk_push_fixed_buffer(SafeContextHandle ctx, UIntPtr size)
        {
            var returnValue = NativeMethods.public_duk_push_fixed_buffer(ctx, size);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_push_global_object(SafeContextHandle ctx)
        {
            NativeMethods.public_duk_push_global_object(ctx);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_push_global_stash(SafeContextHandle ctx)
        {
            NativeMethods.public_duk_push_global_stash(ctx);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_push_heapptr(SafeContextHandle ctx, IntPtr ptr)
        {
            var returnValue = NativeMethods.public_duk_push_heapptr(ctx, ptr);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_push_heap_stash(SafeContextHandle ctx)
        {
            NativeMethods.public_duk_push_heap_stash(ctx);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_push_int(SafeContextHandle ctx, int val)
        {
            NativeMethods.public_duk_push_int(ctx, val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public string duk_push_lstring(SafeContextHandle ctx, string str, UIntPtr len)
        {
            var internalStringstr = MarshalHelper.StringToUTF8(str);
            var internalReturnValue = NativeMethods.public_duk_push_lstring(ctx, internalStringstr, len);
            Marshal.FreeHGlobal(internalStringstr);
            var returnValue = MarshalHelper.StringFromUTF8(internalReturnValue);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_push_nan(SafeContextHandle ctx)
        {
            NativeMethods.public_duk_push_nan(ctx);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_push_null(SafeContextHandle ctx)
        {
            NativeMethods.public_duk_push_null(ctx);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_push_number(SafeContextHandle ctx, double val)
        {
            NativeMethods.public_duk_push_number(ctx, val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_push_object(SafeContextHandle ctx)
        {
            var returnValue = NativeMethods.public_duk_push_object(ctx);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_push_pointer(SafeContextHandle ctx, IntPtr p)
        {
            NativeMethods.public_duk_push_pointer(ctx, p);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public string duk_push_string(SafeContextHandle ctx, string str)
        {
            var internalStringstr = MarshalHelper.StringToUTF8(str);
            var internalReturnValue = NativeMethods.public_duk_push_string(ctx, internalStringstr);
            Marshal.FreeHGlobal(internalStringstr);
            var returnValue = MarshalHelper.StringFromUTF8(internalReturnValue);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public string duk_push_string_file(SafeContextHandle ctx, string path)
        {
            var internalStringpath = MarshalHelper.StringToUTF8(path);
            var internalReturnValue = NativeMethods.public_duk_push_string_file(ctx, internalStringpath);
            Marshal.FreeHGlobal(internalStringpath);
            var returnValue = MarshalHelper.StringFromUTF8(internalReturnValue);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_push_this(SafeContextHandle ctx)
        {
            NativeMethods.public_duk_push_this(ctx);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_push_thread(SafeContextHandle ctx)
        {
            var returnValue = NativeMethods.public_duk_push_thread(ctx);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_push_thread_new_globalenv(SafeContextHandle ctx)
        {
            var returnValue = NativeMethods.public_duk_push_thread_new_globalenv(ctx);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_push_thread_stash(SafeContextHandle ctx, SafeContextHandle target_ctx)
        {
            NativeMethods.public_duk_push_thread_stash(ctx, target_ctx);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_push_true(SafeContextHandle ctx)
        {
            NativeMethods.public_duk_push_true(ctx);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_push_uint(SafeContextHandle ctx, uint val)
        {
            NativeMethods.public_duk_push_uint(ctx, val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_push_undefined(SafeContextHandle ctx)
        {
            NativeMethods.public_duk_push_undefined(ctx);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_put_function_list(SafeContextHandle ctx, int obj_index, duk_function_list_entry[] funcs)
        {
            NativeMethods.public_duk_put_function_list(ctx, obj_index, funcs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_put_global_string(SafeContextHandle ctx, string key)
        {
            var internalStringkey = MarshalHelper.StringToUTF8(key);
            var returnValue = NativeMethods.public_duk_put_global_string(ctx, internalStringkey);
            Marshal.FreeHGlobal(internalStringkey);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_put_number_list(SafeContextHandle ctx, int obj_index, duk_number_list_entry[] numbers)
        {
            NativeMethods.public_duk_put_number_list(ctx, obj_index, numbers);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_put_prop(SafeContextHandle ctx, int obj_index)
        {
            var returnValue = NativeMethods.public_duk_put_prop(ctx, obj_index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_put_prop_index(SafeContextHandle ctx, int obj_index, uint arr_index)
        {
            var returnValue = NativeMethods.public_duk_put_prop_index(ctx, obj_index, arr_index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_put_prop_string(SafeContextHandle ctx, int obj_index, string key)
        {
            var internalStringkey = MarshalHelper.StringToUTF8(key);
            var returnValue = NativeMethods.public_duk_put_prop_string(ctx, obj_index, internalStringkey);
            Marshal.FreeHGlobal(internalStringkey);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_put_var(SafeContextHandle ctx)
        {
            NativeMethods.public_duk_put_var(ctx);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public IntPtr duk_realloc(SafeContextHandle ctx, IntPtr ptr, UIntPtr size)
        {
            var returnValue = NativeMethods.public_duk_realloc(ctx, ptr, size);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public IntPtr duk_realloc_raw(SafeContextHandle ctx, IntPtr ptr, UIntPtr size)
        {
            var returnValue = NativeMethods.public_duk_realloc_raw(ctx, ptr, size);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_remove(SafeContextHandle ctx, int index)
        {
            NativeMethods.public_duk_remove(ctx, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_replace(SafeContextHandle ctx, int to_index)
        {
            NativeMethods.public_duk_replace(ctx, to_index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_require_boolean(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_require_boolean(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public IntPtr duk_require_buffer(SafeContextHandle ctx, int index, IntPtr out_size)
        {
            var returnValue = NativeMethods.public_duk_require_buffer(ctx, index, out_size);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public SafeContextHandle duk_require_context(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_require_context(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public duk_c_function duk_require_c_function(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_require_c_function(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public IntPtr duk_require_heapptr(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_require_heapptr(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_require_int(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_require_int(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public string duk_require_lstring(SafeContextHandle ctx, int index, IntPtr out_len)
        {
            var internalReturnValue = NativeMethods.public_duk_require_lstring(ctx, index, out_len);
            var returnValue = MarshalHelper.StringFromUTF8(internalReturnValue);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_require_normalize_index(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_require_normalize_index(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_require_null(SafeContextHandle ctx, int index)
        {
            NativeMethods.public_duk_require_null(ctx, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public double duk_require_number(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_require_number(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_require_object_coercible(SafeContextHandle ctx, int index)
        {
            NativeMethods.public_duk_require_object_coercible(ctx, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public IntPtr duk_require_pointer(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_require_pointer(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_require_stack(SafeContextHandle ctx, int extra)
        {
            NativeMethods.public_duk_require_stack(ctx, extra);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_require_stack_top(SafeContextHandle ctx, int top)
        {
            NativeMethods.public_duk_require_stack_top(ctx, top);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public string duk_require_string(SafeContextHandle ctx, int index)
        {
            var internalReturnValue = NativeMethods.public_duk_require_string(ctx, index);
            var returnValue = MarshalHelper.StringFromUTF8(internalReturnValue);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_require_top_index(SafeContextHandle ctx)
        {
            var returnValue = NativeMethods.public_duk_require_top_index(ctx);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_require_type_mask(SafeContextHandle ctx, int index, uint mask)
        {
            NativeMethods.public_duk_require_type_mask(ctx, index, mask);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public uint duk_require_uint(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_require_uint(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_require_undefined(SafeContextHandle ctx, int index)
        {
            NativeMethods.public_duk_require_undefined(ctx, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_require_valid_index(SafeContextHandle ctx, int index)
        {
            NativeMethods.public_duk_require_valid_index(ctx, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public IntPtr duk_resize_buffer(SafeContextHandle ctx, int index, UIntPtr new_size)
        {
            var returnValue = NativeMethods.public_duk_resize_buffer(ctx, index, new_size);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public ReturnCode duk_safe_call(SafeContextHandle ctx, duk_safe_call_function func, int nargs, int nrets)
        {
            var returnValue = NativeMethods.public_duk_safe_call(ctx, func, nargs, nrets);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public string duk_safe_to_lstring(SafeContextHandle ctx, int index, IntPtr out_len)
        {
            var internalReturnValue = NativeMethods.public_duk_safe_to_lstring(ctx, index, out_len);
            var returnValue = MarshalHelper.StringFromUTF8(internalReturnValue);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public string duk_safe_to_string(SafeContextHandle ctx, int index)
        {
            var internalReturnValue = NativeMethods.public_duk_safe_to_string(ctx, index);
            var returnValue = MarshalHelper.StringFromUTF8(internalReturnValue);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_set_finalizer(SafeContextHandle ctx, int index)
        {
            NativeMethods.public_duk_set_finalizer(ctx, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_set_global_object(SafeContextHandle ctx)
        {
            NativeMethods.public_duk_set_global_object(ctx);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_set_magic(SafeContextHandle ctx, int index, int magic)
        {
            NativeMethods.public_duk_set_magic(ctx, index, magic);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_set_prototype(SafeContextHandle ctx, int index)
        {
            NativeMethods.public_duk_set_prototype(ctx, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_set_top(SafeContextHandle ctx, int index)
        {
            NativeMethods.public_duk_set_top(ctx, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_strict_equals(SafeContextHandle ctx, int index1, int index2)
        {
            var returnValue = NativeMethods.public_duk_strict_equals(ctx, index1, index2);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_substring(SafeContextHandle ctx, int index, UIntPtr start_char_offset, UIntPtr end_char_offset)
        {
            NativeMethods.public_duk_substring(ctx, index, start_char_offset, end_char_offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_swap(SafeContextHandle ctx, int index1, int index2)
        {
            NativeMethods.public_duk_swap(ctx, index1, index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_swap_top(SafeContextHandle ctx, int index)
        {
            NativeMethods.public_duk_swap_top(ctx, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_throw(SafeContextHandle ctx)
        {
            NativeMethods.public_duk_throw(ctx);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public bool duk_to_boolean(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_to_boolean(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public IntPtr duk_to_buffer(SafeContextHandle ctx, int index, IntPtr out_size)
        {
            var returnValue = NativeMethods.public_duk_to_buffer(ctx, index, out_size);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_to_defaultvalue(SafeContextHandle ctx, int index, CoercionHint hint)
        {
            NativeMethods.public_duk_to_defaultvalue(ctx, index, hint);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public IntPtr duk_to_dynamic_buffer(SafeContextHandle ctx, int index, IntPtr out_size)
        {
            var returnValue = NativeMethods.public_duk_to_dynamic_buffer(ctx, index, out_size);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public IntPtr duk_to_fixed_buffer(SafeContextHandle ctx, int index, IntPtr out_size)
        {
            var returnValue = NativeMethods.public_duk_to_fixed_buffer(ctx, index, out_size);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_to_int(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_to_int(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public int duk_to_int32(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_to_int32(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public string duk_to_lstring(SafeContextHandle ctx, int index, IntPtr out_len)
        {
            var internalReturnValue = NativeMethods.public_duk_to_lstring(ctx, index, out_len);
            var returnValue = MarshalHelper.StringFromUTF8(internalReturnValue);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_to_null(SafeContextHandle ctx, int index)
        {
            NativeMethods.public_duk_to_null(ctx, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public double duk_to_number(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_to_number(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_to_object(SafeContextHandle ctx, int index)
        {
            NativeMethods.public_duk_to_object(ctx, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public IntPtr duk_to_pointer(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_to_pointer(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_to_primitive(SafeContextHandle ctx, int index, CoercionHint hint)
        {
            NativeMethods.public_duk_to_primitive(ctx, index, hint);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public string duk_to_string(SafeContextHandle ctx, int index)
        {
            var internalReturnValue = NativeMethods.public_duk_to_string(ctx, index);
            var returnValue = MarshalHelper.StringFromUTF8(internalReturnValue);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public uint duk_to_uint(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_to_uint(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public ushort duk_to_uint16(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_to_uint16(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public uint duk_to_uint32(SafeContextHandle ctx, int index)
        {
            var returnValue = NativeMethods.public_duk_to_uint32(ctx, index);
            return returnValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_to_undefined(SafeContextHandle ctx, int index)
        {
            NativeMethods.public_duk_to_undefined(ctx, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_trim(SafeContextHandle ctx, int index)
        {
            NativeMethods.public_duk_trim(ctx, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_xcopy_top(SafeContextHandle to_ctx, SafeContextHandle from_ctx, int count)
        {
            NativeMethods.public_duk_xcopy_top(to_ctx, from_ctx, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public void duk_xmove_top(SafeContextHandle to_ctx, SafeContextHandle from_ctx, int count)
        {
            NativeMethods.public_duk_xmove_top(to_ctx, from_ctx, count);
        }

        private static class NativeMethods
        {
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_alloc")]
            static public extern IntPtr public_duk_alloc(SafeContextHandle ctx, UIntPtr size);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_alloc_raw")]
            static public extern IntPtr public_duk_alloc_raw(SafeContextHandle ctx, UIntPtr size);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_base64_decode")]
            static public extern void public_duk_base64_decode(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_base64_encode")]
            static public extern IntPtr public_duk_base64_encode(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_call")]
            static public extern void public_duk_call(SafeContextHandle ctx, int nargs);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_call_method")]
            static public extern void public_duk_call_method(SafeContextHandle ctx, int nargs);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_call_prop")]
            static public extern void public_duk_call_prop(SafeContextHandle ctx, int obj_index, int nargs);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_char_code_at")]
            static public extern int public_duk_char_code_at(SafeContextHandle ctx, int index, UIntPtr char_offset);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_check_stack")]
            static public extern bool public_duk_check_stack(SafeContextHandle ctx, int extra);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_check_stack_top")]
            static public extern bool public_duk_check_stack_top(SafeContextHandle ctx, int top);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_check_type")]
            static public extern bool public_duk_check_type(SafeContextHandle ctx, int index, JSType type);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_check_type_mask")]
            static public extern bool public_duk_check_type_mask(SafeContextHandle ctx, int index, TypeMask mask);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_compact")]
            static public extern void public_duk_compact(SafeContextHandle ctx, int obj_index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_compile")]
            static public extern void public_duk_compile(SafeContextHandle ctx, uint flags);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_compile_file")]
            static public extern void public_duk_compile_file(SafeContextHandle ctx, uint flags, IntPtr path);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_compile_lstring")]
            static public extern void public_duk_compile_lstring(SafeContextHandle ctx, uint flags, IntPtr src, UIntPtr len);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_compile_lstring_filename")]
            static public extern void public_duk_compile_lstring_filename(SafeContextHandle ctx, uint flags, IntPtr src, UIntPtr len);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_compile_string")]
            static public extern void public_duk_compile_string(SafeContextHandle ctx, uint flags, IntPtr src);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_compile_string_filename")]
            static public extern void public_duk_compile_string_filename(SafeContextHandle ctx, uint flags, IntPtr src);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_concat")]
            static public extern void public_duk_concat(SafeContextHandle ctx, int count);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_copy")]
            static public extern void public_duk_copy(SafeContextHandle ctx, int from_index, int to_index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_create_heap")]
            static public extern SafeContextHandle public_duk_create_heap(duk_alloc_function alloc_func, duk_realloc_function realloc_func, duk_free_function free_func, IntPtr heap_udata, duk_fatal_function fatal_handler);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_create_heap_default")]
            static public extern SafeContextHandle public_duk_create_heap_default();
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_debugger_attach")]
            static public extern void public_duk_debugger_attach(SafeContextHandle ctx, duk_debug_read_function read_cb, duk_debug_write_function write_cb, duk_debug_peek_function peek_cb, duk_debug_read_flush_function read_flush_cb, duk_debug_write_flush_function write_flush_cb, duk_debug_detached_function detached_cb, IntPtr udata);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_debugger_cooperate")]
            static public extern void public_duk_debugger_cooperate(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_debugger_detach")]
            static public extern void public_duk_debugger_detach(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_decode_string")]
            static public extern void public_duk_decode_string(SafeContextHandle ctx, int index, duk_decode_char_function callback, IntPtr udata);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_def_prop")]
            static public extern void public_duk_def_prop(SafeContextHandle ctx, int obj_index, PropertyFlags flags);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_del_prop")]
            static public extern bool public_duk_del_prop(SafeContextHandle ctx, int obj_index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_del_prop_index")]
            static public extern bool public_duk_del_prop_index(SafeContextHandle ctx, int obj_index, uint arr_index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_del_prop_string")]
            static public extern bool public_duk_del_prop_string(SafeContextHandle ctx, int obj_index, IntPtr key);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_del_var")]
            static public extern bool public_duk_del_var(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_destroy_heap")]
            static public extern void public_duk_destroy_heap(IntPtr ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_dump_context_stderr")]
            static public extern void public_duk_dump_context_stderr(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_dump_context_stdout")]
            static public extern void public_duk_dump_context_stdout(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_dup")]
            static public extern void public_duk_dup(SafeContextHandle ctx, int from_index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_dup_top")]
            static public extern void public_duk_dup_top(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_enum")]
            static public extern void public_duk_enum(SafeContextHandle ctx, int obj_index, EnumFlag enum_flags);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_equals")]
            static public extern bool public_duk_equals(SafeContextHandle ctx, int index1, int index2);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_eval")]
            static public extern void public_duk_eval(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_eval_file")]
            static public extern void public_duk_eval_file(SafeContextHandle ctx, IntPtr path);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_eval_file_noresult")]
            static public extern void public_duk_eval_file_noresult(SafeContextHandle ctx, IntPtr path);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_eval_lstring")]
            static public extern void public_duk_eval_lstring(SafeContextHandle ctx, IntPtr src, UIntPtr len);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_eval_lstring_noresult")]
            static public extern void public_duk_eval_lstring_noresult(SafeContextHandle ctx, IntPtr src, UIntPtr len);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_eval_noresult")]
            static public extern void public_duk_eval_noresult(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_eval_string")]
            static public extern void public_duk_eval_string(SafeContextHandle ctx, IntPtr src);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_eval_string_noresult")]
            static public extern void public_duk_eval_string_noresult(SafeContextHandle ctx, IntPtr src);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_fatal")]
            static public extern void public_duk_fatal(SafeContextHandle ctx, ErrorCode err_code, IntPtr err_msg);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_free")]
            static public extern void public_duk_free(SafeContextHandle ctx, IntPtr ptr);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_free_raw")]
            static public extern void public_duk_free_raw(SafeContextHandle ctx, IntPtr ptr);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_gc")]
            static public extern void public_duk_gc(SafeContextHandle ctx, uint flags);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_get_boolean")]
            static public extern bool public_duk_get_boolean(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_get_buffer")]
            static public extern IntPtr public_duk_get_buffer(SafeContextHandle ctx, int index, IntPtr out_size);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_get_context")]
            static public extern SafeContextHandle public_duk_get_context(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_get_current_magic")]
            static public extern int public_duk_get_current_magic(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_get_c_function")]
            static public extern duk_c_function public_duk_get_c_function(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_get_error_code")]
            static public extern ErrorCode public_duk_get_error_code(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_get_finalizer")]
            static public extern void public_duk_get_finalizer(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_get_global_string")]
            static public extern bool public_duk_get_global_string(SafeContextHandle ctx, IntPtr key);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_get_heapptr")]
            static public extern IntPtr public_duk_get_heapptr(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_get_int")]
            static public extern int public_duk_get_int(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_get_length")]
            static public extern UIntPtr public_duk_get_length(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_get_lstring")]
            static public extern IntPtr public_duk_get_lstring(SafeContextHandle ctx, int index, IntPtr out_len);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_get_magic")]
            static public extern int public_duk_get_magic(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_get_memory_functions")]
            static public extern void public_duk_get_memory_functions(SafeContextHandle ctx, out duk_memory_functions out_funcs);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_get_number")]
            static public extern double public_duk_get_number(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_get_pointer")]
            static public extern IntPtr public_duk_get_pointer(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_get_prop")]
            static public extern bool public_duk_get_prop(SafeContextHandle ctx, int obj_index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_get_prop_index")]
            static public extern bool public_duk_get_prop_index(SafeContextHandle ctx, int obj_index, uint arr_index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_get_prop_string")]
            static public extern bool public_duk_get_prop_string(SafeContextHandle ctx, int obj_index, IntPtr key);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_get_prototype")]
            static public extern void public_duk_get_prototype(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_get_string")]
            static public extern IntPtr public_duk_get_string(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_get_top")]
            static public extern int public_duk_get_top(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_get_top_index")]
            static public extern int public_duk_get_top_index(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_get_type")]
            static public extern JSType public_duk_get_type(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_get_type_mask")]
            static public extern TypeMask public_duk_get_type_mask(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_get_uint")]
            static public extern uint public_duk_get_uint(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_get_var")]
            static public extern void public_duk_get_var(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_has_prop")]
            static public extern bool public_duk_has_prop(SafeContextHandle ctx, int obj_index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_has_prop_index")]
            static public extern bool public_duk_has_prop_index(SafeContextHandle ctx, int obj_index, uint arr_index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_has_prop_string")]
            static public extern bool public_duk_has_prop_string(SafeContextHandle ctx, int obj_index, IntPtr key);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_has_var")]
            static public extern bool public_duk_has_var(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_hex_decode")]
            static public extern void public_duk_hex_decode(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_hex_encode")]
            static public extern IntPtr public_duk_hex_encode(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_insert")]
            static public extern void public_duk_insert(SafeContextHandle ctx, int to_index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_is_array")]
            static public extern bool public_duk_is_array(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_is_boolean")]
            static public extern bool public_duk_is_boolean(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_is_bound_function")]
            static public extern bool public_duk_is_bound_function(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_is_buffer")]
            static public extern bool public_duk_is_buffer(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_is_callable")]
            static public extern bool public_duk_is_callable(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_is_constructor_call")]
            static public extern bool public_duk_is_constructor_call(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_is_c_function")]
            static public extern bool public_duk_is_c_function(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_is_dynamic_buffer")]
            static public extern bool public_duk_is_dynamic_buffer(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_is_ecmascript_function")]
            static public extern bool public_duk_is_ecmascript_function(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_is_error")]
            static public extern bool public_duk_is_error(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_is_fixed_buffer")]
            static public extern bool public_duk_is_fixed_buffer(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_is_function")]
            static public extern bool public_duk_is_function(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_is_lightfunc")]
            static public extern bool public_duk_is_lightfunc(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_is_nan")]
            static public extern bool public_duk_is_nan(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_is_null")]
            static public extern bool public_duk_is_null(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_is_null_or_undefined")]
            static public extern bool public_duk_is_null_or_undefined(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_is_number")]
            static public extern bool public_duk_is_number(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_is_object")]
            static public extern bool public_duk_is_object(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_is_object_coercible")]
            static public extern bool public_duk_is_object_coercible(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_is_pointer")]
            static public extern bool public_duk_is_pointer(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_is_primitive")]
            static public extern bool public_duk_is_primitive(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_is_strict_call")]
            static public extern bool public_duk_is_strict_call(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_is_string")]
            static public extern bool public_duk_is_string(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_is_thread")]
            static public extern bool public_duk_is_thread(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_is_undefined")]
            static public extern bool public_duk_is_undefined(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_is_valid_index")]
            static public extern bool public_duk_is_valid_index(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_join")]
            static public extern void public_duk_join(SafeContextHandle ctx, int count);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_json_decode")]
            static public extern void public_duk_json_decode(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_json_encode")]
            static public extern IntPtr public_duk_json_encode(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_map_string")]
            static public extern void public_duk_map_string(SafeContextHandle ctx, int index, duk_map_char_function callback, IntPtr udata);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_new")]
            static public extern void public_duk_new(SafeContextHandle ctx, int nargs);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_next")]
            static public extern bool public_duk_next(SafeContextHandle ctx, int enum_index, bool get_value);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_normalize_index")]
            static public extern int public_duk_normalize_index(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_pcall")]
            static public extern ReturnCode public_duk_pcall(SafeContextHandle ctx, int nargs);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_pcall_method")]
            static public extern ReturnCode public_duk_pcall_method(SafeContextHandle ctx, int nargs);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_pcall_prop")]
            static public extern ReturnCode public_duk_pcall_prop(SafeContextHandle ctx, int obj_index, int nargs);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_pcompile")]
            static public extern int public_duk_pcompile(SafeContextHandle ctx, uint flags);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_pcompile_file")]
            static public extern int public_duk_pcompile_file(SafeContextHandle ctx, uint flags, IntPtr path);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_pcompile_lstring")]
            static public extern int public_duk_pcompile_lstring(SafeContextHandle ctx, uint flags, IntPtr src, UIntPtr len);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_pcompile_lstring_filename")]
            static public extern int public_duk_pcompile_lstring_filename(SafeContextHandle ctx, uint flags, IntPtr src, UIntPtr len);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_pcompile_string")]
            static public extern int public_duk_pcompile_string(SafeContextHandle ctx, uint flags, IntPtr src);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_pcompile_string_filename")]
            static public extern int public_duk_pcompile_string_filename(SafeContextHandle ctx, uint flags, IntPtr src);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_peval")]
            static public extern int public_duk_peval(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_peval_file")]
            static public extern int public_duk_peval_file(SafeContextHandle ctx, IntPtr path);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_peval_file_noresult")]
            static public extern int public_duk_peval_file_noresult(SafeContextHandle ctx, IntPtr path);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_peval_lstring")]
            static public extern int public_duk_peval_lstring(SafeContextHandle ctx, IntPtr src, UIntPtr len);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_peval_lstring_noresult")]
            static public extern int public_duk_peval_lstring_noresult(SafeContextHandle ctx, IntPtr src, UIntPtr len);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_peval_noresult")]
            static public extern int public_duk_peval_noresult(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_peval_string")]
            static public extern int public_duk_peval_string(SafeContextHandle ctx, IntPtr src);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_peval_string_noresult")]
            static public extern int public_duk_peval_string_noresult(SafeContextHandle ctx, IntPtr src);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_pop")]
            static public extern void public_duk_pop(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_pop_2")]
            static public extern void public_duk_pop_2(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_pop_3")]
            static public extern void public_duk_pop_3(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_pop_n")]
            static public extern void public_duk_pop_n(SafeContextHandle ctx, int count);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_push_array")]
            static public extern int public_duk_push_array(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_push_boolean")]
            static public extern void public_duk_push_boolean(SafeContextHandle ctx, bool val);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_push_buffer")]
            static public extern IntPtr public_duk_push_buffer(SafeContextHandle ctx, UIntPtr size, bool dynamic);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_push_context_dump")]
            static public extern void public_duk_push_context_dump(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_push_current_function")]
            static public extern void public_duk_push_current_function(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_push_current_thread")]
            static public extern void public_duk_push_current_thread(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_push_c_function")]
            static public extern int public_duk_push_c_function(SafeContextHandle ctx, duk_c_function func, int nargs);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_push_c_lightfunc")]
            static public extern int public_duk_push_c_lightfunc(SafeContextHandle ctx, duk_c_function func, int nargs, int length, int magic);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_push_dynamic_buffer")]
            static public extern IntPtr public_duk_push_dynamic_buffer(SafeContextHandle ctx, UIntPtr size);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_push_false")]
            static public extern void public_duk_push_false(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_push_fixed_buffer")]
            static public extern IntPtr public_duk_push_fixed_buffer(SafeContextHandle ctx, UIntPtr size);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_push_global_object")]
            static public extern void public_duk_push_global_object(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_push_global_stash")]
            static public extern void public_duk_push_global_stash(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_push_heapptr")]
            static public extern int public_duk_push_heapptr(SafeContextHandle ctx, IntPtr ptr);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_push_heap_stash")]
            static public extern void public_duk_push_heap_stash(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_push_int")]
            static public extern void public_duk_push_int(SafeContextHandle ctx, int val);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_push_lstring")]
            static public extern IntPtr public_duk_push_lstring(SafeContextHandle ctx, IntPtr str, UIntPtr len);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_push_nan")]
            static public extern void public_duk_push_nan(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_push_null")]
            static public extern void public_duk_push_null(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_push_number")]
            static public extern void public_duk_push_number(SafeContextHandle ctx, double val);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_push_object")]
            static public extern int public_duk_push_object(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_push_pointer")]
            static public extern void public_duk_push_pointer(SafeContextHandle ctx, IntPtr p);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_push_string")]
            static public extern IntPtr public_duk_push_string(SafeContextHandle ctx, IntPtr str);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_push_string_file")]
            static public extern IntPtr public_duk_push_string_file(SafeContextHandle ctx, IntPtr path);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_push_this")]
            static public extern void public_duk_push_this(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_push_thread")]
            static public extern int public_duk_push_thread(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_push_thread_new_globalenv")]
            static public extern int public_duk_push_thread_new_globalenv(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_push_thread_stash")]
            static public extern void public_duk_push_thread_stash(SafeContextHandle ctx, SafeContextHandle target_ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_push_true")]
            static public extern void public_duk_push_true(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_push_uint")]
            static public extern void public_duk_push_uint(SafeContextHandle ctx, uint val);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_push_undefined")]
            static public extern void public_duk_push_undefined(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_put_function_list")]
            static public extern void public_duk_put_function_list(SafeContextHandle ctx, int obj_index, duk_function_list_entry[] funcs);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_put_global_string")]
            static public extern bool public_duk_put_global_string(SafeContextHandle ctx, IntPtr key);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_put_number_list")]
            static public extern void public_duk_put_number_list(SafeContextHandle ctx, int obj_index, duk_number_list_entry[] numbers);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_put_prop")]
            static public extern bool public_duk_put_prop(SafeContextHandle ctx, int obj_index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_put_prop_index")]
            static public extern bool public_duk_put_prop_index(SafeContextHandle ctx, int obj_index, uint arr_index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_put_prop_string")]
            static public extern bool public_duk_put_prop_string(SafeContextHandle ctx, int obj_index, IntPtr key);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_put_var")]
            static public extern void public_duk_put_var(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_realloc")]
            static public extern IntPtr public_duk_realloc(SafeContextHandle ctx, IntPtr ptr, UIntPtr size);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_realloc_raw")]
            static public extern IntPtr public_duk_realloc_raw(SafeContextHandle ctx, IntPtr ptr, UIntPtr size);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_remove")]
            static public extern void public_duk_remove(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_replace")]
            static public extern void public_duk_replace(SafeContextHandle ctx, int to_index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_require_boolean")]
            static public extern bool public_duk_require_boolean(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_require_buffer")]
            static public extern IntPtr public_duk_require_buffer(SafeContextHandle ctx, int index, IntPtr out_size);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_require_context")]
            static public extern SafeContextHandle public_duk_require_context(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_require_c_function")]
            static public extern duk_c_function public_duk_require_c_function(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_require_heapptr")]
            static public extern IntPtr public_duk_require_heapptr(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_require_int")]
            static public extern int public_duk_require_int(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_require_lstring")]
            static public extern IntPtr public_duk_require_lstring(SafeContextHandle ctx, int index, IntPtr out_len);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_require_normalize_index")]
            static public extern int public_duk_require_normalize_index(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_require_null")]
            static public extern void public_duk_require_null(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_require_number")]
            static public extern double public_duk_require_number(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_require_object_coercible")]
            static public extern void public_duk_require_object_coercible(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_require_pointer")]
            static public extern IntPtr public_duk_require_pointer(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_require_stack")]
            static public extern void public_duk_require_stack(SafeContextHandle ctx, int extra);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_require_stack_top")]
            static public extern void public_duk_require_stack_top(SafeContextHandle ctx, int top);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_require_string")]
            static public extern IntPtr public_duk_require_string(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_require_top_index")]
            static public extern int public_duk_require_top_index(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_require_type_mask")]
            static public extern void public_duk_require_type_mask(SafeContextHandle ctx, int index, uint mask);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_require_uint")]
            static public extern uint public_duk_require_uint(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_require_undefined")]
            static public extern void public_duk_require_undefined(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_require_valid_index")]
            static public extern void public_duk_require_valid_index(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_resize_buffer")]
            static public extern IntPtr public_duk_resize_buffer(SafeContextHandle ctx, int index, UIntPtr new_size);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_safe_call")]
            static public extern ReturnCode public_duk_safe_call(SafeContextHandle ctx, duk_safe_call_function func, int nargs, int nrets);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_safe_to_lstring")]
            static public extern IntPtr public_duk_safe_to_lstring(SafeContextHandle ctx, int index, IntPtr out_len);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_safe_to_string")]
            static public extern IntPtr public_duk_safe_to_string(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_set_finalizer")]
            static public extern void public_duk_set_finalizer(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_set_global_object")]
            static public extern void public_duk_set_global_object(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_set_magic")]
            static public extern void public_duk_set_magic(SafeContextHandle ctx, int index, int magic);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_set_prototype")]
            static public extern void public_duk_set_prototype(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_set_top")]
            static public extern void public_duk_set_top(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_strict_equals")]
            static public extern bool public_duk_strict_equals(SafeContextHandle ctx, int index1, int index2);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_substring")]
            static public extern void public_duk_substring(SafeContextHandle ctx, int index, UIntPtr start_char_offset, UIntPtr end_char_offset);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_swap")]
            static public extern void public_duk_swap(SafeContextHandle ctx, int index1, int index2);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_swap_top")]
            static public extern void public_duk_swap_top(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_throw")]
            static public extern void public_duk_throw(SafeContextHandle ctx);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_to_boolean")]
            static public extern bool public_duk_to_boolean(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_to_buffer")]
            static public extern IntPtr public_duk_to_buffer(SafeContextHandle ctx, int index, IntPtr out_size);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_to_defaultvalue")]
            static public extern void public_duk_to_defaultvalue(SafeContextHandle ctx, int index, CoercionHint hint);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_to_dynamic_buffer")]
            static public extern IntPtr public_duk_to_dynamic_buffer(SafeContextHandle ctx, int index, IntPtr out_size);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_to_fixed_buffer")]
            static public extern IntPtr public_duk_to_fixed_buffer(SafeContextHandle ctx, int index, IntPtr out_size);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_to_int")]
            static public extern int public_duk_to_int(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_to_int32")]
            static public extern int public_duk_to_int32(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_to_lstring")]
            static public extern IntPtr public_duk_to_lstring(SafeContextHandle ctx, int index, IntPtr out_len);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_to_null")]
            static public extern void public_duk_to_null(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_to_number")]
            static public extern double public_duk_to_number(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_to_object")]
            static public extern void public_duk_to_object(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_to_pointer")]
            static public extern IntPtr public_duk_to_pointer(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_to_primitive")]
            static public extern void public_duk_to_primitive(SafeContextHandle ctx, int index, CoercionHint hint);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_to_string")]
            static public extern IntPtr public_duk_to_string(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_to_uint")]
            static public extern uint public_duk_to_uint(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_to_uint16")]
            static public extern ushort public_duk_to_uint16(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_to_uint32")]
            static public extern uint public_duk_to_uint32(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_to_undefined")]
            static public extern void public_duk_to_undefined(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_trim")]
            static public extern void public_duk_trim(SafeContextHandle ctx, int index);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_xcopy_top")]
            static public extern void public_duk_xcopy_top(SafeContextHandle to_ctx, SafeContextHandle from_ctx, int count);
            [DllImport("duktape", CallingConvention = CallingConvention.Cdecl, EntryPoint = "public_duk_xmove_top")]
            static public extern void public_duk_xmove_top(SafeContextHandle to_ctx, SafeContextHandle from_ctx, int count);
        }
    }
}