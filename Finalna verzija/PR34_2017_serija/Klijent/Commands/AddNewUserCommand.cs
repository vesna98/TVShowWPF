using Klijent.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Klijent.Commands
{
    public class AddNewUserCommand:ICommand
    {
        private AddUserWindowViewModel addUser;


        public AddNewUserCommand(AddUserWindowViewModel addUser)
        {
            this.addUser = addUser;
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
            return this.addUser.CanAddNewUser;
        }

        public void Execute(object parameter)
        {
            this.addUser.AddNewUser();
        }
    }
}
