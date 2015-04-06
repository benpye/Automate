using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DukSharp.Interop.SafeHandles
{
    class SafeContextHandle : SafeHandle
    {
        internal SafeContextHandle() : base(IntPtr.Zero, true) { }

        public override bool IsInvalid => IntPtr.Zero == handle;

        protected override bool ReleaseHandle()
        {
            duktape.duk_destroy_heap(handle);
            return true;
        }
    }
}
