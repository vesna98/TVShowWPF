using Common;
using Common.Models;
using Klijent.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Klijent.ViewModel
{
    class LogInfoWindowViewModel:INotifyPropertyChanged
    {
        public LogInfoWindowViewModel()
        {
            RefreshLogsCommand = new RefreshLogsCommand(this);
            BindingLog = Logger.Instance.ReadLogs(@"C:\Users\HP\Desktop\PR34_2017_serija\ClientLogger.txt");
        }

        public Window Window { get; set; }
       

        private List<LogModel> bindingLog;
        public List<LogModel> BindingLog
        {
            get
            {
                return bindingLog;
            }
            set
            {
                bindingLog = value;
                OnPropertyChanged("BindingLog");
            }
        }
        public ICommand RefreshLogsCommand
        {
            get;
            private set;
        }

        public void RefreshLogs()
        {
            try
            {
                //BindingLog = Data.ReadLogs("../RollingFileAppender.txt"); //iz txt fajla cita
                BindingLog = Logger.Instance.ReadLogs(@"C:\Users\HP\Desktop\PR34_2017_serija\ClientLogger.txt");
                Logger.Instance.log.Info("Refreshed logger view.");
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                MessageBox.Show(Window, "Can't connect on server.");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
