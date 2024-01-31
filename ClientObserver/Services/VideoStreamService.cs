using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClientObserver.Services
{
    public class VideoStreamService
    {
        // Flag to indicate if the service is connected to the video stream
        private bool _isConnected;

        // URL of the video stream
        private readonly Uri _streamUrl;

        // HTTP client used for connecting to the video stream
        private readonly HttpClient _httpClient;

        // Public property to get the connection status
        public bool IsConnected => _isConnected;

        // Constructor that initializes the service with a video stream URL
        public VideoStreamService(Uri streamUrl)
        {
            _streamUrl = streamUrl;
            _httpClient = new HttpClient();
        }

        // Asynchronously connects to the video stream
        public async Task ConnectAsync()
        {
            try
            {
                // Tries to get a response from the stream URL
                var response = await _httpClient.GetAsync(_streamUrl, HttpCompletionOption.ResponseHeadersRead);

                // Ensures that the HTTP request was successful
                response.EnsureSuccessStatusCode();

                // Set the isConnected flag to true if the connection is successful
                _isConnected = true;
            }
            catch (Exception ex)
            {
                // Logs an error message if the connection fails
                Console.WriteLine($"Error connecting to video stream: {ex.Message}");

                // Set the isConnected flag to false due to the connection failure
                _isConnected = false;

                // Rethrow the exception to be handled by the caller
                throw;
            }
        }

        // Asynchronously disconnects from the video stream
        public Task DisconnectAsync()
        {
            // Add logic here to properly disconnect from the stream if necessary
            // Currently, it only sets the isConnected flag to false
            _isConnected = false;

            // Returns a completed task as the method is currently synchronous
            return Task.CompletedTask;
        }

        // Returns the stream URL if connected, otherwise null
        public Uri GetStreamUrl()
        {
            return _isConnected ? _streamUrl : null;
        }
    }
}
