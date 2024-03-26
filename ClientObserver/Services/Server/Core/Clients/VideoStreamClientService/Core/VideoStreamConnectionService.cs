using System;
using System.Net.Http;
using System.Threading.Tasks;
using ClientObserver.Models.Server.Core.Clients;
using ClientObserver.Services.Server.Core.Base;

using ClientObserver.Models.Interfaces.Clients.ClientService;
namespace ClientObserver.Services.Server.Core.Clients.VideoStreamClientService
{
    public class VideoStreamConnectionService : BaseClientConnectionService, IDisposable
    {
        //public VideoStreamClient ClientModel { get; private set; }
        private bool disposed = false; // To detect redundant calls
        private VideoStreamClient videoStreamClient;
        public VideoStreamConnectionService(VideoStreamClient clientModel) : base(clientModel)
        {
            videoStreamClient = clientModel;
        }

        public override async Task<bool> InitializeConnection()
        {
            return await ConnectToHTTPAsync();
        }
        public override async Task<bool> Authenticate()
        {
            return true;
        }
        public override async Task<bool> FinalizeConnection()
        {
            return true;
        }
        public async Task<bool> ConnectToHTTPAsync()
        {
            try
            {
                var response = await videoStreamClient.HttpClient.GetAsync(videoStreamClient.VideoStreamUri, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                Console.WriteLine("Successfully Connected to Http");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to video stream: {ex.Message}");
                return false;
            }
        }
        
        // Disconnect or cleanup resources
        public override async Task<bool> DisconnectFromClient()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            //todo find some logic to check this 
            return true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Dispose managed state (managed objects).
                    videoStreamClient.HttpClient.Dispose();
                }

                // Free unmanaged resources (unmanaged objects) and override a finalizer below.
                // Set large fields to null.

                disposed = true;
            }
        }

        // Override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~VideoStreamConnectionService()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // Uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
    }
}
