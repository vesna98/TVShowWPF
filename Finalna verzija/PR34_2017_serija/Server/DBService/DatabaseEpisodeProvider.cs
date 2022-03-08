using Common;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DBService
{
    class DatabaseEpisodeProvider:IDBEpisodeContract
    {
        public bool AddEpisodeToDB(Episode ep, int idseason, int idshow)
        {
            
            ep.Sezonaid = idseason;
            using (var baza = new Serijica2Context())
            {
                try
                {
                    baza.TvShows.FirstOrDefault(x => x.Showid == idshow).EpisodeCnt++;//poveca broj sezone

                    baza.Episodes.Add(ep);
                    baza.SaveChanges();

                    Logger.Instance.log.Info("Episode added to database");
                    return true;
                }
                catch(Exception e)
                {
                    Logger.Instance.log.Error(e.Message);
                    return false;
                }
            }
        }

        public List<Episode> GetAllEpisodesFromDB(int id)
        {
            List<Episode> helpEpisodeList = new List<Episode>();
            using (var baza = new Serijica2Context())
            {
                helpEpisodeList = baza.Episodes.ToList().FindAll(x => x.Sezonaid == id);

            }
            Logger.Instance.log.Info("Getting episode list from database.");
            return helpEpisodeList;
        }

        public bool ChangeEpisodeToDB(Episode ep, int idsez)
        {
            using (var dBContext = new Serijica2Context())
            {
                Episode oldEp = GetAllEpisodesFromDB(idsez).Find(x => x.Index == ep.Index);
                if (oldEp != null)
                {
                    oldEp.PlotOutline = ep.PlotOutline;
                    oldEp.Sezonaid = ep.Sezonaid;
                    oldEp.Title = ep.Title;


                    dBContext.Entry(oldEp).State = System.Data.Entity.EntityState.Modified;
                    dBContext.SaveChanges();
                    Logger.Instance.log.Info("Successfuly changed episode in database.");
                    return true;
                }
                
                    Logger.Instance.log.Warn("Could not change episode in database.");
                    return false;
                
            }
        }

        public bool DeleteEpisodeFromDB(Episode ep)
        {
            using (var baza = new Serijica2Context())
            {
                try
                {
                    var oldEp = baza.Episodes.ToList().Find(x=>x.Index==ep.Index);   //indeks je KLJUC
                    if (oldEp != null)
                    {
                        //baza.TvShows.FirstOrDefault(x => x.Showid == baza.Seasons.FirstOrDefault(y => y.IDseason == ep.Sezonaid).TvShow).EpisodeCnt--;//poveca broj sezone
                        TvShow show = baza.TvShows.ToList().Find(x => x.Showid == baza.Seasons.ToList().Find(y => y.IDseason == ep.Sezonaid).TvShow);//.EpisodeCnt--;//poveca broj sezone
                        if (show.EpisodeCnt - 1 >= 0)
                        {
                            show.EpisodeCnt--;
                            baza.Entry(show).State = System.Data.Entity.EntityState.Modified;
                        }
                        baza.Episodes.Attach(oldEp);
                        baza.Episodes.Remove(oldEp);
                        baza.SaveChanges();

                        Logger.Instance.log.Info("Episode deleted from database");
                        return true;
                    }
                   
                        Logger.Instance.log.Warn("Could not delete episode from database");
                        return false;
                    
                }
                catch(Exception e)
                {
                    Logger.Instance.log.Error(e.Message);
                    return false;
                }
            }
        }
    }
}
