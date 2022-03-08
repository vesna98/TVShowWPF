using Common;
using Common.Models;
using Klijent.ClientHelper;
using Klijent.Commands;
using Klijent.View;
using Klijent.WPFHelper;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Klijent.ViewModel
{
    public class MainWindowViewModel:INotifyPropertyChanged
    {
        public Window Window { get; set; }

        //komande
        public MyICommand LogoutCommand { get; set; }
        public MyICommand ProfileCommand { get; set; }

        private ITvShowCommandExecutor tvShowCommandExecutor;
       // private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public MainWindowViewModel()
        {
            LogoutCommand = new MyICommand(OnLogoutExecute);
            ProfileCommand = new MyICommand(OnProfileExecute);
            //------
            User = CurrentUser.Instance;
            AddWindowCommand = new AddWindowCommand(this);
            AddTvShowWindowCommand = new AddTvShowWindowCommand(this);
            ChangeTvShowWindowCommand = new ChangeTvShowWindowCommand(this);
            RefreshTvShowCommand = new RefreshTvShowCommand(this);
            DeleteTvShowCommand = new DeleteTvShowCommand(this);
            SearchTvShowCommand = new SearchTvShowCommand(this);
            DuplicateTvShowCommand = new DuplicateTvShowCommand(this);
            ShowSeasonsWindowCommand = new ShowSeasonsWindowCommand(this);
            tvShowCommandExecutor = new TvShowCommandExecutor();
            UndoCommand = new UndoCommand(tvShowCommandExecutor);
            RedoCommand = new RedoCommand(tvShowCommandExecutor);

            LogInfoWindowCommand = new LogInfoWindowCommand(this);

            BindingTvShow = ConnectChannel.Instance.ShowProxy.GetAllShows();


        }
        private List<TvShow> bindingTvShow;
        public TvShow ChoosedTvShow { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public List<TvShow> BindingTvShow
        {
            get
            {
                return bindingTvShow;
            }
            set
            {
                bindingTvShow = value;
                OnPropertyChanged("BindingTvShow");
            }
        }
        public ICommand ChangeTvShowWindowCommand
        {
            get;
            private set;
        }
        public ICommand LogInfoWindowCommand
        {
            get;
            private set;
        }
        public ICommand RefreshTvShowCommand
        {
            get;
            private set;
        }
        public IUndoRedoCommand DeleteTvShowCommand
        {
            get;
            private set;
        }
        public ICommand SearchTvShowCommand
        {
            get;
            private set;
        }

        public IUndoRedoCommand DuplicateTvShowCommand
        {
            get;
            private set;
        }
        public ICommand ShowSeasonsWindowCommand
        {
            get;
            private set;
        }
        private void OnProfileExecute(object obj)
        {
            new ProfileWindow().ShowDialog();
        }

        private void OnLogoutExecute(object obj)
        {
            new Login().Show();
            CurrentUser.Instance = null;
            Window.Close();
        }

        //***********
        
        public User User { get; set; }

        public ICommand AddWindowCommand
        {
            get;
            private set;
        }

        public ICommand AddTvShowWindowCommand
        {
            get;
            private set;
        }

        public bool CanAdd
        {
            get
            {
                if (User.Role.ToLower().Equals("admin"))
                {
                    return true;
                }
                return false;

            }
        }


        public void Add()
        {
            try
            {
                Logger.Instance.log.Info("Opended window to add a new user.");
                Window w = new AddUserWindow();
                w.DataContext = new AddUserWindowViewModel() { Window = w };
                w.Show();
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                MessageBox.Show(Window, "Can't connect to server.");
            }
        }

        public bool CanAddTvShow
        {
            get
            {
                return true;

            }
        }


        public void ShowAddTvShowWindow()
        {
            try
            {
                Logger.Instance.log.Info("Opended window to add a new tv show.");
                Window w = new AddTvShowWindow();
                w.DataContext = new AddNewTvShowWindowViewModel(this.tvShowCommandExecutor) { Window = w, TempUser = CurrentUser.Instance};
                w.Show();
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                MessageBox.Show(Window, "Can't connect on server.");
            }
        }

        public bool CanChangeTvShow
        {
            get
            {
                if (ChoosedTvShow == null)
                {
                    return false;
                }
                return true;

            }
        }


        public void ChangeTvShow()
        {
            try
            {
                Logger.Instance.log.Info("Opended window to change tv show.");
                new ChangeTvShowWindow(ChoosedTvShow, tvShowCommandExecutor).Show();

            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                MessageBox.Show(Window, "Can't connect on server.");
            }
        }
        public void RefreshTvShows()
        {
            try
            {
                Logger.Instance.log.Info("Refreshed tv show view.");
                BindingTvShow = ConnectChannel.Instance.ShowProxy.GetAllShows();
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                MessageBox.Show(Window, "Can't connect on server.");
            }
        }

        public bool CanDeleteTvShow
        {
            get
            {
                if (ChoosedTvShow == null)
                {
                    return false;
                }
                return true;
            }
        }


        public void DeleteCurentTvShow(ref TvShow tvShow)
        {
            if(tvShow.Showid == 0)
            {
                //deep copy
                tvShow.Showid = ChoosedTvShow.Showid;
                tvShow.Description = ChoosedTvShow.Description;
                tvShow.EpisodeCnt = ChoosedTvShow.EpisodeCnt;
                tvShow.Name = ChoosedTvShow.Name;
                tvShow.SeasonCnt = ChoosedTvShow.SeasonCnt;
                tvShow.Genres.AddRange(ChoosedTvShow.Genres);
                tvShow.Sezone.AddRange(ChoosedTvShow.Sezone);

                this.tvShowCommandExecutor.Insert(DeleteTvShowCommand);
            }

            try
            {
                Logger.Instance.log.Info("Deleted selected tv show.");
                ConnectChannel.Instance.ShowProxy.DeleteTvShow(tvShow);
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                MessageBox.Show(Window, "Can't connect on server.");
            }
        }

        public void AddTvShow(ref TvShow tvShow)
        {
            try
            {
                Logger.Instance.log.Info("Added tv show.");
                tvShow.Showid = ConnectChannel.Instance.ShowProxy.AddShow(tvShow, User);
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                MessageBox.Show(Window, "Can't connect on server.");
            }
        }
        public string SearchText { get; set; }
        public bool CanSearch
        {
            get
            {
                if (SearchText == null)
                {
                    return false;
                }
                return true;
            }
        }


        public void Search()
        {
            try
            {
                Logger.Instance.log.Info("Searched tv shows.");
                BindingTvShow = ConnectChannel.Instance.ShowProxy.GetAllShows();//svaki put da se dobiju svi podaci,da se moze vise puta pretraziti
                List<TvShow> pretraga = new List<TvShow>();
                foreach (var show in BindingTvShow)
                {
                    if (show.Name.ToUpper().Contains(SearchText.ToUpper()))
                    {
                        pretraga.Add(show);
                    }
                }
                BindingTvShow = pretraga;
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                MessageBox.Show(Window, "Can't connect on server.");
            }
        }

        public bool CanDuplicateTvShow
        {
            get
            {
                if (ChoosedTvShow == null)
                {
                    return false;
                }
                return true;
            }
        }


        public void DuplicateCurrent(TvShow tvShow)
        {
            try
            {
                Logger.Instance.log.Info("Duplicated tv show.");
                if (tvShow.Showid == 0)
                {
                    tvShow.Name = ChoosedTvShow.Name;
                    tvShow.Description = ChoosedTvShow.Description;
                    tvShow.EpisodeCnt = ChoosedTvShow.EpisodeCnt;
                    tvShow.SeasonCnt = ChoosedTvShow.SeasonCnt;
                    tvShow.Showid = ChoosedTvShow.Showid;
                    tvShow.Genres.AddRange(ChoosedTvShow.Genres);
                    tvShow.Sezone.AddRange(ChoosedTvShow.Sezone);

                    tvShowCommandExecutor.Insert(this.DuplicateTvShowCommand);
                    DuplicateTvShowCommand command = (DuplicateTvShowCommand)DuplicateTvShowCommand;
                    command.PreviousId = tvShow.Showid;
                }
                else
                {
                    DuplicateTvShowCommand command = (DuplicateTvShowCommand)DuplicateTvShowCommand;
                    tvShow.Showid = command.PreviousId;
                }
                tvShow.Showid = ConnectChannel.Instance.ShowProxy.DuplicateShow(tvShow);
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                MessageBox.Show(Window, "Can't connect on server.");
            }
        }

        public void DeleteDuplicate(TvShow tvShow)
        {
            try
            {
                Logger.Instance.log.Info("Deleted duplicate.");
                ConnectChannel.Instance.ShowProxy.DeleteTvShow(tvShow);
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                MessageBox.Show(Window, "Can't connect on server.");
            }
        }
        public bool CanShowSeasonsWindow
        {
            get
            {
                if (ChoosedTvShow == null)
                {
                    return false;
                }
                return true;
            }
        }


        public void ShowSeasonsWindow()
        {
            try
            {
                Logger.Instance.log.Info("Opended seasons window.");
                new ShowSeasonsWindow(ChoosedTvShow).Show();
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                MessageBox.Show(Window, "Can't connect on server.");
            }
        }

        public ICommand RedoCommand
        {
            get;
            private set;
        }
       
        public ICommand UndoCommand
        {
            get;
            private set;
        }

        public bool CanShowLogInfoWindow
        {
            get
            {
                return true;
            }
        }


        public void ShowLogInfoWindow()
        {
            try
            {
                Logger.Instance.log.Info("Opended logger info window.");
                new LogInfoWindow().Show();
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                MessageBox.Show(Window, "Can't connect on server.");
            }
        }

    }
}
