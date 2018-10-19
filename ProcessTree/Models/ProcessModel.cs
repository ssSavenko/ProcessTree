using System.Collections.Generic;
using System.Diagnostics;

namespace ProcessTree.Models
{
    public class ProcessModel
    {
        private int baseProcessId;
        private int id;
        private string name;
        private ICollection<ProcessModel> subprocesses;

        public ProcessModel(Process process, int parentId)
        {
            name = process.ProcessName;
            id = process.Id;
            baseProcessId = parentId;
            subprocesses = new List<ProcessModel>();
        }

        public int BaseProcess => baseProcessId;

        public int Id => id;

        public string Name => name;

        public IEnumerable<ProcessModel> SubProcesses => subprocesses;

        public void AddSubprocess(ProcessModel process)
        {
            subprocesses.Add(process);
        }
    }
}