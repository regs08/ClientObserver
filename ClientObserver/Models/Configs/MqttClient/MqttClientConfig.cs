using ClientObserver.Models.TopicList;

namespace ClientObserver.Models.Configs
{
    /// <summary>
    /// Represents the configuration settings for an MQTT client.
    /// </summary>
    public class MqttClientConfig
    {
        /// <summary>
        /// IP address or hostname of the MQTT broker.
        /// </summary>
        public string BrokerAddress { get; set; }

        /// <summary>
        /// Port number on which the MQTT broker is running.
        /// </summary>
        public string PortNumber { get; set; }

        /// <summary>
        /// List of topics that the client should subscribe to.
        /// </summary>
        public SubTopicList SubscriptionTopics { get; set; }

        /// <summary>
        /// List of topics that the client will publish messages to.
        /// </summary>
        public PubTopicList PublishTopics { get; set; }

        /// <summary>
        /// Optional username for connecting to the MQTT broker.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Optional password for connecting to the MQTT broker.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Checks for any null or empty properties in the configuration.
        /// </summary>
        /// <returns>The name of the first property found to be null or empty, or null if all properties are set.</returns>
        public string NullProperties()
        {
            if (string.IsNullOrEmpty(BrokerAddress))
            {
                return nameof(BrokerAddress);
            }
            if (string.IsNullOrEmpty(PortNumber))
            {
                return nameof(PortNumber);
            }
            if (SubscriptionTopics == null)
            {
                return nameof(SubscriptionTopics);
            }
            if (PublishTopics == null)
            {
                return nameof(PublishTopics);
            }
            // Username and Password are optional, so no need to check them

            return null;
        }
    }
}
