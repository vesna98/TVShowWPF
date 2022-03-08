using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Klijent
{
    public interface ITvShowCommandExecutor
    {
        void Insert(IUndoRedoCommand command);
        void Redo();
        void Undo();

        bool CanUndo();
        bool CanRedo();
    }
}