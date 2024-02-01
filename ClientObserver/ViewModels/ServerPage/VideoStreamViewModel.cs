using System;
using System.ComponentModel;
using System.Windows.Input; // If using WPF
using ClientObserver.Services;
using System.IO;

namespace ClientObserver.ViewModels
{
    public class VideoStreamViewModel : INotifyPropertyChanged
    {
        // Service for handling video stream operations
        private readonly VideoStreamService _videoStreamService;

        // Event for property change notifications
        public event PropertyChangedEventHandler PropertyChanged;

        // URL of the video stream for display
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

        // Flag to indicate connection status to the video stream
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

        // Commands for UI interaction
        public ICommand ConnectCommand { get; }
        public ICommand DisconnectCommand { get; }
        public ICommand ToggleStreamViewCommand { get; }

        // Constructor
        public VideoStreamViewModel(VideoStreamService videoStreamService)
        {
            _videoStreamService = videoStreamService;

            // Initialize commands for connect, disconnect, and toggle stream view
            ConnectCommand = new Command(async () => await ConnectAsync());
            DisconnectCommand = new Command(async () => await Disconnect());
            ToggleStreamViewCommand = new Command(ToggleStreamView);

            // Set initial connection status
            _isConnected = videoStreamService.IsConnected;
        }

        // Toggles the video stream view
        private void ToggleStreamView()
        {
            Console.WriteLine(VideoUrl);
            if (_videoStreamService.IsConnected)
            {
                if (string.IsNullOrEmpty(VideoUrl))
                {
                    // If not currently viewing, start viewing the stream
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

        // Connects to the video stream
        private async Task ConnectAsync()
        {
            Console.WriteLine("connecting to video stream");
            await _videoStreamService.ConnectAsync();
            IsConnected = _videoStreamService.IsConnected; // Update the ViewModel's IsConnected property
        }

        // Disconnects from the video stream
        private async Task Disconnect()
        {
            await _videoStreamService.DisconnectAsync();
            VideoUrl = null; // Clear the video URL
            IsConnected = _videoStreamService.IsConnected; // Update the ViewModel's IsConnected property
        }

        // Invokes the PropertyChanged event
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
