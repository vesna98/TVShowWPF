using Common.ClientContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Klijent
{
    public class ConnectChannel
    {
        public IUserContract userProxy { get; set; }
        public ITvShowContract ShowProxy { get; set; }
        public ISeasonContract SeasonProxy { get; set; }
        public IEpisodeContract EpisodeProxy { get; set; }
        private static ConnectChannel instance;

        public static ConnectChannel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConnectChannel();
                }
                return instance;
            }
        }

        public ConnectChannel()
        {
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;

            ChannelFactory<IUserContract> channelFactoryUser = new ChannelFactory<IUserContract>(binding, new EndpointAddress("net.tcp://localhost:11000/IUserContract"));
            userProxy = channelFactoryUser.CreateChannel();

            ChannelFactory<ITvShowContract> channelFactoryShow = new ChannelFactory<ITvShowContract>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:11000/ITvShowContract"));
            ShowProxy = channelFactoryShow.CreateChannel();

            ChannelFactory<ISeasonContract> channelFactorySeason = new ChannelFactory<ISeasonContract>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:11000/ISeasonContract"));
            SeasonProxy = channelFactorySeason.CreateChannel();

            ChannelFactory<IEpisodeContract> channelFactoryEpisode = new ChannelFactory<IEpisodeContract>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:11000/IEpisodeContract"));
            EpisodeProxy = channelFactoryEpisode.CreateChannel();
        }
    }
}
