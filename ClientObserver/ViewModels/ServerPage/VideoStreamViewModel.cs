using System;
using System.ComponentModel;
using System.Windows.Input; // If using WPF
using ClientObserver.Services;
using System.IO;
namespace ClientObserver.ViewModels
{

    public class VideoStreamViewModel : INotifyPropertyChanged
    {
        private readonly VideoStreamService _videoStreamService;

        public event PropertyChangedEventHandler PropertyChanged;

        private string _videoUrl;
        public string VideoUrl
        {
            get => _videoUrl;
            set
            {
                _videoUrl = value;
                OnPropertyChanged(nameof(VideoUrl));
            }
        }
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
        public ICommand ConnectCommand { get; }
        public ICommand DisconnectCommand { get; }
        public ICommand ToggleStreamViewCommand { get;  }

        public VideoStreamViewModel(VideoStreamService videoStreamService)
        {
            _videoStreamService = videoStreamService;
            ConnectCommand = new Command(async () => await ConnectAsync());
            DisconnectCommand = new Command(async () => await Disconnect());
            ToggleStreamViewCommand = new Command(ToggleStreamView);
            _isConnected = videoStreamService.IsConnected;

        }
        private void ToggleStreamView()
        {
            Console.WriteLine(VideoUrl);
            if (_videoStreamService.IsConnected)
            {
                if (string.IsNullOrEmpty(VideoUrl))
                {
                    // If not currently viewing, start viewing the stream
                    // Convert the Uri to a string
                    VideoUrl = _videoStreamService.GetStreamUrl().ToString();
                }
                else
                {
                    // If currently viewing, stop viewing the stream
                    VideoUrl = null;
                }
            }
            else
            {
                Console.WriteLine("Not connected to video stream.");
            }
        }

        private async Task ConnectAsync()
        {
            Console.WriteLine("connecting to ");
            await _videoStreamService.ConnectAsync();
            IsConnected = _videoStreamService.IsConnected; // Update the ViewModel's IsConnected property
        }

        private async Task Disconnect()
        {
            await _videoStreamService.DisconnectAsync();
            VideoUrl = null; // Clear the WebView
            IsConnected = _videoStreamService.IsConnected; // Update the ViewModel's IsConnected property

        }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}

