using Klijent.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Klijent.Commands
{
    class AddSeasonWindowCommand:ICommand
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

        private ShowSeasonsWindowViewModel sswm;

        public AddSeasonWindowCommand(ShowSeasonsWindowViewModel sswm)
        {
            this.sswm = sswm;
        }

        public bool CanExecute(object parameter)
        {
            return sswm.CanAddSeasonWindow;
        }

        public void Execute(object parameter)
        {
            sswm.AddSeasonWindow();
        }
    }
}
