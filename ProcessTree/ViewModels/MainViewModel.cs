using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Utilities.Commands;
using Utilities.ViewModels;
using ProcessTree.Models;

namespace ProcessTree.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private ICollection<ProcessModel> processes;
        private ICommand closeProcess;
        private ICommand startProcess;
        private ICommand refreshTreeView;

        public MainViewModel() 
        {
            processes = ProcessesReader.GetProcesses();
           
        }

        public ICollection<ProcessModel> Processes => processes;
    }
}