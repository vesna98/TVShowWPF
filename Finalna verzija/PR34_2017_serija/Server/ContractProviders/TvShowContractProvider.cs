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
    public class TvShowContractProvider : ITvShowContract
    {
        IDBTvShowContract baza = new DatabaseTvShowProvider();
        object x = new object();
        public int AddShow(TvShow show, User user)
        {
            try
            {
                lock (x)
                {
                     
                    return baza.AddShowToDatabase(show, user);
                }
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                //Console.WriteLine(e.Message);
                return -1;
            }
        }

        public bool ChangeShow(TvShow show)
        {
            try
            {
                lock (x)
                {
                    bool rezultat = baza.ChangeShowToDatabase(show);
                    return rezultat;
                }
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                return false;
            }
        }

        public bool DeleteTvShow(TvShow show)
        {
            try
            {
                lock (x)
                {
                    bool rezultat = baza.DeleteShowFromDatabase(show);
                    return rezultat;
                }
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                return false;
            }
        }

        public int DuplicateShow(TvShow show)
        {
            try
            {
                lock (x)
                {
                    return baza.DuplicateShowToDatabase(show);
                }
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                return 0;
            }
        }

        public List<TvShow> GetAllShows()
        {
            try
            {
                lock (x)
                {
                    List<TvShow> rezultat = baza.GetAllShows();
                    return rezultat;
                }
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                return null;
            }
        }

        public TvShow GetOneShow(int id)
        {
            try
            {
                lock (x)
                {
                   TvShow rezultat = baza.GetOneShow(id);
                    return rezultat;
                }
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                return null;
            }
        }

        //public bool TryChangeShow(TvShow show)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
