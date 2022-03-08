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
    class DeleteTvShowCommand: IUndoRedoCommand
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
        private TvShow tvShow;
        private MainWindowViewModel mwvm;

        public DeleteTvShowCommand(MainWindowViewModel mwvm)
        {
            this.mwvm = mwvm;
            tvShow = new TvShow();
        }


        public bool CanExecute(object parameter)
        {
            return this.mwvm.CanDeleteTvShow;
        }

        public void Execute(object parameter)
        {
            this.mwvm.DeleteCurentTvShow(ref tvShow);
        }

        public void UnExecute(object parametar)
        {
            this.mwvm.AddTvShow(ref tvShow);
        }
    }
}
