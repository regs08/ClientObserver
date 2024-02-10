using System;
using ClientObserver.Managers;
using ClientObserver.Models.Servers; 
namespace ClientObserver.Services
{
    public class ServiceManager
    {
        // Holds the configuration for the server
        public ServerConfigs Config { get; private set; }

        // Services used by the application
        public MqttClientService MqttService { get; private set; }
        public LogService LogService { get; private set; }
        public VideoStreamService VideoStreamService { get; private set; }
        public ImageReceiverService ImageReceiverService { get; private set; }

        // Constructor initializes the ServiceManager with a server configuration
        public ServiceManager(ServerConfigs ConfigManager)
        {
            Config = ConfigManager;
            ImplementServiceManager();          
        }
        
        // Configures and initializes services based on the provided server configuration
        private void GetServicesFromConfig()
        {
            // Creates an MQTT client service using the MQTT client configuration from the server config
            MqttService = new MqttClientService(Config.MqttClientConfig);

            // Initializes the log service (comment indicates a potential update needed for the constructor)
            // todo update the constructor for log service to not take the mqtt service 
            LogService = new LogService(MqttService);

            // Initializes the video stream service with the URI from the video stream configuration
            VideoStreamService = new VideoStreamService(Config.VideoStreamConfig.VideoStreamUri);

            // Initializes the image receiver service
            ImageReceiverService = new ImageReceiverService(MqttService);
        }

        // Method to implement the service manager, setting up all necessary services
        private void ImplementServiceManager()
        {
            GetServicesFromConfig();
        }

        // Additional methods for service management (like initializing or disposing services) could be added here
    }
}
