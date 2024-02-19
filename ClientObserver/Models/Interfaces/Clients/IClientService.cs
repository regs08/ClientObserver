using System;
using ClientObserver.Models.Server.Core.Configs;

namespace ClientObserver.Models.Interfaces.Clients
{

    // IClientService interface
    public interface IClientService
    {
        void Connect();
        void ApplyConfig(BaseConfig config);
    }

}

