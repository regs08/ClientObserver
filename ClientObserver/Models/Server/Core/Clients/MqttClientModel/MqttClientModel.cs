using System;
using ClientObserver.Models.Server.Core.Configs;
using ClientObserver.Models.Server.Core.Configs.MqttClient.TopicList;
using ClientObserver.Services.Server.Core.Clients.MqtttClientService;
namespace ClientObserver.Models.Server.Core.Clients
{
    public class MqttClientModel : BaseClientModel
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

        public MqttClientModel(MqttClientConfig config) : base(config, "MqttClientModel")
        {
            Config = config;
            CleanSession = true;
            SetClientService(new MqttClientService(this));
            InitializeWithConfig();
            //ConnectionStatus 
        }
    }
}



