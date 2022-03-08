using Klijent.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Klijent.Commands
{
    class ChangeTvShowWindowCommand: ICommand
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

        private MainWindowViewModel mwmv;

        public ChangeTvShowWindowCommand(MainWindowViewModel mwmv)
        {
            this.mwmv = mwmv;
        }

        public bool CanExecute(object parameter)
        {
            return mwmv.CanChangeTvShow;
        }

        public void Execute(object parameter)
        {
            mwmv.ChangeTvShow();
        }
    }
}
