using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ProcessTree.Models
{
    internal static class NativeMethods
    {
        private const string DllName = @"..\..\..\Debug\NativeLibrary.dll";

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ParentProcessId(int processId);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MakeSnapshot();
    }
}
