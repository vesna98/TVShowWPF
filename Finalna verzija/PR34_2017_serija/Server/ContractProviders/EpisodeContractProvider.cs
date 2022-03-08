using Common;
using Common.ClientContracts;
using Common.Models;
using Server.DBService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.ContractProviders
{
    class EpisodeContractProvider : IEpisodeContract
    {
        IDBEpisodeContract baza = new DatabaseEpisodeProvider();
        object x = new object();
        public bool AddEpisode(Episode ep, int idseason,int idshow)
        {
            try
            {
                lock (x)
                {
                    bool rezultat = baza.AddEpisodeToDB(ep, idseason,idshow);
                    return rezultat;
                }
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                return false;
            }
        }

        public bool ChangeEpisode(Episode ep, int idsez)
        {
            try
            {
                lock (x)
                {
                    bool rezultat = baza.ChangeEpisodeToDB(ep,idsez);
                    return rezultat;
                }
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                return false;
            }
        }

        public bool DeleteEpisode(Episode ep)
        {
            try
            {
                lock (x)
                {
                    bool rezultat = baza.DeleteEpisodeFromDB(ep);
                    return rezultat;
                }
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                return false;
            }
        }

        public List<Episode> GetAllEpisodes(int id)
        {
            try
            {
                lock (x)
                {
                    List<Episode> rezultat = baza.GetAllEpisodesFromDB(id);
                    return rezultat;
                }
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                return null;
            }
        }
    }
}
