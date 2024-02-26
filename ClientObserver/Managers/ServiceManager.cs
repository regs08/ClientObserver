using System;
using ClientObserver.Models.Server.Core.Configs;
using ClientObserver.Models.Server.Framework.Configs;
// todo replace this 
namespace ClientObserver.Services
{
    /// <summary>
    /// Manages the initialization and configuration of various services within the application, based on server configurations.
    /// </summary>
    public class ServiceManager
    {
        /// <summary>
        /// Gets the server configuration used by the service manager.
        /// </summary>
        public ServerConfigs Config { get; private set; }

        /// <summary>
        /// Manages MQTT client services within the application.
        /// </summary>
        public MqttClientService MqttService { get; private set; }

        /// <summary>
        /// Manages logging services within the application.
        /// </summary>
        public LogService LogService { get; private set; }

        /// <summary>
        /// Manages video stream services within the application.
        /// </summary>
        public VideoStreamService VideoStreamService { get; private set; }

        /// <summary>
        /// Manages image receiving services within the application.
        /// </summary>
        public ImageReceiverService ImageReceiverService { get; private set; }

        /// <summary>
        /// Initializes a new instance of the ServiceManager class with the specified server configurations.
        /// </summary>
        /// <param name="ConfigManager">The server configurations to use for initializing services.</param>
        public ServiceManager(ServerConfigs ConfigManager)
        {
            Config = ConfigManager;
            ImplementServiceManager();
        }

        /// <summary>
        /// Configures and initializes services based on the provided server configuration.
        /// </summary>
        private void GetServicesFromConfig()
        {
            // Initializes services like MQTT, Log, Video Stream, and Image Receiver based on the server configurations.
        }

        /// <summary>
        /// Implements the service manager, setting up all necessary services by calling GetServicesFromConfig.
        /// </summary>
        private void ImplementServiceManager()
        {
            GetServicesFromConfig();
        }

        // Note: Additional methods for service management like initializing or disposing services could be added here.
    }
}
