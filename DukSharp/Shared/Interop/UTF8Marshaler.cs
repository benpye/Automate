using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace DukSharp.Interop
{
    class UTF8Marshaler : ICustomMarshaler
    {
        public void CleanUpManagedData(object managedObj)
        {
            return;
        }

        public void CleanUpNativeData(IntPtr nativeData)
        {
            if (nativeData != IntPtr.Zero)
                Marshal.FreeHGlobal(nativeData);
        }

        public int GetNativeDataSize()
        {
            return IntPtr.Size;
        }

        public IntPtr MarshalManagedToNative(object managedObj)
        {
            if (managedObj == null)
                return IntPtr.Zero;
            else if (!(managedObj is string))
                throw new MarshalDirectiveException($"{nameof(UTF8Marshaler)} must be used on a string.");

            return MarshalHelper.StringToUTF8(managedObj as string);
        }

        public object MarshalNativeToManaged(IntPtr nativeData)
        {
            return MarshalHelper.StringFromUTF8(nativeData);
        }

        private static ICustomMarshaler marshaller = null;

        public static ICustomMarshaler GetInstance(string cookie)
        {
            if (marshaller == null)
                marshaller = new UTF8Marshaler();

            return marshaller;
        }
    }
}
