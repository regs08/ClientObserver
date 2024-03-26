using ClientObserver.Models.Server.Core.Configs;
using ClientObserver.Models.Interfaces.Clients;
using ClientObserver.Helpers.Exceptions;
using ClientObserver.Models.Server.Core.Clients;
using ClientObserver.Services.Server.Core.Base;

//todo look at abstracting away the conneciton and disconnection logic.. 
namespace ClientObserver.Services.Server.Core.Clients
{
    // BaseClientService without the direct generic constraint to a model
    public abstract class BaseClientService : IClientService
    {
        protected BaseClientModel ClientModel { get; set; }
        public BaseClientConnectionService ConnectionService { get; protected set; }

        public BaseClientService(BaseClientModel clientModel, BaseClientConnectionService connectionService)
        {
            ClientModel = clientModel ?? throw new ArgumentNullException(nameof(clientModel));
            ConnectionService = connectionService ?? throw new ArgumentNullException(nameof(connectionService));
        }
        public async Task<bool> ConnectAsync()
        {
            return await ConnectionService.ConnectAsync();
        }

        public async Task DisconnectAsync()
        {
            await ConnectionService.DisconnectAsync();
        }
       
        public abstract void ApplyConfig(BaseConfig config);
    }
}

