using System;
namespace ClientObserver.Services
{
    public class ServiceManager
    {
        public ServerConfig Config { get; private set; }
        public MqttClientService MqttService { get; private set; }
        public LogService LogService { get; private set; }
        public VideoStreamService VideoStreamService { get; private set; }
        public ImageReceiverService ImageReceiverService { get; private set; }

        public ServiceManager(ServerConfig serverConfig)
        {
            Config = serverConfig;
            ImplementServiceManager();
        }
        private void GetServicesFromConfig()
        {
            MqttService = new MqttClientService(Config.MqttClientConfig);
            // todo update the constructor for log service to not take the mqtt service 
            LogService = new LogService(MqttService);
             
            VideoStreamService = new VideoStreamService(Config.VideoStreamConfig.VideoStreamUri);
            ImageReceiverService = new ImageReceiverService(MqttService);
        }

        private void ImplementServiceManager()
        {
            GetServicesFromConfig();
        }
        // Additional methods for service management (like initializing or disposing services) could be added here
    }

}

