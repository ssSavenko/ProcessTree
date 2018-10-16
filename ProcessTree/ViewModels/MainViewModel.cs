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
using System.ComponentModel;
using System.Windows;

namespace ProcessTree.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private ICommand closeProcess;
        private string textBoxText;
        private ICollection<ProcessModel> processes;
        private ICommand refreshTreeView;
        private ICommand startProcess;
        private string selectedProcess = "";

        public MainViewModel()
        {
            processes = ProcessesReader.GetProcesses();

            refreshTreeView = new DelegateCommand(RefreshTreeView);
            startProcess = new DelegateCommand(OpenProcess);
            closeProcess = new DelegateCommand(CloseProcess);
        }

        public string ProcessName
        {
            get { return textBoxText; }
            set { textBoxText = value; }
        }

        public ICollection<ProcessModel> Processes => processes;

        public string SelectedProcess
        {
            get { return selectedProcess; }
            set { selectedProcess = value; }
        }

        public ICommand StartProcess => startProcess;

        public ICommand StopProcess => closeProcess;

        public ICommand RefreshData => refreshTreeView;
        
        public void CloseProcess()
        {

        }

        public void OpenProcess()
        {
            try
            {
                Process.Start(textBoxText, null);
            }
            catch (Win32Exception e) { }
        }

        public void RefreshTreeView()
        {
            processes = ProcessesReader.GetProcesses();
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Processes)));
        }
    }
}