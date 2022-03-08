using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DBService
{
    public interface IDBSeasonContract
    {
        bool AddSeasonToDB(string name, int idshow);

        List<Season> GetAllSeasonsFromDB(int id);

        bool ChangeSeasonToDB(Season sez);

        bool DeleteSeasonFromDB(Season sez);
    }
}
