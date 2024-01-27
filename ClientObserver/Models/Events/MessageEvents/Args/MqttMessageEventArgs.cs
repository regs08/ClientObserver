// Mqtt message event class used when receiving a message from the mqtt client
// Note all messages from mqtt recieved arre cast as this class. Then based on
// the type of payload they are cast as either an image, log, ot test message
// event args

//todo update the logic in the message event args. first idea is have a service
// that will fill in MessageType in more implicity. 

using System;
using ClientObserver.Models.MessageEvents;

namespace ClientObserver.Models.MessageEvents 
{
    public class MqttMessageEventArgs : EventArgs
    {
        // Property to hold the topic of the MQTT message
        public string Topic { get; private set; }

        // Property to hold the message content
        public byte[] Message { get; private set; }
        public MessageEventArgs<Object> MessageType; 

        // Constructor
        public MqttMessageEventArgs(string topic, byte[] message)
        {
            Topic = topic;
            Message = message;
        }

    }
}
