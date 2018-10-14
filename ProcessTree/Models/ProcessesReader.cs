using System;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management;

namespace ProcessTree.Models
{
    internal class ProcessesReader
    {
        private ProcessesReader()
        { }

        public static ICollection<ProcessModel> GetProcesses()
        {
            var allProcesses = Process.GetProcesses();
            NativeMethods.MakeSnapshot();

            ICollection<ProcessModel> savedProcesses = new Collection<ProcessModel>();
            ICollection<Process> subProcesses = new Collection<Process>();
            bool isNotSubPocess = true;

            foreach (var currentProcess in allProcesses)
            {
                isNotSubPocess = true;
                foreach (var process in allProcesses)
                {
                    int paretntId = NativeMethods.ParentProcessId(currentProcess.Id);
                    if (process.Id == paretntId)
                    {
                        isNotSubPocess = false;
                        break;
                    }
                }

                if(!isNotSubPocess)
                {
                    subProcesses.Add(currentProcess);
                }
                else
                {
                    savedProcesses.Add(new ProcessModel(currentProcess));
                }
            }

            foreach(var process in savedProcesses)
            {
                SaveAllSubProcesses(process, subProcesses);
            }

            return savedProcesses;
        }

        private static void SaveAllSubProcesses(ProcessModel curentProcess, ICollection<Process> subprocesses)
        {
            foreach(var subprocess in subprocesses)
            {
                if (NativeMethods.ParentProcessId(subprocess.Id) == curentProcess.Id)
                {
                    curentProcess.AddSubprocess(new ProcessModel(subprocess));
                }
            }
            
            foreach(var subprocess in curentProcess.SubProcesses)
            {
                SaveAllSubProcesses(subprocess, subprocesses);
            }
        }
    }
}
