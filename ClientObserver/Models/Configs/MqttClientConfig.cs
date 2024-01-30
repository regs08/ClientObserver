using ClientObserver.Models.TopicList;

namespace ClientObserver.Models.Configs
{
    public class MqttClientConfig
    {
        public string BrokerAddress { get; set; }
        public string PortNumber { get; set; }
        public SubTopicList SubscriptionTopics { get; set; }
        public PubTopicList PublishTopics { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

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
