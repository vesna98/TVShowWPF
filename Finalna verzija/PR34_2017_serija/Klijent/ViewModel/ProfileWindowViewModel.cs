using Common.Models;
using Klijent.ClientHelper;
using Klijent.View;
using Klijent.WPFHelper;
using Klijent.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Common;

namespace Klijent.ViewModel
{
    public class ProfileWindowViewModel : INotifyPropertyChanged
    {
        public ProfileWindowViewModel()
        {
            ChangeUserCommand = new ChangeUserCommand(this);
            ChangeUser = CurrentUser.Instance;
            //Refresh();
           // ContentHandler.Refresh();
        }

        public Window Window { get; set; }
        public User ChangeUser { get; set; }
        //private User changeUser;
        //public User ChangeUser
        //{
        //    get { return changeUser; }
        //    set
        //    {
        //        changeUser = value;
        //        OnPropertyChanged("ChangeUser");
        //    }
        //}


        public ICommand ChangeUserCommand
        {
            get;
            private set;
        }


        public bool CanChangeUser
        {
            get
            {
                return !String.IsNullOrWhiteSpace(ChangeUser.Name) &&
                        !String.IsNullOrWhiteSpace(ChangeUser.Lastname);
            }
        }


        public void ChangeCurrentUser()
        {
            

            if (!ChangeUser.Name.Trim().Equals("") && !ChangeUser.Lastname.Trim().Equals("") )
            {
                if (ConnectChannel.Instance.userProxy.ChangeUser(ChangeUser))
                {
                    CurrentUser.Instance.Name = ChangeUser.Name;
                    CurrentUser.Instance.Lastname = ChangeUser.Lastname;
                    Logger.Instance.log.Info("Changed profile information.");
                    Window.Close();
                }
                else
                {
                    Logger.Instance.log.Error("Could not chage profile information(user doesnt exits).");
                    MessageBox.Show("Korisnik vise ne postoji", "Information", MessageBoxButton.OK, MessageBoxImage.Error);
                    new Login().Show();
                    CurrentUser.Instance = null;
                    Window.Close();
                }
            }
            else
            {
                Logger.Instance.log.Warn("Could not change profile information,empty inputs.");
                MessageBox.Show("Polja nisu dobro popunjena!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);

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
