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
    class ChangeEpisodeWindowViewModel:INotifyPropertyChanged
    {
        public ChangeEpisodeWindowViewModel()
        {
            ChangeEpisodeCommand = new ChangeEpisodeCommand(this);
            //ChangeS = new Season();
        }
        public Window Window { get; set; }
        public Episode ChangeE { get; set; }
        public ICommand ChangeEpisodeCommand
        {
            get;
            private set;
        }
        public bool CanChangeEpisode
        {
            get
            {
                return !String.IsNullOrWhiteSpace(ChangeE.Title) && !String.IsNullOrWhiteSpace(ChangeE.PlotOutline) && !String.IsNullOrWhiteSpace(ChangeE.Index.ToString());
            }
        }


        public void ChangeCurrentEpisode()
        {
            try
            {
                int broj = 0;//trebace i provera postojji li pre dodavanja
                if (int.TryParse(ChangeE.Index.ToString(), out broj) && broj >= 0)
                {
                    //
                    if (ConnectChannel.Instance.EpisodeProxy.ChangeEpisode(ChangeE, ChangeE.Sezonaid))
                    {
                        Logger.Instance.log.Info("Changed episode");
                        Window.Close();
                    }
                    else
                    {
                        Logger.Instance.log.Warn("Could not change episode,index exists.");
                        MessageBox.Show("Postoji vec epizoda sa tim brojem.");
                    }
                }
                else
                {
                    Logger.Instance.log.Warn("Could not change episode,negative index");
                    MessageBox.Show("Broj epizode mora biti pozitivan broj.");
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
