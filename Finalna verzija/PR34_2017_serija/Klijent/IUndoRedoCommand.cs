using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Klijent
{
    public interface IUndoRedoCommand : ICommand
    {
        void UnExecute(object parametar);
    }
}
