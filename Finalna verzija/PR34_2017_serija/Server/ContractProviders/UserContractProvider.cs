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
    public class UserContractProvider : IUserContract
    {
        IDBUserContract baza = new DatabaseUserProvider();
        object x = new object();

        public bool AddUser(User user)
        {
            try
            {
                lock (x)
                {
                    bool rezultat= baza.AddUserToDatabase(user);
                    return rezultat;
                }
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                return false;
            }
            
        }

        public bool ChangeUser(User user)
        {
            try
            {
                lock (x)
                {
                    bool rezultat=baza.UpdateUserToDatabase(user);
                    return rezultat;
                }
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                return false;
            }
           
        }

        public List<User> GetAllUsers()
        {
            try 
            {
                lock (x)
                {
                    List<User> korisnici= baza.GetAllUsers();
                    return korisnici;
                }
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                return null;
            }
        }

        
        public User Login(string username)
        {
            try
            {
                lock (x)
                {
                    User user = baza.FindUser(username);
                    //logger.LogMessage(LOG_TYPE.INFO, "Login executed succesfuly.");
                    return user;
                }
            }
            catch(Exception e)
            {
                Logger.Instance.log.Error(e.Message);
                // logger.LogMessage(LOG_TYPE.ERROR, "Login executed unsuccesfuly.");
                return null;
            }
        }
    }
}
