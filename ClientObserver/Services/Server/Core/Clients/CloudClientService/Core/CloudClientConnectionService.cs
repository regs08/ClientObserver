using System;
using ClientObserver.Services.Server.Core.Base;
using ClientObserver.Models.Server.Core.Clients;
namespace ClientObserver.Services.Server.Core.Clients.CloudClientService.Core
{
	public class CloudClientConnectionService : BaseClientConnectionService
	{
		public CloudClientConnectionService(CloudClient clientModel) : base(clientModel)
        {
		}
        public override async Task<bool> InitializeConnection()
        {
            return true;
        }
        public override async Task<bool> Authenticate()
        {
            return true;
        }
        public override async Task<bool> FinalizeConnection()
        {
            return true;
        }
        public override async Task<bool> DisconnectFromClient()
        {
            return true;
        }
    }
}

