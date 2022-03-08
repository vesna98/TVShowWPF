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
    public interface ITvShowContract
    {
        [OperationContract]
        int DuplicateShow(TvShow show);
        //[OperationContract]
        //bool TryChangeShow(TvShow show);
        [OperationContract]
        bool ChangeShow(TvShow show);

        //[OperationContract]
        //bool ChangeShowSeason(TvShow show, int id);

        [OperationContract]
        int AddShow(TvShow show, User user);
        [OperationContract]
        List<TvShow> GetAllShows();

        [OperationContract]
        TvShow GetOneShow(int id);
        [OperationContract]
        bool DeleteTvShow(TvShow show);
    }
}
