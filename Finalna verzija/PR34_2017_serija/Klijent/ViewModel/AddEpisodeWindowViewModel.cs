using Common;
using Common.Models;
using Klijent.Commands;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Klijent.ViewModel
{
    class AddEpisodeWindowViewModel
    {
        public AddEpisodeWindowViewModel()
        {
            AddNewEpisodeCommand = new AddNewEpisodeCommand(this);
        }
        public Window Window { get; set; }
       
        public string Title { get; set; }
        public string Plot { get; set; }
        public string Index { get; set; }
        public Season Idseason { get; set; }
        public ICommand AddNewEpisodeCommand
        {
            get;
            private set;
        }

        public bool CanAddEpisode
        {

            get
            {
                return !String.IsNullOrWhiteSpace(Title) && !String.IsNullOrWhiteSpace(Plot) && !String.IsNullOrWhiteSpace(Index);
            }
        }
        public void AddEpisode()
        {
            try
            {
                int broj = 0;
                if (int.TryParse(Index, out broj) && broj >= 0)
                {
                    Logger.Instance.log.Info("Added episode for season.");
                    Episode episode = new Episode();
                    episode.Index = broj;
                    episode.PlotOutline = Plot;
                    episode.Title = Title;
                    ConnectChannel.Instance.EpisodeProxy.AddEpisode(episode,Idseason.IDseason,Idseason.TvShow);
                    Window.Close();
                }
                else
                {
                    //poruka da mora biti broj
                    Logger.Instance.log.Warn("Could not add episode.");
                }


            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                MessageBox.Show(Window, "Can't connect on server.");
            }
        }
    }
}
