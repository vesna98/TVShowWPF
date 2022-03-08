using Common;
using Common.Models;
using Klijent.Commands;
using Klijent.View;
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
    class ShowEpisodesWindowViewModel:INotifyPropertyChanged
    {
        public ShowEpisodesWindowViewModel(Season season)
        {
            AddEpisodeWindowCommand = new AddEpisodeWindowCommand(this);
            RefreshEpisodesCommand = new RefreshEpisodesCommand(this);
            DeleteEpisodeCommand = new DeleteEpisodeCommand(this);
            ChangeEpisodeWindowCommand = new ChangeEpisodeWindowCommand(this);

            ShowSeason = season;
            BindingEpisode = ConnectChannel.Instance.EpisodeProxy.GetAllEpisodes(ShowSeason.IDseason);
        }

        public Episode ChoosedEpisode { get; set; }

        public Window Window { get; set; }
        //public TvShow ShowTvShow { get; set; }
        public Season ShowSeason { get; set; }

        private List<Episode> bindingEpisode;
        public List<Episode> BindingEpisode
        {
            get
            {
                return bindingEpisode;
            }
            set
            {
                bindingEpisode = value;
                OnPropertyChanged("BindingEpisode");
            }
        }
        public ICommand AddEpisodeWindowCommand
        {
            get;
            private set;
        }

        public ICommand RefreshEpisodesCommand
        {
            get;
            private set;
        }
        public ICommand DeleteEpisodeCommand
        {
            get;
            private set;
        }
        public ICommand ChangeEpisodeWindowCommand
        {
            get;
            private set;
        }
        public bool CanAddEpisodeWindow
        {
            get
            {
                return true;
            }
        }


        public void AddEpisodeWindow()
        {
            try
            {
                Logger.Instance.log.Info("Opened new add episode window.");
                new AddEpisodeWindow(ShowSeason).Show();

            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                MessageBox.Show(Window, "Can't connect on server.");
            }
        }

        public void RefreshEpisodes()
        {
            try
            {
                Logger.Instance.log.Info("Refreshed episodes view.");
                BindingEpisode = ConnectChannel.Instance.EpisodeProxy.GetAllEpisodes(ShowSeason.IDseason);
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                MessageBox.Show(Window, "Can't connect on server.");
            }
        }
        public bool CanDeleteEpisode
        {
            get
            {
                if (ChoosedEpisode == null)
                {
                    return false;
                }
                return true;
            }
        }


        public void DeleteCurentEpisode()
        {
            try
            {
                Logger.Instance.log.Info("Deleted selected episode.");
                ConnectChannel.Instance.EpisodeProxy.DeleteEpisode(ChoosedEpisode);
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                MessageBox.Show(Window, "Can't connect on server.");
            }
        }
        public bool CanChangeEpisodeWindow
        {
            get
            {
                if (ChoosedEpisode == null)
                {
                    return false;
                }
                return true;
            }
        }


        public void ChangeEpisodeWindow()
        {
            try
            {
                Logger.Instance.log.Info("Opened window for changing episode.");
                Window w = new ChangeEpisodeWindow();
                w.DataContext = new ChangeEpisodeWindowViewModel() { Window = w, ChangeE = this.ChoosedEpisode };
                w.ShowDialog();

            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                MessageBox.Show(Window, "Can't connect on server.");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
