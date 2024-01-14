using System.Collections.ObjectModel;
using System.ComponentModel;
using ClientObserver.Services;

namespace ClientObserver.ViewModels
{
    public class PhotoViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ImageSource> Photos { get; private set; }
        private ImageReceiverService _imageReceiverService;

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

        public event PropertyChangedEventHandler PropertyChanged;

        public PhotoViewModel(ImageReceiverService imageReceiverService)
        {
            _imageReceiverService = imageReceiverService;
            _imageReceiverService.ImagesUpdated += OnImagesUpdated;

            Photos = new ObservableCollection<ImageSource>(_imageReceiverService.ReceivedImages);
        }

        private void OnImagesUpdated(object sender, EventArgs e)
        {
            Photos.Clear();
            foreach (var image in _imageReceiverService.ReceivedImages)
            {
                Photos.Add(image);
            }
            OnPropertyChanged(nameof(Photos));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
