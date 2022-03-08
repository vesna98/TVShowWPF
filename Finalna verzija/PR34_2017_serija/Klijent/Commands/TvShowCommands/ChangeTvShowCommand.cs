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
    class ChangeTvShowCommand: IUndoRedoCommand
    {
        public TvShow CurrentTvShow { get; set; }
        public TvShow NewTvShow { get; set; }

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

        private ChangeTvShowWindowViewModel ctvwvm;

        public ChangeTvShowCommand(ChangeTvShowWindowViewModel ctvwvm)
        {
            this.ctvwvm = ctvwvm;
            CurrentTvShow = new TvShow();
            NewTvShow = new TvShow();
        }


        public bool CanExecute(object parameter)
        {
            return this.ctvwvm.CanChangeTV;
        }

        public void Execute(object parameter)
        {
            this.ctvwvm.ChangeCurrentTV(CurrentTvShow);
        }

        public void UnExecute(object parametar)
        {
            this.ctvwvm.ChangeCurrentTV(NewTvShow);
        }
    }
}
