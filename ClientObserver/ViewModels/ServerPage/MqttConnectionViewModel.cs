using System.ComponentModel;
using System.Windows.Input;
using ClientObserver.Services;
using ClientObserver.Models.MessageEvents;
using System.Threading.Tasks;
//todo 
// when receving images they are showing n the photo view as well
// should seperate this concern e.g pass in a config and createa seperate mqtt Service 
namespace ClientObserver
{
    public class MqttConnectionViewModel : INotifyPropertyChanged
    {

        // Services
        private MqttClientService _mqttClientService;
        private ImageReceiverService _imageReceiverService;

        // Commands
        public ICommand ConnectCommand { get; private set; }
        public ICommand SendPingCommand { get; private set; }
        public ICommand DisplayImageCommand { get; private set; }


        public string ReceivedText { get; private set; }
        public IReadOnlyList<ImageSource> ReceivedImages => _imageReceiverService.ReceivedImages;

        private bool _isConnected;
        public bool IsConnected
        {
            get => _isConnected;
            set
            {
                _isConnected = value;
                OnPropertyChanged(nameof(IsConnected));
            }
        }

        private ImageSource _displayedImageSource;
        public ImageSource DisplayedImageSource
        {
            get => _displayedImageSource;
            private set
            {
                _displayedImageSource = value;
                OnPropertyChanged(nameof(DisplayedImageSource));
            }
        }

        public MqttConnectionViewModel(MqttClientService mqttClientService)
        {
            _mqttClientService = mqttClientService;
            _imageReceiverService = new ImageReceiverService(mqttClientService);

            // Subscribing to methods.. 
            _mqttClientService.PongReceived += OnMqttPingReceived;
            _imageReceiverService.ImagesUpdated += OnImagesUpdated;

            // Initialize commands
            ConnectCommand = new Command(async () => await ConnectToMqtt());
            SendPingCommand = new Command(async () => await SendPing());
            DisplayImageCommand = new Command(DisplayImage);

        }

        private async Task ConnectToMqtt()
        {
            try
            {
                await _mqttClientService.ConnectAsync();
                await RequestTestImage();

                IsConnected = true;
            }
            catch
            {
                IsConnected = false;
            }
        }

        private async Task SendPing()
        {
            Console.WriteLine("sending ping!");
            await _mqttClientService.PublishAsync(topic: "test/ping", payload:"ping");
        }
        private async Task RequestTestImage()
        {
            Console.Write("asking for image!");
            await _mqttClientService.PublishAsync(topic: "test/image", payload: "image");
        }
        private async void DisplayImage()
        {
            // ask app to send image 

            if (ReceivedImages.Any())
            {
                DisplayedImageSource = ReceivedImages.First();
            }
        }

        private void OnMqttPingReceived(object sender, TextMessageEventArgs e)
        {
            ReceivedText = e.Text;
            if (ReceivedText == "pong")
            {
                OnPropertyChanged(nameof(ReceivedText));

            }
        }

        private void OnImagesUpdated(object sender, EventArgs e)
        {
            // Notify the UI that the images have been updated
            OnPropertyChanged(nameof(ReceivedImages));
        }

        // Implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
