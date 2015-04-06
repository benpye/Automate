using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace DukSharp.Interop
{
    class UTF8ReturnMarshaler : ICustomMarshaler
    {
        public void CleanUpManagedData(object managedObj)
        {
            return;
        }

        public void CleanUpNativeData(IntPtr nativeData)
        {
            return;
        }

        public int GetNativeDataSize()
        {
            return IntPtr.Size;
        }

        public IntPtr MarshalManagedToNative(object managedObj)
        {
            throw new NotImplementedException();
        }

        public object MarshalNativeToManaged(IntPtr nativeData)
        {
            return MarshalHelper.StringFromUTF8(nativeData);
        }

        private static ICustomMarshaler marshaller = null;

        public static ICustomMarshaler GetInstance(string cookie)
        {
            if (marshaller == null)
                marshaller = new UTF8ReturnMarshaler();

            return marshaller;
        }
    }
}
