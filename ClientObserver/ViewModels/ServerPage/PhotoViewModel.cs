using System.Collections.ObjectModel;
using System.ComponentModel;
using ClientObserver.Services;

namespace ClientObserver.ViewModels
{
    public class PhotoViewModel : INotifyPropertyChanged
    {
        // Collection for storing and displaying photos in the UI
        public ObservableCollection<ImageSource> Photos { get; private set; }

        // Service used for receiving images
        private ImageReceiverService _imageReceiverService;

        // Stores the timestamp of the most recent picture
        private DateTime _timeOfMostRecentPicture;
        public DateTime TimeOfMostRecentPicture
        {
            get => _timeOfMostRecentPicture;
            set
            {
                _timeOfMostRecentPicture = value;
                OnPropertyChanged(nameof(TimeOfMostRecentPicture));
            }
        }

        // Event to notify when a property value changes
        public event PropertyChangedEventHandler PropertyChanged;

        // Constructor initializes the ViewModel with the ImageReceiverService
        public PhotoViewModel(ImageReceiverService imageReceiverService)
        {
            _imageReceiverService = imageReceiverService;

            // Subscribes to the ImagesUpdated event
            _imageReceiverService.ImagesUpdated += OnImagesUpdated;

            // Initializes the Photos collection with images received from the service
            Photos = new ObservableCollection<ImageSource>(_imageReceiverService.ReceivedImages);
        }

        // Handles the ImagesUpdated event from the ImageReceiverService
        private void OnImagesUpdated(object sender, EventArgs e)
        {
            // Clears the existing photos and adds the updated ones
            Photos.Clear();
            foreach (var image in _imageReceiverService.ReceivedImages)
            {
                Photos.Add(image);
            }

            // Notify the UI that the Photos collection has been updated
            OnPropertyChanged(nameof(Photos));
        }

        // Method to invoke the PropertyChanged event
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
