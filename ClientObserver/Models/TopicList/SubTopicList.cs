using System;
namespace ClientObserver.Models.TopicList
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

