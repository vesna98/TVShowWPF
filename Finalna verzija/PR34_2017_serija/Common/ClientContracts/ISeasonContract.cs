using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common.ClientContracts
{   [ServiceContract]
    public interface ISeasonContract
    {
        [OperationContract]
        bool AddSeason(string name, int idshow);

        [OperationContract]
        List<Season> GetAllSeasons(int id);

        [OperationContract]
        bool ChangeSeason(Season sez);

        [OperationContract]
        bool DeleteSeason(Season sez);
    }
}
