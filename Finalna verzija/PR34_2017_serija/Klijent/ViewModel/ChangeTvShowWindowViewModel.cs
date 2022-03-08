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
    class ChangeTvShowWindowViewModel:INotifyPropertyChanged
    {
        private ITvShowCommandExecutor tvShowCommandExecutor;
        public ChangeTvShowWindowViewModel(ITvShowCommandExecutor tvShowCommandExecutor, TvShow tvShow)
        {
            ChangeTvShowCommand = new ChangeTvShowCommand(this);
            this.tvShowCommandExecutor = tvShowCommandExecutor;
            ChangeTvShowCommand command = (ChangeTvShowCommand)ChangeTvShowCommand;

            command.CurrentTvShow.Description = tvShow.Description;
            command.CurrentTvShow.Name = tvShow.Name;
            command.CurrentTvShow.EpisodeCnt = tvShow.EpisodeCnt;
            command.CurrentTvShow.SeasonCnt = tvShow.SeasonCnt;
            command.CurrentTvShow.Showid = tvShow.Showid;
            command.CurrentTvShow.Genres.AddRange(tvShow.Genres);
            command.CurrentTvShow.Sezone.AddRange(tvShow.Sezone);


        }

        public Window Window { get; set; }
        public TvShow ChangeTvShow { get; set; }
        public string StariZanrovi { get; set; }

        public IUndoRedoCommand ChangeTvShowCommand
        {
            get;
            private set;
        }
        private bool[] checkBoxs = new bool[5] { false, false, false, false,false };
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

        public bool CanChangeTV
        {
            get
            {
                return !String.IsNullOrWhiteSpace(ChangeTvShow.Name) && !String.IsNullOrWhiteSpace(ChangeTvShow.Description) && (CheckBoxes[0] || CheckBoxes[1] || CheckBoxes[2] || CheckBoxes[3] || CheckBoxes[4]);//!String.IsNullOrWhiteSpace(ChangePlayList.PlayListName);
            }
        }


        public void ChangeCurrentTV(TvShow tvShow)
        {

            try
            {
                Logger.Instance.log.Info("Changed tv show.");
                ChangeTvShowCommand command = (ChangeTvShowCommand)ChangeTvShowCommand;

                if(command.NewTvShow.Showid == 0)
                {
                    command.NewTvShow.Description = tvShow.Description;
                    command.NewTvShow.Name = tvShow.Name;
                    command.NewTvShow.EpisodeCnt = tvShow.EpisodeCnt;
                    command.NewTvShow.SeasonCnt = tvShow.SeasonCnt;
                    command.NewTvShow.Showid = tvShow.Showid;
                    command.NewTvShow.Genres.AddRange(tvShow.Genres);
                    command.NewTvShow.Sezone.AddRange(tvShow.Sezone);

                    tvShow.Name = ChangeTvShow.Name;
                    tvShow.Description = ChangeTvShow.Description;

                    tvShowCommandExecutor.Insert(ChangeTvShowCommand);
                }

                tvShow.Genres.Clear();
                if (CheckBoxes[0])
                    tvShow.Genres.Add(new Genre("DRAMA"));
                if (CheckBoxes[1])
                    tvShow.Genres.Add(new Genre("ROMANCE"));
                if (CheckBoxes[2])
                    tvShow.Genres.Add(new Genre("CRIME"));
                if (CheckBoxes[3])
                    tvShow.Genres.Add(new Genre("MISTERY"));
                if (CheckBoxes[4])
                    tvShow.Genres.Add(new Genre("COMEDY"));

                

                ConnectChannel.Instance.ShowProxy.ChangeShow(tvShow);

                Window.Close();
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                MessageBox.Show(Window, "Can't connect to server.");
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
