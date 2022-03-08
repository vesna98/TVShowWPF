using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Models;

namespace Server.DBService
{
    public class DatabaseUserProvider : IDBUserContract
    {
        public bool AddUserToDatabase(User user)
        {
            using (var dBContext = new Serijica2Context())
            {
                if (FindUser(user.Username) == null)
                {
                    dBContext.Users.Add(user);
                    dBContext.SaveChanges();

                    Logger.Instance.log.Info("User added to database");
                    return true;
                }
                return false;
            }
        }



        public User FindUser(string username)
        {
            User user = null;
          
            using (Serijica2Context context = new Serijica2Context())
            {
               user=context.Users.FirstOrDefault(x => x.Username.Equals(username));
            }
            Logger.Instance.log.Info("Finding specific user from database");
            return user;
        }


        public List<User> GetAllUsers()
        {
            using (var dBContext = new Serijica2Context())
            {
                List<User> users = dBContext.Users.ToList();
                
                return users;
            }
        }

        public bool UpdateUserToDatabase(User user)
        {
            using (var dBContext = new Serijica2Context())
            {
                User oldUser = FindUser(user.Username);
                if (oldUser != null)
                {
                    oldUser.Name = user.Name;
                    oldUser.Lastname = user.Lastname;
                    oldUser.Password = user.Password;

                    dBContext.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    dBContext.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }

     
    }
}
