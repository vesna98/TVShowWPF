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
    public class AddNewTvShowWindowViewModel:INotifyPropertyChanged
    {
        private ITvShowCommandExecutor tvShowCommandExecutor;
        public Window Window { get; set; }
        public TvShow NewTvShow { get; set; }

        public User TempUser { get; set; }

        public IUndoRedoCommand AddNewTvShowCommand { get; private set; }


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
        public AddNewTvShowWindowViewModel(ITvShowCommandExecutor tvShowCommandExecutor)
        {
            AddNewTvShowCommand = new AddNewTvShowCommand(this);
            NewTvShow = new TvShow();
            TempUser = new User();
            this.tvShowCommandExecutor = tvShowCommandExecutor;
        }

        public bool CanAddNewTvShow
        {
            get
            {
                return !String.IsNullOrWhiteSpace(NewTvShow.Name) && !String.IsNullOrWhiteSpace(NewTvShow.Description) && (CheckBoxes[0] || CheckBoxes[1] || CheckBoxes[2] || CheckBoxes[3] || CheckBoxes[4]);
            }
        }


        public void AddNewTvShow(TvShow newTvShow)
        {
            try
            {
                if(newTvShow.Genres.Count == 0)
                {
                    AddGenres(newTvShow);
                }

                newTvShow.Showid = ConnectChannel.Instance.ShowProxy.AddShow(newTvShow, TempUser);
                Logger.Instance.log.Info("Added new tv show.");
                Window.Close();
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                MessageBox.Show(Window, "Can't connect on server.");
            }
        }

        private void AddGenres(TvShow newTvShow)
        {
            if (CheckBoxes[0])
                newTvShow.Genres.Add(new Genre("DRAMA"));
            if (CheckBoxes[1])
                newTvShow.Genres.Add(new Genre("ROMANCE"));
            if (CheckBoxes[2])
                newTvShow.Genres.Add(new Genre("CRIME"));
            if (CheckBoxes[3])
                newTvShow.Genres.Add(new Genre("MISTERY"));
            if (CheckBoxes[4])
                newTvShow.Genres.Add(new Genre("COMEDY"));

            newTvShow.Description = NewTvShow.Description;
            newTvShow.Name = NewTvShow.Name;

            tvShowCommandExecutor.Insert(this.AddNewTvShowCommand);
        }
        public void RemoveNewTvShow(TvShow newTvShow)
        {
            try
            {
                Logger.Instance.log.Info("Remove tv show.");
                ConnectChannel.Instance.ShowProxy.DeleteTvShow(newTvShow);
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
