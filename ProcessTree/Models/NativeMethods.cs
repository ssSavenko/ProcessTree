using System.Runtime.InteropServices;

namespace ProcessTree.Models
{
    internal static class NativeMethods
    {
        private const string DllName = @"..\..\..\Debug\NativeLibrary.dll";

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MakeSnapshot();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ParentProcessId(int processId);
    }
}