using System;
using ClientObserver.Models.Server.Core.Configs;
using ClientObserver.Models.Server.Core.Configs.MqttClient.TopicList;

namespace ClientObserver.Models.Server.Core.Clients
{
	public class MqttClientModel: BaseClientModel
    {
        public string BrokerAddress { get;  set; }
        public int BrokerPort { get;  set; }
        public SubTopicList SubscriptionTopics { get;  set; }
        public PubTopicList PubTopics { get;  set; }
        // Additional properties for MQTT
        public string ClientId { get;  set; }
        public string Username { get;  set; }
        public string Password { get;  set; }
        public bool CleanSession { get;  set; }
        public int KeepAlivePeriod { get;  set; }

        public MqttClientConfig Config { get; set; }

        public MqttClientModel(MqttClientConfig  config): base(config, name: "MqttClientModel")
		{
            if (config!= null)
            {
                Config = config;
                ApplyConfig();
            }
        }
        // todo look at why the config is losing its value once 
        public override void ApplyConfig()
        {
            
            BrokerAddress = Config.BrokerAddress;
            BrokerPort = int.TryParse(Config.PortNumber, out int port) ? port : 1883;
            SubscriptionTopics = Config.SubscriptionTopics;
            PubTopics = Config.PublishTopics;
            Username = Config.Username;
            Password = Config.Password;
        }
    }
}


