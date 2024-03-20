using System;
using ClientObserver.Services.Server.Core.Clients.CloudClientService;
using ClientObserver.Services.Server.Core.Clients.MqttClientService;
using ClientObserver.Services.Server.Core.Clients.ModelParamClientService;
using ClientObserver.Services.Server.Core.Clients.VideoStreamClientService;
using ClientObserver.Services.Core.Server.Core.Base;

namespace ClientObserver.Services.Server.RPI.ConnectionService
{
	public class RPIConnectionService: BaseConnectionService
	{
		public RPIConnectionService()
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

