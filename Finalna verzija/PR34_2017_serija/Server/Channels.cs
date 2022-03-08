using Common.ClientContracts;
using Server.ContractProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Channels
    {
        private static ServiceHost userServiceHost;
        private static ServiceHost showServiceHost;
        private static ServiceHost seasonServiceHost;
        private static ServiceHost episodeServiceHost;
        public Channels()
        {
            NetTcpBinding binding = new NetTcpBinding();


            userServiceHost = new ServiceHost(typeof(UserContractProvider));
            userServiceHost.AddServiceEndpoint(typeof(IUserContract), binding, new Uri("net.tcp://localhost:11000/IUserContract"));
            
            
            showServiceHost = new ServiceHost(typeof(TvShowContractProvider));
            showServiceHost.AddServiceEndpoint(typeof(ITvShowContract), binding, new Uri("net.tcp://localhost:11000/ITvShowContract"));

            seasonServiceHost = new ServiceHost(typeof(SeasonContractProvider));
            seasonServiceHost.AddServiceEndpoint(typeof(ISeasonContract), binding, new Uri("net.tcp://localhost:11000/ISeasonContract"));

            episodeServiceHost = new ServiceHost(typeof(EpisodeContractProvider));
            episodeServiceHost.AddServiceEndpoint(typeof(IEpisodeContract), binding, new Uri("net.tcp://localhost:11000/IEpisodeContract"));
        }
        public void Open()
        {
            userServiceHost.Open();
            showServiceHost.Open();
            seasonServiceHost.Open();
            episodeServiceHost.Open();
        }

        public void Close()
        {
            userServiceHost.Close();
            showServiceHost.Close();
            seasonServiceHost.Close();
            episodeServiceHost.Close();
        }
    }
}
