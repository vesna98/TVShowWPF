using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DBService
{
    public interface IDBTvShowContract
    {
        int DuplicateShowToDatabase(TvShow show);

        //bool TryChangeShowToDatabase(TvShow show);

        bool ChangeShowToDatabase(TvShow show);
        bool DeleteShowFromDatabase(TvShow show);

        int AddShowToDatabase(TvShow show, User user);

        List<TvShow> GetAllShows();

        TvShow GetOneShow(int id);
    }
}
