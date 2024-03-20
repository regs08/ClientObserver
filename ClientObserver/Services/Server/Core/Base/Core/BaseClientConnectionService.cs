using System;
using ClientObserver.Services.Core.Server.Core.Base;
using ClientObserver.Models.Server.Core.Clients;
namespace ClientObserver.Services.Server.Core.Base
{
	public abstract class BaseClientConnectionService : BaseConnectionService
    {
        public BaseClientModel ClientModel { get; set; }

        public BaseClientConnectionService(BaseClientModel clientModel)
		{
            ClientModel = clientModel;

        }
    }
}

