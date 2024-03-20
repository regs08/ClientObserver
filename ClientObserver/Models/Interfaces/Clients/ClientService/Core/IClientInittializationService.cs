using System;
using ClientObserver.Models.Server.Core.Configs;

namespace ClientObserver.Models.Interfaces.Clients.ClientService.Core
{
	public interface IClientInittializationService
	{
        void ApplyConfig(BaseConfig config);

    }
}

