using System;
using System.Collections.Generic;
using ClientObserver.Models.Configs;
using ClientObserver.Models.TopicList;
namespace ClientObserver
{
    // Mqtt client model

    //todo find out what clean session is? maybe this is why im getting connection issues
    // todo crreate a brokert port class and broker address to get better typing 

    public class MqttClientModel
    {
        public string BrokerAddress { get; private set; }
        public int BrokerPort { get; private set; }
        public SubTopicList SubscriptionTopics { get; private set; }
        public PubTopicList PubTopics { get; private set;}
        // Additional properties for MQTT
        public string ClientId { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool CleanSession { get; private set; }
        public int KeepAlivePeriod { get; private set; }

        public MqttClientModel(MqttClientConfig config)
        {
            // Extract information from ServerConfig
            BrokerAddress = config.BrokerAddress;
            BrokerPort = int.Parse(config.PortNumber); 
            SubscriptionTopics = config.SubscriptionTopics;
            PubTopics = config.PublishTopics;

            // Initialize other properties
            ClientId = Guid.NewGuid().ToString(); // Or any other logic for client ID
            CleanSession = true;
            KeepAlivePeriod = 60; // Default value in seconds
            // Username and Password can be set as needed
        }

    }
}
