using System;
using System.Threading.Tasks;
using ClientObserver.Models.Server.Core.Clients;
using ClientObserver.Models.Server.Core.Configs;
using ClientObserver.Services.Server.Core.Clients.CloudClientService.Core;

namespace ClientObserver.Services.Server.Core.Clients
{
    /// <summary>
    /// Represents a service for managing cloud client connections, extending the base functionality
    /// to include cloud-specific connection steps such as initialization, authentication, and finalization.
    /// </summary>
    public class CloudService : BaseClientService
    {
        public CloudClient CloudClient => ClientModel as CloudClient;

        /// <summary>
        /// Initializes a new instance of the CloudClientService with the specified cloud client model.
        /// </summary>
        /// <param name="clientModel">The cloud client model to be managed by this service.</param>
        public CloudService(CloudClient clientModel) : base(clientModel, new CloudClientConnectionService(clientModel))
        {
            ClientModel = clientModel;
        }



        /// <summary>
        /// Applies cloud-specific configuration settings to the cloud client model.
        /// </summary>
        /// <param name="config">The cloud configuration to apply. Expected to be of type CloudConfig.</param>
        /// <exception cref="InvalidCastException">Thrown if the client model is not of the expected cloud client type.</exception>
        /// <exception cref="ArgumentException">Thrown if the provided configuration is not of the expected cloud configuration type.</exception>
        public override void ApplyConfig(BaseConfig config)
        {
            if (config is CloudConfig cloudConfig)
            {
                if (ClientModel is CloudClient cloudClient)
                {
                    // Apply cloud-specific configuration settings
                    cloudClient.CloudKey = cloudConfig.CloudKey;
                }
                else
                {
                    throw new InvalidCastException("ClientModel is not of type CloudClient");
                }
            }
            else
            {
                throw new ArgumentException("Config must be of type CloudConfig", nameof(config));
            }
        }
    }
}
