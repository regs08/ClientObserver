using ClientObserver.Models.Server.Core.Configs;
using ClientObserver.Models.Interfaces.Clients;
using ClientObserver.Services.Server.Core.Clients;

namespace ClientObserver.Models.Server.Core.Clients
{
    public abstract class BaseClientModel : IClient
    {
        public string Name { get; set; }
        public bool ConnectionStatus { get; set; } = false;
        public BaseConfig Config { get; protected set; }
        protected IClientService ClientService { get; set; }

        protected BaseClientModel(BaseConfig config, string name = "BaseClientModel")
        {
            Name = name;
            Config = config;
        }

        public void InitializeWithConfig()
        {
            ClientService.ApplyConfig(Config);
        }

        public void Connect()
        {
            ClientService.Connect();
        }

        protected void SetClientService(IClientService service)
        {
            ClientService = service;
        }
    }


}
