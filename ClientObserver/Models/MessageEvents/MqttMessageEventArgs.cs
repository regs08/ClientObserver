using System;

namespace ClientObserver.Models.MessageEvents // Replace with your actual namespace
{
    public class MqttMessageEventArgs : EventArgs
    {
        // Property to hold the topic of the MQTT message
        public string Topic { get; private set; }

        // Property to hold the message content
        public byte[] Message { get; private set; }

        // Constructor
        public MqttMessageEventArgs(string topic, byte[] message)
        {
            Topic = topic;
            Message = message;
        }
    }
}
