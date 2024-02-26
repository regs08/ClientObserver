using System;
using System.Threading.Tasks;
using ClientObserver.Models.Server.Core.Clients;
using ClientObserver.Models.Server.Core.Configs;

namespace ClientObserver.Services.Server.Core.Clients
{
    /// <summary>
    /// Represents a service for managing video stream client connections, extending the base functionality
    /// to include video stream-specific connection steps.
    /// </summary>
    public class VideoStreamClientService : BaseClientService
    {
        /// <summary>
        /// Initializes a new instance of the VideoStreamClientService with the specified video stream client model.
        /// </summary>
        /// <param name="clientModel">The video stream client model to be managed by this service.</param>
        public VideoStreamClientService(VideoStreamClient clientModel)
        {
            ClientModel = clientModel;
        }

        /// <summary>
        /// Gets the sequence of connection steps to be executed for establishing a video stream connection.
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
        /// Initializes the connection for the video stream client.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation, containing a boolean value indicating success or failure.</returns>
        private async Task<bool> InitializeConnection()
        {
            try
            {
                // Simulated initialization logic
                return true; // Assuming initialization is successful
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught during initialization: {ex.Message}");
                return false; // Return false if initialization fails
            }
        }

        /// <summary>
        /// Authenticates the video stream client.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation, containing a boolean value indicating success or failure.</returns>
        private async Task<bool> Authenticate()
        {
            // Simulated authentication logic
            return true; // Assuming authentication is successful
        }

        /// <summary>
        /// Finalizes the connection setup for the video stream client.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation, containing a boolean value indicating success or failure.</returns>
        private async Task<bool> FinalizeConnection()
        {
            // Simulated finalization logic
            return true; // Assuming finalization is successful
        }

        /// <summary>
        /// Applies configuration settings to the video stream client model.
        /// </summary>
        /// <param name="config">The configuration to apply. Expected to be of type VideoStreamConfig.</param>
        /// <exception cref="InvalidCastException">Thrown if the client model is not of the expected video stream client type.</exception>
        /// <exception cref="ArgumentException">Thrown if the provided configuration is not of the expected video stream configuration type.</exception>
        public override void ApplyConfig(BaseConfig config)
        {
            if (config is VideoStreamConfig videoStreamConfig)
            {
                if (ClientModel is VideoStreamClient videoStreamClient)
                {
                    videoStreamClient.StreamIP = videoStreamConfig.StreamIP;
                    videoStreamClient.StreamPortNumber = videoStreamConfig.StreamPortNumber;
                    videoStreamClient.VideoStreamUri = videoStreamConfig.VideoStreamUri;
                }
                else
                {
                    throw new InvalidCastException("ClientModel is not of type VideoStreamClient");
                }
            }
            else
            {
                throw new ArgumentException("Config must be of type VideoStreamConfig", nameof(config));
            }
        }
    }
}
