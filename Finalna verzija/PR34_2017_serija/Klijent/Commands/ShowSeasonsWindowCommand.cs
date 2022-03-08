using Klijent.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Klijent.Commands
{
    class ShowSeasonsWindowCommand:ICommand
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

        public ShowSeasonsWindowCommand(MainWindowViewModel mwmv)
        {
            this.mwmv = mwmv;
        }

        public bool CanExecute(object parameter)
        {
            return mwmv.CanShowSeasonsWindow;
        }

        public void Execute(object parameter)
        {
            mwmv.ShowSeasonsWindow();
        }
    }
}
