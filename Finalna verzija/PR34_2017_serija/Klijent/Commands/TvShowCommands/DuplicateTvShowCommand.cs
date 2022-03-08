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
    public class DuplicateTvShowCommand: IUndoRedoCommand
    {
        private MainWindowViewModel mwvm;
        public TvShow TvShow { get; set; }
        public int PreviousId { get; set; }
        public DuplicateTvShowCommand(MainWindowViewModel mwvm)
        {
            this.mwvm = mwvm;
            TvShow = new TvShow();
            PreviousId = 0;
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
            return this.mwvm.CanDuplicateTvShow;
        }

        public void Execute(object parameter)
        {
            this.mwvm.DuplicateCurrent(TvShow);
        }

        public void UnExecute(object parametar)
        {
            this.mwvm.DeleteDuplicate(TvShow);
        }
    }
}
