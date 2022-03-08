using Klijent.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Klijent.Commands
{
    class ChangeSeasonCommand:ICommand
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

        private ChangeSeasonWindowViewModel cswvm;

        public ChangeSeasonCommand(ChangeSeasonWindowViewModel cswvm)
        {
            this.cswvm = cswvm;
        }


        public bool CanExecute(object parameter)
        {
            return this.cswvm.CanChangeSeason;
        }

        public void Execute(object parameter)
        {
            this.cswvm.ChangeCurrentSeason();
        }
    }
}
