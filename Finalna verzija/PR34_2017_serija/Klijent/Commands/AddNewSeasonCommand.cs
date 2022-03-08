using Klijent.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Klijent.Commands
{
    class AddNewSeasonCommand:ICommand
    {
        private AddSeasonWindowViewModel addSWVM;


        public AddNewSeasonCommand(AddSeasonWindowViewModel addSWVM)
        {
            this.addSWVM = addSWVM;
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
            return this.addSWVM.CanAddSeason;
        }

        public void Execute(object parameter)
        {
            this.addSWVM.AddSeason();
        }
    }
}
