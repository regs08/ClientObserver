using System;
using System.Collections.ObjectModel;

namespace ClientObserver.Models.TopicList
{
    public class TopicList
    {
        public ObservableCollection<string> Topics { get;  set; }

        public TopicList()
        {
            Topics = new ObservableCollection<string>();
        }

        public void AddTopic(string topic)
        {
            if (IsValidTopic(topic) && !Topics.Contains(topic))
            {
                Topics.Add(topic);
            }
        }

        public void RemoveTopic(string topic)
        {
            if (Topics.Contains(topic))
            {
                Topics.Remove(topic);
            }
        }

        protected virtual bool IsValidTopic(string topic)
        {
            // Common validation logic
            return !string.IsNullOrWhiteSpace(topic);
        }

        public override string ToString()
        {
            return string.Join(", ", Topics);
        }

        // Other common methods or properties, if any
    }
}
