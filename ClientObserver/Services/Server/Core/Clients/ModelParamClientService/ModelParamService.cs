using System;
using System.Threading.Tasks;
using ClientObserver.Models.Server.Core.Clients;
using ClientObserver.Models.Server.Core.Configs;

namespace ClientObserver.Services.Server.Core.Clients
{
    /// <summary>
    /// Represents a service for managing model parameter client connections, extending the base functionality
    /// to include steps specific to model parameter configuration and connection.
    /// </summary>
    public class ModelParamService : BaseClientService
    {
        /// <summary>
        /// Initializes a new instance of the ModelParamService with the specified model parameter client model.
        /// </summary>
        /// <param name="clientModel">The model parameter client model to be managed by this service.</param>
        public ModelParamService(ModelParamClient clientModel)
        {
            ClientModel = clientModel;
        }

        /// <summary>
        /// Gets the sequence of connection steps to be executed for establishing a connection.
        /// </summary>
        protected override ConnectionStep[] ConnectionSteps
        {
            get
            {
                return new ConnectionStep[]
                {
                    InitializeConnection,
                    Authenticate,
                    FinalizeConnection
                };
            }
        }

        /// <summary>
        /// Initializes the connection for the model parameter client.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation, containing a boolean value indicating success or failure.</returns>
        private async Task<bool> InitializeConnection()
        {
            try
            {
                // Simulated initialization logic
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught during initialization: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Authenticates the model parameter client.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation, containing a boolean value indicating success or failure.</returns>
        private async Task<bool> Authenticate()
        {
            // Simulated authentication logic
            return true;
        }

        /// <summary>
        /// Finalizes the connection setup for the model parameter client.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation, containing a boolean value indicating success or failure.</returns>
        private async Task<bool> FinalizeConnection()
        {
            // Simulated finalization logic
            return true;
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
