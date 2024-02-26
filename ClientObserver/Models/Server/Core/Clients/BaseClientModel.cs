using ClientObserver.Models.Server.Core.Configs;
using ClientObserver.Models.Interfaces.Clients;
using ClientObserver.Models.Events.ObservableProperties;
using System.Threading.Tasks;

namespace ClientObserver.Models.Server.Core.Clients
{
    /// <summary>
    /// Provides a base implementation for client models, encapsulating common properties and behaviors such as connection status and configuration handling. 
    /// This abstract class implements the IClient interface, ensuring that all derived client models are identifiable and capable of connecting.
    /// </summary>
    public abstract class BaseClientModel : IClient
    {
        /// <summary>
        /// Gets or sets the name of the client model.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// An observable property that tracks the client's connection status.
        /// </summary>
        public ObservableProperty<bool> IsConnected { get; } = new ObservableProperty<bool>();

        /// <summary>
        /// The configuration used by this client model.
        /// </summary>
        public BaseConfig Config { get; protected set; }

        /// <summary>
        /// The client service responsible for handling connection logic specific to this client model.
        /// </summary>
        protected IClientService ClientService { get; set; }

        /// <summary>
        /// Initializes a new instance of the BaseClientModel class with the specified configuration and optional name.
        /// </summary>
        /// <param name="config">The configuration to be used by the client.</param>
        /// <param name="name">The name of the client. Defaults to "BaseClientModel".</param>
        protected BaseClientModel(BaseConfig config, string name = "BaseClientModel")
        {
            Name = name;
            Config = config;
        }

        /// <summary>
        /// Applies the configuration to the client using the associated client service.
        /// </summary>
        public void InitializeWithConfig()
        {
            ClientService.ApplyConfig(Config);
        }

        /// <summary>
        /// Initiates an asynchronous connection attempt using the client service.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Connect()
        {
            await ClientService.ConnectAsync();
        }

        /// <summary>
        /// Sets the client service responsible for handling the client's connection logic.
        /// </summary>
        /// <param name="service">The client service to be used.</param>
        protected void SetClientService(IClientService service)
        {
            ClientService = service;
        }
    }
}
