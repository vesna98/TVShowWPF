using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DBService
{
    interface IDBEpisodeContract
    {
        bool AddEpisodeToDB(Episode ep, int idseason,int idshow);

        List<Episode> GetAllEpisodesFromDB(int id);

        bool ChangeEpisodeToDB(Episode ep, int idsez);

        bool DeleteEpisodeFromDB(Episode ep);
    }
}
