using Klijent.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Klijent.Commands
{
    class AddNewEpisodeCommand:ICommand
    {
        private AddEpisodeWindowViewModel addSWVM;


        public AddNewEpisodeCommand(AddEpisodeWindowViewModel addSWVM)
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
            return this.addSWVM.CanAddEpisode;
        }

        public void Execute(object parameter)
        {
            this.addSWVM.AddEpisode();
        }
    }
}
