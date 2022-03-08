using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Models;

namespace Server.DBService
{
    public class DatabaseTvShowProvider : IDBTvShowContract
    {
        public int AddShowToDatabase(TvShow show, User user)
        {
            using (var dBContext = new Serijica2Context())
            {
                if (GetOneShow(show.Showid) == null)
                {
                    List<Genre> zanrovi = show.Genres;
                    foreach (Genre zanr in zanrovi)
                    {
                        zanr.IDshow = show.Showid;
                        dBContext.Genres.Add(zanr);//dodaje zanrove redom oznacene
                        
                    }
                    
                    show.Genres.Clear();
                    if (show.Showid != 0)
                    {
                        dBContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT TvShows OFF;");
                        dBContext.TvShows.Add(show);
                        dBContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT TvShows ON;");
                    }
                    else
                    {
                        dBContext.TvShows.Add(show);
                    }

                    dBContext.SaveChanges();
                    Logger.Instance.log.Info("Tv show added to database");
                    return show.Showid;
                }
                Logger.Instance.log.Warn("Could not add tv show to database");
                return -1;
            }
        }

        public bool ChangeShowToDatabase(TvShow show)
        {
            using (var dBContext = new Serijica2Context())
            {
                TvShow oldShow = GetOneShow(show.Showid);
                if (oldShow != null)
                {
                    oldShow.SeasonCnt = show.SeasonCnt;
                    oldShow.Sezone = show.Sezone;
                    oldShow.Name = show.Name;
                    oldShow.Description = show.Description;
                    oldShow.EpisodeCnt = show.EpisodeCnt;
                    oldShow.Genres = show.Genres;

                    //brise sve zanrove i dodaje nove
                    foreach (Genre item in dBContext.Genres)
                    {
                        if (item.IDshow == show.Showid)
                        {
                            dBContext.Genres.Attach(item);
                            dBContext.Genres.Remove(item);
                           
                        }
                   
                    }

                    List<Genre> zanrovi = show.Genres;
                    foreach (Genre zanr in zanrovi)
                    {
                        zanr.IDshow = show.Showid;
                        dBContext.Genres.Add(zanr);//dodaje zanrove redom oznacene
                    }

                    dBContext.Entry(show).State = System.Data.Entity.EntityState.Modified;
                    dBContext.SaveChanges();

                    Logger.Instance.log.Info("Tv show changed in database");
                    return true;
                }
                Logger.Instance.log.Warn("Could not change tv show in database");
                return false;
            }
        }

        public bool DeleteShowFromDatabase(TvShow show)
        {
            try
            {
                TvShow remove = null;
                TvShow b = new TvShow();
                b = show;
                using (var dbContext = new Serijica2Context())
                {
                    foreach (TvShow item in dbContext.TvShows)
                    {
                        if (item.Showid == b.Showid)
                            remove = item;

                    }
                    if (remove != null)
                    {
                        dbContext.TvShows.Attach(remove);
                        dbContext.TvShows.Remove(remove);
                        // dbContext.SaveChanges();
                        //brise sve zanrove
                        foreach (Genre item in dbContext.Genres)
                        {
                            if (item.IDshow == show.Showid)
                            {
                                dbContext.Genres.Attach(item);
                                dbContext.Genres.Remove(item);
                               // dbContext.SaveChanges();
                            }
                        }
                        

                        dbContext.SaveChanges();
                        Logger.Instance.log.Info("Tv show deleted from database");
                        return true;
                    }

                    Logger.Instance.log.Warn("Could not delete tv show to database");
                    return false;
                    
                }
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                return false;
            }
        }
        public static int idOfNewShow;
        public int DuplicateShowToDatabase(TvShow show)
        {
            using (var baza = new Serijica2Context())
            {
                try
                {
                    var oldShow = baza.TvShows.Find(show.Showid);
                    if (oldShow != null)
                    {
                        //kopirati sve posebno, i sezone i serije i zanrove i epizode
                        var seasons = baza.Seasons.Where(x => x.TvShow == oldShow.Showid);
                        oldShow.Sezone.Clear();
                        //oldShow.Sezone.AddRange(seasons);

                       // baza.Seasons.AddRange(seasons);
                        baza.TvShows.Add(oldShow);

                        baza.SaveChanges();
                        Logger.Instance.log.Info("Tv show duplicated to database");
                        return oldShow.Showid;
                    }
                  
                    Logger.Instance.log.Warn("Could not duplicate tv show to database");
                    return 0;
                    
                }
                catch(Exception e)
                {
                    Logger.Instance.log.Error(e.Message);
                    return 0;
                }
            }
        }

        public List<TvShow> GetAllShows()
        {
            using (var dBContext = new Serijica2Context())
            {
                List<TvShow> shows = dBContext.TvShows.ToList();
                List<Genre> genres = dBContext.Genres.ToList();
                for (int i = 0; i < shows.Count; i++)
                {
                    for (int j = 0; j < genres.Count; j++)
                    {
                        if (genres[j].IDshow + 1 == shows[i].Showid)
                            shows[i].Genres.Add(genres[j]);        //za svaku seriju ubace oznaceni zanrovi
                    }
                }
                Logger.Instance.log.Info("Getting tv show list from database");
                return shows;
            }
        }

        public TvShow GetOneShow(int id)
        {
            using (Serijica2Context context = new Serijica2Context())
            {
                Logger.Instance.log.Info("Getting one tv show from database");
                return context.TvShows.FirstOrDefault(x => x.Showid == id);
            }
        }
    }
}
