using Klijent.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Klijent.Commands
{
    class AddEpisodeWindowCommand:ICommand
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

        private ShowEpisodesWindowViewModel sewm;

        public AddEpisodeWindowCommand(ShowEpisodesWindowViewModel sewm)
        {
            this.sewm = sewm;
        }

        public bool CanExecute(object parameter)
        {
            return sewm.CanAddEpisodeWindow;
        }

        public void Execute(object parameter)
        {
            sewm.AddEpisodeWindow();
        }
    }
}
