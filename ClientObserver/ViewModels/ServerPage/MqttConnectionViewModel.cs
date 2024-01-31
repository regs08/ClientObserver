using System.ComponentModel;
using System.Windows.Input;
using ClientObserver.Services;
using ClientObserver.Models.MessageEvents;
using System.Threading.Tasks;

namespace ClientObserver
{
    public class MqttConnectionViewModel : INotifyPropertyChanged
    {
        // Services used for MQTT communication and image reception
        private MqttClientService _mqttClientService;
        private ImageReceiverService _imageReceiverService;

        // Commands for UI interaction
        public ICommand ConnectCommand { get; private set; }
        public ICommand SendPingCommand { get; private set; }
        public ICommand DisplayImageCommand { get; private set; }

        // Holds the latest text received from MQTT
        public string ReceivedText { get; private set; }

        // Readonly property to expose received images from the ImageReceiverService
        public IReadOnlyList<ImageSource> ReceivedImages => _imageReceiverService.ReceivedImages;

        // Indicates if the MQTT connection is active
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

        // Source of the image to be displayed
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

        // Constructor
        public MqttConnectionViewModel(MqttClientService mqttClientService)
        {
            _mqttClientService = mqttClientService;
            _imageReceiverService = new ImageReceiverService(mqttClientService);

            // Subscribing to MQTT client events
            _mqttClientService.PongReceived += OnMqttPingReceived;
            _imageReceiverService.ImagesUpdated += OnImagesUpdated;

            // Initializing commands for UI interaction
            ConnectCommand = new Command(async () => await ConnectToMqtt());
            SendPingCommand = new Command(async () => await SendPing());
            DisplayImageCommand = new Command(DisplayImage);
        }

        // Connects to MQTT
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

        // Sends a ping message via MQTT
        private async Task SendPing()
        {
            Console.WriteLine("sending ping!");
            await _mqttClientService.PublishAsync(topic: "test/ping", payload: "ping");
        }

        // Requests a test image via MQTT
        private async Task RequestTestImage()
        {
            Console.Write("asking for image!");
            await _mqttClientService.PublishAsync(topic: "test/image", payload: "image");
        }

        // Displays the first image in the received images collection
        private async void DisplayImage()
        {
            if (ReceivedImages.Any())
            {
                DisplayedImageSource = ReceivedImages.First();
            }
        }

        // Handles the receipt of a ping message
        private void OnMqttPingReceived(object sender, TextMessageEventArgs e)
        {
            ReceivedText = e.Data;
            if (ReceivedText == "pong")
            {
                OnPropertyChanged(nameof(ReceivedText));
            }
        }

        // Handles updates to the images received from MQTT
        private void OnImagesUpdated(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(ReceivedImages));
        }

        // Implementation of INotifyPropertyChanged to update the UI on property changes
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
