using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ProcessTree.Models
{
    internal class ProcessesReader
    {
        private ProcessesReader()
        { }

        public static ICollection<ProcessModel> GetProcesses()
        {
            var Processes = Process.GetProcesses();
            NativeMethods.MakeSnapshot();

            ICollection<ProcessModel> allProcesses = new Collection<ProcessModel>();
            ICollection<ProcessModel> savedProcesses = new Collection<ProcessModel>();
            ICollection<ProcessModel> subProcesses = new Collection<ProcessModel>();
            bool isSubProcess;

            foreach (var process in Processes)
            {
                allProcesses.Add(new ProcessModel(process, NativeMethods.ParentProcessId(process.Id)));
            }

            foreach (var currentProcess in allProcesses)
            {
                isSubProcess = false;

                if (currentProcess.Id == currentProcess.BaseProcess)
                {
                    savedProcesses.Add(currentProcess);
                    continue;
                }
                foreach (var process in allProcesses)
                {
                    if (currentProcess.BaseProcess == process.Id)
                    {
                        subProcesses.Add(currentProcess);
                        isSubProcess = true;
                        break;
                    }
                }
                if (!isSubProcess)
                {
                    savedProcesses.Add(currentProcess);
                }
            }
            
            foreach (var process in savedProcesses)
            {
                SaveAllSubProcesses(process, subProcesses);
            }

            return savedProcesses;
        }

        private static void SaveAllSubProcesses(ProcessModel curentProcess, ICollection<ProcessModel> subprocesses)
        {
            foreach (var subprocess in subprocesses)
            {
                if (curentProcess.Id == subprocess.BaseProcess)
                {
                    curentProcess.AddSubprocess(subprocess);
                }
            }

            foreach (var subprocess in curentProcess.SubProcesses)
            {
                SaveAllSubProcesses(subprocess, subprocesses);
            }
        }
    }
}