using System;
using ClientObserver.Models.Server.Core.Configs;

namespace ClientObserver.Models.Server.Core.Configs
{
    public class CloudConfig : BaseConfig
    {
        public string CloudKey { get; set; }
        public CloudConfig() : base("CloudClientConfig")
        {

        }
    }
}

