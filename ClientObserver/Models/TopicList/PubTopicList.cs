using System;
namespace ClientObserver.Models.TopicList
{
    public class PubTopicList : TopicList
    {
        protected override bool IsValidTopic(string topic)
        {
            return base.IsValidTopic(topic);
        }
    }

}

