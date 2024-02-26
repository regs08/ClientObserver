using System;
using ClientObserver.Models.Server.Core.Configs;
using ClientObserver.Models.Events.ObservableProperties;

namespace ClientObserver.Models.Interfaces.Clients
{

    // IClientService interface
    public interface IClientService
    {
        Task ConnectAsync();
        void ApplyConfig(BaseConfig config);

    }

}

