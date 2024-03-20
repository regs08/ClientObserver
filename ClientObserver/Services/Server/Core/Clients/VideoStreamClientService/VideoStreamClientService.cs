using System;
using System.Threading.Tasks;
using ClientObserver.Models.Server.Core.Clients;
using ClientObserver.Models.Server.Core.Configs;

namespace ClientObserver.Services.Server.Core.Clients.VideoStreamClientService
{
    /// <summary>
    /// Represents a service for managing video stream client connections, extending the base functionality
    /// to include video stream-specific connection steps.
    /// </summary>
    public class VideoStreamClientService : BaseClientService
    {
        public VideoStreamClient VideoStreamClient => ClientModel as VideoStreamClient;
        public VideoStreamConnectionService videoStreamConnectionService => (VideoStreamConnectionService)ConnectionService;

        /// <summary>
        /// Initializes a new instance of the VideoStreamClientService with the specified video stream client model.
        /// </summary>
        /// <param name="clientModel">The video stream client model to be managed by this service.</param>
        public VideoStreamClientService(VideoStreamClient clientModel)
            : base(clientModel, new VideoStreamConnectionService(clientModel))
        {
            // If additional initialization is needed, it can be performed here.
            // Note: With the corrected approach, there's no need to assign 'connectionService' here,
            // as 'ConnectionService' is initialized and passed to the base constructor.
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
        // Additional methods and properties specific to video stream client service can be added here.
    }
}




