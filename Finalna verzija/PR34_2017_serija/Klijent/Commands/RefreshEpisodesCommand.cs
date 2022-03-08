using Klijent.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Klijent.Commands
{
    class RefreshEpisodesCommand:ICommand
    {
        private ShowEpisodesWindowViewModel hwvm;

        public RefreshEpisodesCommand(ShowEpisodesWindowViewModel hwvm)
        {
            this.hwvm = hwvm;
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
            return true;
        }

        public void Execute(object parameter)
        {
            this.hwvm.RefreshEpisodes();
        }
    }
}
