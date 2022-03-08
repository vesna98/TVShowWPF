using Common;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DBService
{
    class DatabaseSeasonProvider:IDBSeasonContract
    {
        public bool AddSeasonToDB(string number, int idshow)
        {
            
            Season season = new Season();
            season.Number = int.Parse(number);
            season.TvShow = idshow;//ovako ce se automatski generisati number,ali treba promeniti da ne bude autogen
            using (var baza = new Serijica2Context())
            {
                try
                {
                    Season pretraga = GetAllSeasonsFromDB(idshow).Find(x => x.Number == int.Parse(number));
                    if (pretraga == null)
                    {
                        baza.TvShows.FirstOrDefault(x => x.Showid == idshow).SeasonCnt++;//poveca broj sezone
                        baza.Seasons.Add(season);
                        baza.SaveChanges();

                        Logger.Instance.log.Info("Season added to database");
                        return true;
                    }
                    Logger.Instance.log.Warn("Could not add season to database");
                    return false;
                }
                catch(Exception e)
                {
                    Logger.Instance.log.Error(e.Message);
                    return false;
                }
            }
        }

        public List<Season> GetAllSeasonsFromDB(int id)
        {
            List<Season> helpSeasonList = new List<Season>();
            using (var baza = new Serijica2Context())
            {
                helpSeasonList = baza.Seasons.ToList().FindAll(x => x.TvShow == id);
                
            }
            Logger.Instance.log.Info("Getting season list from database");
            return helpSeasonList;
        }

        public bool ChangeSeasonToDB(Season sez)
        {
            // throw new NotImplementedException();    //dopuniti klasu i bazu,da bi se uopste mogla odraditi izmena
            using (var dBContext = new Serijica2Context())
            {
                Season sezonaSaTimID = GetAllSeasonsFromDB(sez.TvShow).Find(x => x.Number == sez.Number);//postoji vec

                Season oldSeason = GetAllSeasonsFromDB(sez.TvShow).Find(x => x.IDseason == sez.IDseason);
                if (oldSeason != null && sezonaSaTimID == null)
                {

                    oldSeason.Number = sez.Number;

                    dBContext.Entry(sez).State = System.Data.Entity.EntityState.Modified;
                    dBContext.SaveChanges();

                    Logger.Instance.log.Info("Season changed in database");
                    return true;
                }
                Logger.Instance.log.Warn("Could not change season in database");
                return false;
            }
        }

        public bool DeleteSeasonFromDB(Season sez)
        {
            using (var baza = new Serijica2Context())
            {
                try
                {
                    var oldSeason = baza.Seasons.ToList().Find(x=>x.Number==sez.Number);
                    int brojEp = 0;
                    if (oldSeason != null)
                    {
                        TvShow show = baza.TvShows.ToList().Find(x => x.Showid == sez.TvShow);//.SeasonCnt--;//poveca broj sezone
                        if (show.SeasonCnt - 1 >= 0)
                        {
                            show.SeasonCnt--;//broj svih epizoda te seyone
                            brojEp = baza.Episodes.ToList().FindAll(x => x.Sezonaid == sez.IDseason).Count;
                            if(show.EpisodeCnt - brojEp>=0)
                                show.EpisodeCnt = show.EpisodeCnt - brojEp;
                            baza.Entry(show).State = System.Data.Entity.EntityState.Modified;
                        }

                        baza.Seasons.Attach(oldSeason);
                        baza.Seasons.Remove(oldSeason);
                        baza.SaveChanges();

                        Logger.Instance.log.Info("Removed season from database");
                        return true;
                    }
                    Logger.Instance.log.Warn("Could not delete season from database");
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
