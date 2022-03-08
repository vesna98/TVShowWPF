using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klijent
{
    public class TvShowCommandExecutor : ITvShowCommandExecutor
    {
        public List<IUndoRedoCommand> CommandsList { get; set; }
        private int commandIndex;
        public TvShowCommandExecutor()
        {
            CommandsList = new List<IUndoRedoCommand>();
            commandIndex = -1;
        }

        public void Insert(IUndoRedoCommand command)
        {
            if (commandIndex < CommandsList.Count - 1)
            {
                CommandsList.RemoveRange(commandIndex, CommandsList.Count - commandIndex - 1);
            }

            CommandsList.Add(command);
            commandIndex++;
        }

        public void Undo()
        {

            CommandsList[commandIndex].UnExecute(null);
            commandIndex--;                                 
        }

        public void Redo()
        {
            commandIndex++;
            CommandsList[commandIndex].Execute(null);
        }

        public bool CanUndo()
        {
            if (commandIndex == -1)
            {
                return false;
            }

            return true;
        }

        public bool CanRedo()
        {
            if (commandIndex == CommandsList.Count - 1)
            {
                return false;
            }

            return true;
        }
    }
}
