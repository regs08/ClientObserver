// Class for a MQTT clients subscribe topics 


using System;
namespace ClientObserver.Models.Server.Core.Configs.MqttClient.TopicList
{
    public class SubTopicList : TopicList
    {
        protected override bool IsValidTopic(string topic)
        {
            // Special validation logic for subscription topics
            return base.IsValidTopic(topic);
        }
    }

}

