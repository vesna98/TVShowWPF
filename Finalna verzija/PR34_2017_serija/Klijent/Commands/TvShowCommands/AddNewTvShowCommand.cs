using Common.Models;
using Klijent.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Klijent.Commands
{
    public class AddNewTvShowCommand: IUndoRedoCommand
    {
        private AddNewTvShowWindowViewModel addTVWVM;
        public TvShow NewTvShow { get; set; }
        public AddNewTvShowCommand(AddNewTvShowWindowViewModel addTVWVM)
        {
            this.addTVWVM = addTVWVM;
            NewTvShow = new TvShow();
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
            return this.addTVWVM.CanAddNewTvShow;
        }

        public void Execute(object parameter)
        {
            this.addTVWVM.AddNewTvShow(NewTvShow);
        }

        public void UnExecute(object parametar)
        {
            this.addTVWVM.RemoveNewTvShow(NewTvShow);
        }
    }
}
