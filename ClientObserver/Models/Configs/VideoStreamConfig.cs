using System;

namespace ClientObserver.Models.Configs
{
    public class VideoStreamConfig
    {
        public string StreamPortNumber { get; set; }
        public string StreamIP { get; set; }

        public Uri VideoStreamUri
        {
            get
            {
                return new Uri($"http://{StreamIP}:{StreamPortNumber}/video");
            }
        }

        public string NullProperties()
        {
            if (string.IsNullOrEmpty(StreamIP))
            {
                return nameof(StreamIP);
            }
            if (string.IsNullOrEmpty(StreamPortNumber))
            {
                return nameof(StreamPortNumber);
            }

            return null;
        }
    }
}
