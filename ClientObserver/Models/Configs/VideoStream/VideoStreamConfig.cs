using System;

namespace ClientObserver.Models.Configs
{
    /// <summary>
    /// Configuration settings for a video stream.
    /// </summary>
    public class VideoStreamConfig
    {
        /// <summary>
        /// The port number on which the video stream is accessible.
        /// </summary>
        public string StreamPortNumber { get; set; }

        /// <summary>
        /// The IP address or hostname of the video stream server.
        /// </summary>
        public string StreamIP { get; set; }

        /// <summary>
        /// Generates the full URI for the video stream based on the IP and port.
        /// </summary>
        public Uri VideoStreamUri
        {
            get
            {
                // Constructs the URI using the StreamIP and StreamPortNumber
                return new Uri($"http://{StreamIP}:{StreamPortNumber}/video");
            }
        }

        /// <summary>
        /// Checks for any null or empty properties in the video stream configuration.
        /// </summary>
        /// <returns>
        /// The name of the first property found to be null or empty, or null if all properties are set.
        /// </returns>
        public string NullProperties()
        {
            // Check if StreamIP is null or empty
            if (string.IsNullOrEmpty(StreamIP))
            {
                return nameof(StreamIP);
            }
            // Check if StreamPortNumber is null or empty
            if (string.IsNullOrEmpty(StreamPortNumber))
            {
                return nameof(StreamPortNumber);
            }

            // All properties are set
            return null;
        }
    }
}
