using Klijent.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Klijent.Commands
{
    public class ChangeUserCommand:ICommand
    {
        private ProfileWindowViewModel change_wvm;

        public ChangeUserCommand(ProfileWindowViewModel change_wvm)
        {
            this.change_wvm = change_wvm;
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
            return this.change_wvm.CanChangeUser;
        }

        public void Execute(object parameter)
        {
            this.change_wvm.ChangeCurrentUser();
        }
    }
}
