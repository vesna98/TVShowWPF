using Klijent.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Klijent.Commands
{
    public class AddWindowCommand:ICommand
    {
        public event EventHandler CanExecuteChanged;
        private MainWindowViewModel hwmv;

        public AddWindowCommand(MainWindowViewModel hwmv)
        {
            this.hwmv = hwmv;
        }

        public bool CanExecute(object parameter)
        {
            return hwmv.CanAdd;
        }

        public void Execute(object parameter)
        {
            hwmv.Add();
        }
    }
}
