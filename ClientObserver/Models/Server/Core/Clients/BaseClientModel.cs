using ClientObserver.Models.Server.Core.Configs;
using ClientObserver.Models.Interfaces.Clients;
using ClientObserver.Models.Events.ObservableProperties;

namespace ClientObserver.Models.Server.Core.Clients
{
    public abstract class BaseClientModel : IClient
    {
        public string Name { get; set; }
        public ObservableProperty<bool> IsConnected { get; } = new ObservableProperty<bool>();
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

        public async Task Connect()
        {
            await ClientService.ConnectAsync();
        }

        protected void SetClientService(IClientService service)
        {
            ClientService = service;
        }
    }


}
