using System.Collections.Generic;
using System.Windows.Input;
using System.Diagnostics;
using Utilities.Commands;
using Utilities.ViewModels;
using ProcessTree.Models;
using System.ComponentModel;

namespace ProcessTree.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private readonly ICommand closeProcess;
        private readonly ProcessesReader processesReader;
        private readonly ICommand refreshTreeView;
        private readonly ICommand startProcess;

        private ICollection<ProcessModel> processes;
        private string processName = "";
        private ProcessModel selectedProcess;

        public MainViewModel()
        {
            processesReader = new ProcessesReader();
            processes = processesReader.GetProcesses();

            refreshTreeView = new DelegateCommand(RefreshTreeView);
            startProcess = new DelegateCommand(OpenProcess);
            closeProcess = new DelegateCommand(CloseProcess);
        }

        public bool IsStartButtonEnable
        {
            get => ProcessName != "";
        }

        public bool IsStopButtonEnable
        {
            get => selectedProcess != null;
        }

        public ICollection<ProcessModel> Processes => processes;

        public string ProcessName
        {
            get { return processName; }
            set
            {
                processName = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsStartButtonEnable)));
            }
        }

        public ICommand RefreshData => refreshTreeView;

        public ProcessModel SelectedProcess
        {
            get { return selectedProcess; }
            set
            {
                selectedProcess = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsStopButtonEnable)));
            }
        }

        public ICommand StartProcess => startProcess;

        public ICommand StopProcess => closeProcess;

        public void CloseProcess()
        {
            Process.GetProcessById(selectedProcess.Id).Kill();
            SelectedProcess = null;
            RefreshData.Execute(null);
        }

        public void OpenProcess()
        {
            try
            {
                Process.Start(processName, null);
            }
            catch (Win32Exception e) { }
        }

        public void RefreshTreeView()
        {
            processes = processesReader.GetProcesses();
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Processes)));
        }
    }
}