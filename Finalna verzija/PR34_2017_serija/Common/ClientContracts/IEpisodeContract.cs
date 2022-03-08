using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common.ClientContracts
{
    [ServiceContract]
    public interface IEpisodeContract
    {
        [OperationContract]
        bool AddEpisode(Episode ep, int idseason,int idshow);

        [OperationContract]
        List<Episode> GetAllEpisodes(int id);

        [OperationContract]
        bool ChangeEpisode(Episode ep, int idsez);

        [OperationContract]
        bool DeleteEpisode(Episode ep);
    }
}
