using Klijent.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Klijent.Commands
{
    class ChangeSeasonWindowCommand:ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        private ShowSeasonsWindowViewModel sswvm;

        public ChangeSeasonWindowCommand(ShowSeasonsWindowViewModel sswvm)
        {
            this.sswvm = sswvm;
        }

        public bool CanExecute(object parameter)
        {
            return sswvm.CanChangeSeasonWindow;
        }

        public void Execute(object parameter)
        {
            sswvm.ChangeSeasonWindow();
        }
    }
}
