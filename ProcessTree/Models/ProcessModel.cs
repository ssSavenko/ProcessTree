using System.Collections.Generic;
using System.Diagnostics;

namespace ProcessTree.Models
{
    public class ProcessModel
    {
        private readonly int baseProcessId;
        private readonly int id;
        private readonly string name;
        private readonly ICollection<ProcessModel> subprocesses = new List<ProcessModel>();

        public ProcessModel(Process process, int parentId)
        {
            name = process.ProcessName;
            id = process.Id;
            baseProcessId = parentId;
        }

        public int BaseProcess => baseProcessId;

        public int Id => id;

        public string Name => name;

        public IEnumerable<ProcessModel> Subprocesses => subprocesses;

        public bool DeleteSubProcessById(int processID)
        {
            bool isProcessDeleted = false;

            foreach(var subprocess in subprocesses)
            {
                if(subprocess.Id == processID)
                {
                    subprocesses.Remove(subprocess);
                    isProcessDeleted = true;
                }
                else
                {
                    isProcessDeleted = subprocess.DeleteSubProcessById(processID);
                    if (isProcessDeleted)
                    {
                        break;
                    }
                }
            }
            return isProcessDeleted;
        }

        public void AddSubprocess(ProcessModel process)
        {
            subprocesses.Add(process);
        }
    }
}