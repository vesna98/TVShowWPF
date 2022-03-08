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
    public class AddSeasonWindowViewModel
    {
        public AddSeasonWindowViewModel()
        {
            AddNewSeasonCommand = new AddNewSeasonCommand(this);
        }

        public Window Window { get; set; }
        
        public string Number { get; set; }
        public int IdTvShow { get; set; }


        public ICommand AddNewSeasonCommand
        {
            get;
            private set;
        }

        public int broj { get; set; }
        public bool CanAddSeason
        {
            
            get
            {
                return !String.IsNullOrWhiteSpace(Number) ;
            }
        }
        public void AddSeason()
        {
            try
            {
                int broj = 0;//trebace i provera postojji li pre dodavanja
                if (int.TryParse(Number, out broj) && broj >= 0)
                {
                    if (ConnectChannel.Instance.SeasonProxy.AddSeason(Number, IdTvShow))
                    {
                        Logger.Instance.log.Info("Added new season.");
                        Window.Close();
                    }
                    else
                    {
                        Logger.Instance.log.Warn("Could not add  new season,existing number.");
                        MessageBox.Show("Postoji vec taj broj sezone.");
                    }
                }
                else
                {
                    Logger.Instance.log.Warn("Could not add new season,negative number");
                    MessageBox.Show("Broj sezone mora biti pozitivan broj.");
                    //poruka da mora biti broj
                }


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
