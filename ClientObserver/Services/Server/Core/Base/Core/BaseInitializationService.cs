using System;
using ClientObserver.Models.Server.Core.Configs;
using ClientObserver.Models.Interfaces.Clients.ClientService.Core;

namespace ClientObserver.Services.Server.Core.Base.Core
{
	public abstract class BaseInitializationService : IClientInittializationService
	{
		public BaseInitializationService()
		{

		}
        public abstract void ApplyConfig(BaseConfig config);

    }
}



