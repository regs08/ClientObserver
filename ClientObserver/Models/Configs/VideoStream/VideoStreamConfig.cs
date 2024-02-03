using System;
using ClientObserver.Configs; // Assuming BaseConfig is in this namespace

namespace ClientObserver.Configs
{
    /// <summary>
    /// Configuration settings for a video stream.
    /// </summary>
    public class VideoStreamConfig : BaseConfig // Inherits from BaseConfig
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
        /// Overrides the Validate method to check for null or empty properties in VideoStreamConfig.
        /// </summary>
        /// <returns>
        /// The name of the first property found to be null or empty, or null if all properties are set.
        /// </returns>
        public override string Validate()
        {
            // Use the base class's validation method first
            var baseValidationResult = base.Validate();
            if (!string.IsNullOrEmpty(baseValidationResult))
            {
                return baseValidationResult;
            }

            // Specific validations for VideoStreamConfig
            if (string.IsNullOrEmpty(StreamIP))
            {
                return nameof(StreamIP);
            }
            if (string.IsNullOrEmpty(StreamPortNumber))
            {
                return nameof(StreamPortNumber);
            }

            return null; // No null or empty properties found
        }
    }
}
