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
    class ShowSeasonsWindowViewModel : INotifyPropertyChanged
    {
        public ShowSeasonsWindowViewModel(TvShow show)
        {
            AddSeasonWindowCommand = new AddSeasonWindowCommand(this);
            RefreshSeasonsCommand = new RefreshSeasonsCommand(this);
            DeleteSeasonCommand = new DeleteSeasonCommand(this);
            ChangeSeasonWindowCommand = new ChangeSeasonWindowCommand(this);
            ShowEpisodesWindowCommand = new ShowEpisodesWindowCommand(this);
            ShowTvShow = show;

            BindingSeason = ConnectChannel.Instance.SeasonProxy.GetAllSeasons(show.Showid);
        }
        public Season ChoosedSeason { get; set; }

        public Window Window { get; set; }
        public TvShow ShowTvShow { get; set; }

        private List<Season> bindingSeason;
        public List<Season> BindingSeason
        {
            get
            {
                return bindingSeason;
            }
            set
            {
                bindingSeason = value;
                OnPropertyChanged("BindingSeason");
            }
        }
        public ICommand AddSeasonWindowCommand
        {
            get;
            private set;
        }
        public ICommand RefreshSeasonsCommand
        {
            get;
            private set;
        }
        public ICommand DeleteSeasonCommand
        {
            get;
            private set;
        }
        public ICommand ChangeSeasonWindowCommand
        {
            get;
            private set;
        }
        public ICommand ShowEpisodesWindowCommand
        {
            get;
            private set;
        }
        public bool CanAddSeasonWindow
        {
            get
            {
                return true;
            }
        }


        public void AddSeasonWindow()
        {
            try
            {
                Logger.Instance.log.Info("Opened window for adding season.");
                new AddSeasonWindow(ShowTvShow.Showid).Show();

            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                MessageBox.Show(Window, "Can't connect on server.");
            }
        }
        public void RefreshSeasons()
        {
            try
            {
                Logger.Instance.log.Info("Refreshed seasons view.");
                BindingSeason = ConnectChannel.Instance.SeasonProxy.GetAllSeasons(ShowTvShow.Showid);
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                MessageBox.Show(Window, "Can't connect on server.");
            }
        }

        public bool CanDeleteSeason
        {
            get
            {
                if (ChoosedSeason == null)
                {
                    return false;
                }
                return true;
            }
        }


        public void DeleteCurentSeason()
        {
            try
            {
                Logger.Instance.log.Info("Deleted selected season.");
                ConnectChannel.Instance.SeasonProxy.DeleteSeason(ChoosedSeason);
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                MessageBox.Show(Window, "Can't connect on server.");
            }
        }

        public bool CanChangeSeasonWindow
        {
            get
            {
                if (ChoosedSeason == null)
                {
                    return false;
                }
                return true;
            }
        }


        public void ChangeSeasonWindow()
        {
            try
            {
                Logger.Instance.log.Info("Opened change season window.");
                Window w = new ChangeSeasonWindow();
                w.DataContext = new ChangeSeasonWindowViewModel() { Window = w, ChangeS = this.ChoosedSeason };
                w.ShowDialog();

            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                MessageBox.Show(Window, "Can't connect on server.");
            }
        }

        public bool CanShowEpisodesWindow
        {
            get
            {
                if (ChoosedSeason == null)
                {
                    return false;
                }
                return true;
            }
        }


        public void ShowEpisodesWindow()
        {
            try
            {
                Logger.Instance.log.Info("Opened episodes window for selected season.");
                new ShowEpisodesWindow(ChoosedSeason).Show();
                // Window w = new ShowSeasonsWindow();
                // w.DataContext = new ShowSeasonsWindowViewModel() { Window = w, ShowTvShow = ChoosedTvShow };
                // w.DataContext = new AddNewTvShowWindowViewModel() { Window = w, TempUser = CurrentUser.Instance };
                //w.Show();

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
