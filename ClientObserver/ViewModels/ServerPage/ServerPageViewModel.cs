using System.ComponentModel;
using ClientObserver.Services;
using ClientObserver.ViewModels;
namespace ClientObserver.Views
{
    public class ServerPageViewModel : INotifyPropertyChanged
    {
        private ServerConfig _serverConfig;
        private MqttClientService _mqttClientService;
        public ServerConfig ServerConfig
        {
            get => _serverConfig;
            set
            {
                _serverConfig = value;
                OnPropertyChanged(nameof(ServerConfig));
            }
        }
        public MqttClientService MqttClientService
        {
            get => _mqttClientService;
            set
            {
                _mqttClientService = value;
                OnPropertyChanged(nameof(MqttClientService));
            }
        }

        public ServerPageViewModel(ServiceManager serviceManager)
        {
            ServerConfig = serviceManager.Config;
            MqttClientService = serviceManager.MqttService;
            _logService = serviceManager.LogService;
            _videoStreamService = serviceManager.VideoStreamService;
            _imageReceiverService = serviceManager.ImageReceiverService;
        }
        // update to the property change, its needed to update the UI in rreal time 
        private LogService _logService;
        private VideoStreamService _videoStreamService;
        private ImageReceiverService _imageReceiverService;

        public Command NavigateToMQTTCommand => new Command(async () => await NavigateToMQTT());
        public Command NavigateToLogViewCommand => new Command(async () => await NavigateToLogView());
        public Command NavigateToPhotoViewCommand => new Command(async () => await NavigateToPhotoView());
        public Command NavigateToStreamViewCommand => new Command(async () => await NavigateToStreamView());


        private async Task NavigateToMQTT()
        {
            var mqttPage = new MqttConnectionView(_mqttClientService); 
            await Shell.Current.Navigation.PushAsync(mqttPage);
        }
        private async Task NavigateToLogView()
        {
            // look into creating logService as class param or here
            var logPage = new LogView(_logService);
            await Shell.Current.Navigation.PushAsync(logPage);
        }
        private async Task NavigateToPhotoView()
        {
            var photoPage = new PhotoView(_imageReceiverService);
            await Shell.Current.Navigation.PushAsync(photoPage);
        }
        private async Task NavigateToStreamView()
        {
            var viewModel = new VideoStreamViewModel(_videoStreamService);
            await Shell.Current.Navigation.PushAsync(new VideoStreamView(viewModel));

        }
        // Implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
