// config for connencting to our 'server' 

using ClientObserver.Models.Configs;

namespace ClientObserver
{
    public class ServerConfig
    {
        /// <summary>
        /// Configuration settings for connecting to a server with various components like MQTT, video streaming, and model parameters.
        /// </summary>
        /// <summary>
        /// Name of the server.
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// Configuration settings for the MQTT client.
        /// </summary>
        public MqttClientConfig MqttClientConfig { get; set; }

        /// <summary>
        /// Configuration settings for the video stream.
        /// </summary>
        public VideoStreamConfig VideoStreamConfig { get; set; }

        /// <summary>
        /// Configuration settings for model parameters.
        /// </summary>
        public ModelParamConfig ModelParamConfig { get; set; }

        /// <summary>
        /// Constructor to initialize the ServerConfig with specific configurations.
        /// </summary>
        /// <param name="serverName">Name of the server.</param>
        /// <param name="mqttClientConfig">MQTT client configuration.</param>
        /// <param name="videoStreamConfig">Video stream configuration.</param>
        /// <param name="modelParamConfig">Model parameters configuration.</param>

        public ServerConfig(string serverName, MqttClientConfig mqttClientConfig, VideoStreamConfig videoStreamConfig, ModelParamConfig modelParamConfig)
        {
            ServerName = serverName;
            MqttClientConfig = mqttClientConfig;
            VideoStreamConfig = videoStreamConfig;
            ModelParamConfig = modelParamConfig;
        }
        /// <summary>
        /// Verifies if all configurations are properly set.
        /// </summary>
        /// <returns>A string indicating which configuration is not set, or null if all are set.</returns>
        public string VerifyConfigurations()
        {
            if (MqttClientConfig == null)
            {
                return "MqttClientConfig is not set.";
            }
            else
            {
                string mqttConfigNullProperty = MqttClientConfig.NullProperties();
                if (!string.IsNullOrEmpty(mqttConfigNullProperty))
                {
                    return $"MqttClientConfig: {mqttConfigNullProperty} is not set.";
                }
            }

            if (VideoStreamConfig == null)
            {
                return "VideoStreamConfig is not set.";
            }
            else
            {
                string videoConfigNullProperty = VideoStreamConfig.NullProperties();
                if (!string.IsNullOrEmpty(videoConfigNullProperty))
                {
                    return $"VideoStreamConfig: {videoConfigNullProperty} is not set.";
                }
            }

            if (ModelParamConfig == null)
            {
                return "ModelParamConfig is not set.";
            }
            else
            {
                string modelConfigNullProperty = ModelParamConfig.NullProperties();
                if (!string.IsNullOrEmpty(modelConfigNullProperty))
                {
                    return $"ModelParamConfig: {modelConfigNullProperty} is not set.";
                }
            }

            // If all configurations are complete
            return null;
        }
        /// <summary>
        /// Gets a formatted string displaying the configuration details.
        /// </summary>
        public string FormattedDisplay
        {
            get
            {
                return FormatForDisplay();
            }
        }
        public string FormatForDisplay()
        {
            return $"Server Name: {ServerName}\n" +
                   $"BrokerAddress Address: {MqttClientConfig.BrokerAddress}\n" +
                   $"MQTT Port Number: {MqttClientConfig.PortNumber}\n" +
                   $"Subscription Topics: {MqttClientConfig.SubscriptionTopics}\n" +
                   $"Pub Topics: {MqttClientConfig.PublishTopics}\n" +
                   $"Stream IP Address: {VideoStreamConfig.StreamIP}\n" +
                   $"Video Stream Port Number: {VideoStreamConfig.StreamPortNumber}\n" +
                   $"Selected Labels: {string.Join(", ", ModelParamConfig.SelectedLabels)}\n" +
                   $"Available Labels: {string.Join(", ", ModelParamConfig.AvailableLabels)}\n" +
                   $"Confidence Threshold: {ModelParamConfig.ConfidenceThreshold}";
        }

        /// <summary>
        /// Validates the ServerConfig object to ensure all configurations are valid.
        /// </summary>
        /// <returns>True if the configuration is valid, otherwise false.</returns>
        public bool IsValid()
        {
            // Validate string properties
            if (string.IsNullOrWhiteSpace(ServerName) ||
                string.IsNullOrWhiteSpace(MqttClientConfig.BrokerAddress) ||
                string.IsNullOrWhiteSpace(VideoStreamConfig.StreamIP) ||
                string.IsNullOrWhiteSpace(VideoStreamConfig.StreamPortNumber) ||
                string.IsNullOrWhiteSpace(MqttClientConfig.PortNumber))
            {
                return false;
            }

            // Validate port numbers as positive integers
            if (!IsPositiveInteger(VideoStreamConfig.StreamPortNumber) || !IsPositiveInteger(MqttClientConfig.PortNumber))
            {
                return false;
            }

            // Validate lists
            if (ModelParamConfig.SelectedLabels == null || !ModelParamConfig.SelectedLabels.Any() ||
                ModelParamConfig.AvailableLabels == null || !ModelParamConfig.AvailableLabels.Any())
            {
                return false;
            }

            // Validate topic lists
            if (MqttClientConfig.SubscriptionTopics == null || MqttClientConfig.SubscriptionTopics.Topics == null || !MqttClientConfig.SubscriptionTopics.Topics.Any() ||
                MqttClientConfig.PublishTopics == null || MqttClientConfig.PublishTopics.Topics == null || !MqttClientConfig.PublishTopics.Topics.Any())
            {
                return false;
            }

            // Validate numeric values
            if (ModelParamConfig.ConfidenceThreshold < 0)
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// Checks if a given string represents a positive integer.
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <returns>True if the string is a positive integer, otherwise false.</returns>
        private bool IsPositiveInteger(string value)
        {
            return int.TryParse(value, out int result) && result > 0;
        }
    }
}



