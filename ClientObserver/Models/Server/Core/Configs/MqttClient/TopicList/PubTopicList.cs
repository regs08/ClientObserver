// Class for a MQTT clients publish topics 

using System;
namespace ClientObserver.Models.Server.Core.Configs.MqttClient.TopicList
{
    public class PubTopicList : TopicList
    {
        protected override bool IsValidTopic(string topic)
        {
            return base.IsValidTopic(topic);
        }
    }

}

