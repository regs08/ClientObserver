using ClientObserver.Models.TopicList;
using ClientObserver.Configs;
using Newtonsoft.Json;
using System.Text;

namespace ClientObserver.Configs
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


        //Initialize constructoir with the given config name 
        public MqttClientConfig() : base("MqttClientConfig")
        {
        }
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

        protected override string FormatForDisplay()
        {
            var sb = new StringBuilder();
            sb.AppendLine(base.ToString()); // Calls ToString on BaseConfig to include the name
            sb.AppendLine($"Broker Address: {BrokerAddress}");
            sb.AppendLine($"Port Number: {PortNumber}");
            sb.AppendLine($"Subscription Topics: {(SubscriptionTopics != null ? JsonConvert.SerializeObject(SubscriptionTopics, Formatting.Indented) : "null")}");
            sb.AppendLine($"Publish Topics: {(PublishTopics != null ? JsonConvert.SerializeObject(PublishTopics, Formatting.Indented) : "null")}");
            sb.AppendLine($"Username: {Username ?? "not set"}");
            // Password is not printed for security reasons
            return sb.ToString();
        }
        
    }
}
