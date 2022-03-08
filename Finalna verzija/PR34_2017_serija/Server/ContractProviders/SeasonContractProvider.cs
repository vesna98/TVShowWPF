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
    class SeasonContractProvider : ISeasonContract
    {
        IDBSeasonContract baza = new DatabaseSeasonProvider();
        object x = new object();
        public bool AddSeason(string name, int idshow)
        {
            try
            {
                lock (x)
                {
                    bool rezultat = baza.AddSeasonToDB(name, idshow);
                    return rezultat;
                }
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                return false;
            }
        }

        public bool ChangeSeason(Season sez)
        {
            try
            {
                lock (x)
                {
                    bool rezultat = baza.ChangeSeasonToDB(sez);
                    return rezultat;
                }
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                return false;
            }
        }

        public bool DeleteSeason(Season sez)
        {
            try
            {
                lock (x)
                {
                    bool rezultat = baza.DeleteSeasonFromDB(sez);
                    return rezultat;
                }
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                return false;
            }
        }

        public List<Season> GetAllSeasons(int id)
        {
            try
            {
                lock (x)
                {
                    List<Season> rezultat = baza.GetAllSeasonsFromDB(id);
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
