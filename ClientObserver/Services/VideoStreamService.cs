using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClientObserver.Services
{
    public class VideoStreamService
    {
        private bool _isConnected;
        private readonly Uri _streamUrl;
        private readonly HttpClient _httpClient;

        public bool IsConnected => _isConnected;

        public VideoStreamService(Uri streamUrl)
        {
            _streamUrl = streamUrl;
            _httpClient = new HttpClient();
        }

        public async Task ConnectAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(_streamUrl, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                _isConnected = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to video stream: {ex.Message}");
                _isConnected = false;
                throw;
            }
        }

        public Task DisconnectAsync()
        {
            // Add logic here to properly disconnect from the stream if necessary
            _isConnected = false;
            return Task.CompletedTask;
        }

        public Uri GetStreamUrl()
        {
            return _isConnected ? _streamUrl : null;
        }
    }
}
