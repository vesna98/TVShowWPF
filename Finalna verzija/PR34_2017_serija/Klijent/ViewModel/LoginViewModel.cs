using Common;
using Common.Models;
using Klijent.ClientHelper;
using Klijent.View;
using Klijent.WPFHelper;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Klijent.ViewModel
{
    class LoginViewModel:BindableBase
    {
        public Window Window { get; set; }
        private string username;
        private string password;
        //private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        public MyICommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new MyICommand(OnLogin);
        }

        public void OnLogin(object parameter)
        {
            Logger.Instance.log.Info("The program has started.");

            if (String.IsNullOrWhiteSpace(Username))
            {
                MessageBox.Show("Niste uneli korisnicko ime.");
                Logger.Instance.log.Warn("Empty username.");
            }

            if (String.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Niste uneli lozinku.");
                Logger.Instance.log.Warn("Empty password.");
            }
           
            if (!String.IsNullOrWhiteSpace(Username) && !String.IsNullOrWhiteSpace(Password))
            {
                User user = ConnectChannel.Instance.userProxy.Login(Username);

                if (user == null || user.Password != Password)
                {
                    MessageBox.Show("Niste uneli ispravne podatke.");
                    Logger.Instance.log.Error("Wrong username or password");
                }
                else
                {
                    Logger.Instance.log.Info("The user has logged  in successfuly.");
                    CurrentUser.Instance = user;
                    new MainWindow().Show();
                    Window.Close();
                }
            }
        }
    }
}
