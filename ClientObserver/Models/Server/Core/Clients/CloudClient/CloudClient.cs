using System;
using ClientObserver.Models.Server.Core.Configs;
using ClientObserver.Services.Server.Core.Clients;

namespace ClientObserver.Models.Server.Core.Clients
{
    public class CloudClient : BaseClientModel
    {
        public string CloudKey { get; set; }
        public CloudClient(CloudConfig config) : base(config, "CloudClient")
        {
            Config = config;
            SetClientService(new CloudService(this));
            InitializeWithConfig();

        }
    }
}



