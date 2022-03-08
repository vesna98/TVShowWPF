using Klijent.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Klijent.Commands
{
    class ShowEpisodesWindowCommand:ICommand
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


        private ShowSeasonsWindowViewModel mwmv;

        public ShowEpisodesWindowCommand(ShowSeasonsWindowViewModel mwmv)
        {
            this.mwmv = mwmv;
        }

        public bool CanExecute(object parameter)
        {
            return mwmv.CanShowEpisodesWindow;
        }

        public void Execute(object parameter)
        {
            mwmv.ShowEpisodesWindow();
        }
    }
}
