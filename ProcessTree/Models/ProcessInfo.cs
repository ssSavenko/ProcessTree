using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ProcessTree.Models
{
    internal struct ProcessInfo
    {
        int id;
        int parentId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 200)]
        string name;

        public ProcessInfo(int id, int parentId, string name)
        {
            this.id = id;
            this.parentId = parentId;
            this.name = name;
        }
    };
}
