using Klijent.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Klijent.Commands
{
    class RedoCommand:ICommand
    {
        private ITvShowCommandExecutor tvShowcommandExecutor;

        public RedoCommand(ITvShowCommandExecutor commandExecutor)
        {
            this.tvShowcommandExecutor = commandExecutor;
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
            return tvShowcommandExecutor.CanRedo();
        }

        public void Execute(object parameter)
        {
            tvShowcommandExecutor.Redo();
        }
    }
}
