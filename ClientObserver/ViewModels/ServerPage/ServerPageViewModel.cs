using System.ComponentModel;
using ClientObserver.Services;
using ClientObserver.Views;
using ClientObserver.Views.ServerPage;
using ClientObserver.Models.Server.Framework.Configs;

namespace ClientObserver.ViewModels
{
    public class ServerPageViewModel : INotifyPropertyChanged
    {
        // Server configuration used throughout the application
        private ServerConfigs _serverConfig;
        private LogService _logService;
        private VideoStreamService _videoStreamService;
        private ImageReceiverService _imageReceiverService;

        // MQTT client service for MQTT communication
        private MqttClientService _mqttClientService;

        // Property for server configuration
        public ServerConfigs ServerConfig
        {
            get => _serverConfig;
            set
            {
                _serverConfig = value;
                OnPropertyChanged(nameof(ServerConfig));
            }
        }

        // Property for MQTT client service
        public MqttClientService MqttClientService
        {
            get => _mqttClientService;
            set
            {
                _mqttClientService = value;
                OnPropertyChanged(nameof(MqttClientService));
            }
        }

        // Constructor initializes the ViewModel with services from ServiceManager
        public ServerPageViewModel(ServiceManager serviceManager)
        {
            ServerConfig = serviceManager.Config;
            MqttClientService = serviceManager.MqttService;

            // Other services initialized from the service manager
            _logService = serviceManager.LogService;
            _videoStreamService = serviceManager.VideoStreamService;
            _imageReceiverService = serviceManager.ImageReceiverService;
        }

        // Commands for navigation to different views
        public Command NavigateToMQTTCommand => new Command(async () => await NavigateToMQTT());
        public Command NavigateToLogViewCommand => new Command(async () => await NavigateToLogView());
        public Command NavigateToPhotoViewCommand => new Command(async () => await NavigateToPhotoView());
        public Command NavigateToStreamViewCommand => new Command(async () => await NavigateToStreamView());

        // Method for navigating to the MQTT connection view
        private async Task NavigateToMQTT()
        {
            var mqttPage = new MqttDisplayView(_mqttClientService);
            await Shell.Current.Navigation.PushAsync(mqttPage);
        }

        // Method for navigating to the log view
        private async Task NavigateToLogView()
        {
            var logPage = new LogView(_logService);
            await Shell.Current.Navigation.PushAsync(logPage);
        }

        // Method for navigating to the photo view
        private async Task NavigateToPhotoView()
        {
            var photoPage = new PhotoView(_imageReceiverService);
            await Shell.Current.Navigation.PushAsync(photoPage);
        }

        // Method for navigating to the video stream view
        private async Task NavigateToStreamView()
        {
            var viewModel = new VideoStreamViewModel(_videoStreamService);
            await Shell.Current.Navigation.PushAsync(new VideoStreamView(viewModel));
        }

        // Implementation of INotifyPropertyChanged to update the UI on property changes
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
