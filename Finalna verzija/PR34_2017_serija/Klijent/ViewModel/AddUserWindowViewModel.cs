using Common;
using Common.Models;
using Klijent.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Klijent.ViewModel
{
    public class AddUserWindowViewModel:INotifyPropertyChanged
    {
        public AddUserWindowViewModel()
        {
            AddNewUserCommand = new AddNewUserCommand(this);
            NewUser = new User();
        }

        public Window Window { get; set; }
        public User NewUser { get; set; }

        private bool[] checkBoxs = new bool[2] { false, false };
        public bool[] CheckBoxes
        {
            get
            {
                return checkBoxs;
            }

            set
            {
                checkBoxs = value;
            }
        }



        public ICommand AddNewUserCommand
        {
            get;
            private set;
        }


        public bool CanAddNewUser
        {
            get
            {
                return !String.IsNullOrWhiteSpace(NewUser.Name) &&
                        !String.IsNullOrWhiteSpace(NewUser.Lastname) &&
                        !String.IsNullOrWhiteSpace(NewUser.Username) &&
                        !String.IsNullOrWhiteSpace(NewUser.Password)
                         && (CheckBoxes[0] || CheckBoxes[1]) /*&& ConnectChannel.Instance.userProxy..Equals("admin")*/;

            }
        }


        public void AddNewUser()
        {
            try
            {
                string[] roles = new string[2] { "admin", "user" };
                if (CheckBoxes[0])
                    NewUser.Role += roles[0];
                else
                    NewUser.Role += roles[1];

                Logger.Instance.log.Info("Added new user.");
                ConnectChannel.Instance.userProxy.AddUser(NewUser);
                Window.Close();
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                MessageBox.Show(Window, "Can't connect on server.");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
