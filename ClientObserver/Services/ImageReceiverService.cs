using System;
using ClientObserver.Models.MessageEvents;

namespace ClientObserver.Services
    {
        public class ImageReceiverService
        {
            public DateTime TimeOfMostRecentPicture { get; private set; } // Timestamp of the most recent picture

            private const int MaxStoredImages = 2; // Max number of images to store
            private List<ImageSource> _receivedImages = new List<ImageSource>();
            private ImageSource _receviedTestImage;
            private MqttClientService _mqttClientService;

            public IReadOnlyList<ImageSource> ReceivedImages => _receivedImages.AsReadOnly();

            public event EventHandler ImagesUpdated;

            public ImageReceiverService(MqttClientService mqttClientService)
            {
                _mqttClientService = mqttClientService;
                _mqttClientService.ImageReceived += OnMqttImageReceived;
                _mqttClientService.TestImageReceived += OnMqttTestImageReceived;
            }

            private void OnMqttImageReceived(object sender, ImageMessageEventArgs e)
            {
                // Since imageData is already a JPEG byte array, just create an ImageSource from it
                ImageSource imageData = ConvertToImageSource(e.ImageData);
                // Add the latest image to the start of the list
                _receivedImages.Insert(0, imageData);
                TimeOfMostRecentPicture = DateTime.UtcNow; // Using UTC time to avoid timezone issues

            // Ensure the list size does not exceed MaxStoredImages
                while (_receivedImages.Count > MaxStoredImages)
                {
                    _receivedImages.RemoveAt(_receivedImages.Count - 1);
                }

                ImagesUpdated?.Invoke(this, new ImageMessageEventArgs(e.ImageData));
            }
            private ImageSource ConvertToImageSource(byte[] imageData)
            {
                return ImageSource.FromStream(() => new MemoryStream(imageData));
            }

            private void OnMqttTestImageReceived(object sender, ImageMessageEventArgs e)
            {
                _receviedTestImage = ConvertToImageSource(e.ImageData);
            }
        }


    }
    



