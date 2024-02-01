using ClientObserver.Models.TopicList;
using ClientObserver.Models.Configs;

namespace ClientObserver.Models.Configs
{
    /// <summary>
    /// Represents the configuration settings for an MQTT client.
    /// </summary>
    public class MqttClientConfig : BaseConfig // Inherits from BaseConfig
    {
        // Properties...
        public string BrokerAddress { get; set; }
        public string PortNumber { get; set; }
        public SubTopicList SubscriptionTopics { get; set; }
        public PubTopicList PublishTopics { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// Overrides the Validate method to check for null or empty properties in MqttClientConfig.
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

            // Specific validations for MqttClientConfig
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
            // Username and Password are optional and not checked here

            return null; // No null or empty properties found
        }
    }
}
