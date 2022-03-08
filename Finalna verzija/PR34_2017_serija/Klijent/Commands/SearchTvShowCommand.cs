using Klijent.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Klijent.Commands
{
    class SearchTvShowCommand:ICommand
    {
        private MainWindowViewModel mwvm;

        public SearchTvShowCommand(MainWindowViewModel mwvm)
        {
            this.mwvm = mwvm;
        }

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

        public bool CanExecute(object parameter)
        {
            return mwvm.CanSearch;
        }

        public void Execute(object parameter)
        {
            mwvm.Search();
        }
    }
}
