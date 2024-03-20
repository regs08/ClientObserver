using System;
using System.Threading.Tasks;
using ClientObserver.Models.Server.Core.Clients;
using ClientObserver.Models.Server.Core.Configs;
using ClientObserver.Services.Server.Core.Clients.ModelParamClientService.Core;

namespace ClientObserver.Services.Server.Core.Clients
{
    /// <summary>
    /// Represents a service for managing model parameter client connections, extending the base functionality
    /// to include steps specific to model parameter configuration and connection.
    /// </summary>
    public class ModelParamService : BaseClientService
    {
        public ModelParamClient ModelParamClientModel => ClientModel as ModelParamClient;

        /// <summary>
        /// Initializes a new instance of the ModelParamService with the specified model parameter client model.
        /// </summary>
        /// <param name="clientModel">The model parameter client model to be managed by this service.</param>
        public ModelParamService(ModelParamClient clientModel) : base(clientModel, new ModelParamConnectionService(clientModel))
        {
            ClientModel = clientModel;
        }

        /// <summary>
        /// Applies configuration settings to the model parameter client model.
        /// </summary>
        /// <param name="config">The configuration to apply. Expected to be of type ModelParamConfig.</param>
        public override void ApplyConfig(BaseConfig config)
        {
            if (config is ModelParamConfig modelParamConfig)
            {
                if (ClientModel is ModelParamClient modelParamClient)
                {
                    modelParamClient.AvailableLabels = modelParamConfig.AvailableLabels;
                    modelParamClient.ConfidenceThreshold = modelParamConfig.ConfidenceThreshold;
                    modelParamClient.SelectedLabels = modelParamConfig.SelectedLabels;
                }
                else
                {
                    throw new InvalidCastException("ClientModel is not of type ModelParamClient");
                }
            }
            else
            {
                throw new ArgumentException("Config must be of type ModelParamConfig", nameof(config));
            }
        }
    }
}
