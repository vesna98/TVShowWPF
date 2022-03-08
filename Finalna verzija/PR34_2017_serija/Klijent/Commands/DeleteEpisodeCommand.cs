using Klijent.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Klijent.Commands
{
    class DeleteEpisodeCommand:ICommand
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

        private ShowEpisodesWindowViewModel sswvm;

        public DeleteEpisodeCommand(ShowEpisodesWindowViewModel sswvm)
        {
            this.sswvm = sswvm;
        }


        public bool CanExecute(object parameter)
        {
            return this.sswvm.CanDeleteEpisode;
        }

        public void Execute(object parameter)
        {
            this.sswvm.DeleteCurentEpisode();
        }
    }
}
