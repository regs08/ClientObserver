using System;
using ClientObserver.Models.MessageEvents;

namespace ClientObserver.Services
{
    public class ImageReceiverService
    {
        // Timestamp of the most recent picture received
        public DateTime TimeOfMostRecentPicture { get; private set; }

        // Maximum number of images to store
        private const int MaxStoredImages = 2;

        // List to store received images
        private List<ImageSource> _receivedImages = new List<ImageSource>();

        // Stores the latest test image received
        private ImageSource _receviedTestImage;

        // Reference to the MQTT client service
        private MqttClientService _mqttClientService;

        // Provides read-only access to the received images
        public IReadOnlyList<ImageSource> ReceivedImages => _receivedImages.AsReadOnly();

        // Event to notify when images are updated
        public event EventHandler ImagesUpdated;

        // Constructor that subscribes to MQTT client service events
        public ImageReceiverService(MqttClientService mqttClientService)
        {
            _mqttClientService = mqttClientService;

            // Subscribing to MQTT image received events
            _mqttClientService.ImageReceived += OnMqttImageReceived;
            _mqttClientService.TestImageReceived += OnMqttTestImageReceived;
        }

        // Handles the ImageReceived event from the MQTT client
        private void OnMqttImageReceived(object sender, ImageMessageEventArgs e)
        {
            // Converts the byte array to an ImageSource
            ImageSource imageData = ConvertToImageSource(e.Data);

            // Inserts the new image at the beginning of the list
            _receivedImages.Insert(0, imageData);

            // Updates the timestamp of the most recent picture
            TimeOfMostRecentPicture = DateTime.UtcNow; // Using UTC time to avoid timezone issues

            // Ensures the list size does not exceed the predefined maximum
            while (_receivedImages.Count > MaxStoredImages)
            {
                _receivedImages.RemoveAt(_receivedImages.Count - 1);
            }

            // Raises the ImagesUpdated event
            ImagesUpdated?.Invoke(this, new ImageMessageEventArgs(e.Data));
        }

        // Converts a byte array to an ImageSource
        private ImageSource ConvertToImageSource(byte[] imageData)
        {
            // Conversion logic
            return ImageSource.FromStream(() => new MemoryStream(imageData));
        }

        // Handles the TestImageReceived event from the MQTT client
        private void OnMqttTestImageReceived(object sender, ImageMessageEventArgs e)
        {
            // Converts the test image data to an ImageSource
            _receviedTestImage = ConvertToImageSource(e.Data);
        }
    }
}
