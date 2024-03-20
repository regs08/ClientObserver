using System;
using ClientObserver.Models.Server.Core.Configs;
using ClientObserver.Services.Server.Core.Clients.VideoStreamClientService;

namespace ClientObserver.Models.Server.Core.Clients
{
    public class VideoStreamClient : BaseClientModel
    {

        public string StreamPortNumber { get; set; }


        public string StreamIP { get; set; }

        /// <summary>
        /// Generates the full URI for the video stream based on the IP and port.
        /// </summary>
        public Uri VideoStreamUri { get; set; }

        // HTTP client used for connecting to the video stream
        public HttpClient HttpClient;

        public VideoStreamClient(VideoStreamConfig config) : base(config, "VideoStreamClient")
        {
            Config = config;
            SetClientService(new VideoStreamClientService(this));
            InitializeWithConfig();

        }
    }
}



