using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Diagnostics;

namespace ProcessTree.Models
{
    public class ProcessModel
    {
        private int baseProcess;
        private int id;
        private string name;
        private ICollection<ProcessModel> subprocesses;

        public ProcessModel(Process process)
        {
            name = process.ProcessName;
            id = process.Id;
            baseProcess = process.BasePriority;
            subprocesses = new List<ProcessModel>();
        }

        public int BaseProcess => baseProcess;

        public int Id => id;

        public string Name => name;

        public IEnumerable<ProcessModel> SubProcesses => subprocesses;

        public void AddSubprocess(ProcessModel process)
        {
            subprocesses.Add(process);
        }
    }
}