using Klijent.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Klijent.Commands
{
    class AddTvShowWindowCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private MainWindowViewModel mwmv;

        public AddTvShowWindowCommand(MainWindowViewModel mwmv)
        {
            this.mwmv = mwmv;
        }

        public bool CanExecute(object parameter)
        {
            return mwmv.CanAddTvShow;
        }

        public void Execute(object parameter)
        {
            mwmv.ShowAddTvShowWindow();
        }
    }
}
