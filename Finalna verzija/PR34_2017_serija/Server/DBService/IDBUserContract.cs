using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DBService
{
    public interface IDBUserContract
    {
        bool AddUserToDatabase(User user);
        bool UpdateUserToDatabase(User user);
        List<User> GetAllUsers();
        User FindUser(string username);
        //show
        //bool DuplicateShowToDatabase(TvShow show);
        
        //bool TryChangeShowToDatabase(TvShow show);
        
        //bool ChangeShowToDatabase(TvShow show);

        //bool AddShowToDatabase(TvShow show, User user);
       
        //List<TvShow> GetAllShows();

        //TvShow GetOneShow(int id);
    }
}
