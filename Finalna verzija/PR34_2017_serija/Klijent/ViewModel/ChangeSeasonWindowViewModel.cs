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
    class ChangeSeasonWindowViewModel:INotifyPropertyChanged
    {
        public ChangeSeasonWindowViewModel()
        {
            ChangeSeasonCommand = new ChangeSeasonCommand(this);
            //ChangeS = new Season();
        }
        public Window Window { get; set; }
        public Season ChangeS { get; set; }
        public ICommand ChangeSeasonCommand
        {
            get;
            private set;
        }
        public bool CanChangeSeason
        {
            get
            {
                return !String.IsNullOrWhiteSpace(ChangeS.Number.ToString());
            }
        }


        public void ChangeCurrentSeason()
        {
            try
            {
                int broj = 0;//trebace i provera postojji li pre dodavanja
                if (int.TryParse(ChangeS.Number.ToString(), out broj) && broj >= 0)
                {
                    //
                    if (ConnectChannel.Instance.SeasonProxy.ChangeSeason(ChangeS))
                    {
                        Logger.Instance.log.Info("Chaged season.");
                        Window.Close();
                    }
                    else
                    {
                        Logger.Instance.log.Warn("Could not change season,index exists.");
                        MessageBox.Show("Postoji vec sezona sa tim brojem.");
                    }
                }
                else
                {
                    Logger.Instance.log.Warn("Could not change season,negative index.");
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
