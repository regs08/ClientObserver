using System;
using ClientObserver.Models.Server.Core.Configs;
using ClientObserver.Models.Server.Core.Clients;
using ClientObserver.Services.Server.Core.Clients.MqtttClientService;
using ClientObserver.Models.Server.Core.Configs.MqttClient.TopicList;

namespace ClientObserver.Models.Server.Core.Clients
{
    /// <summary>
    /// Represents an MQTT client model, encapsulating the properties and functionalities specific to MQTT protocol communication, such as connection parameters and topic subscriptions.
    /// Inherits from BaseClientModel to utilize common client model features while providing MQTT-specific implementations.
    /// </summary>
    public class MqttClientModel : BaseClientModel
    {
        /// <summary>
        /// Gets or sets the broker's network address.
        /// </summary>
        public string BrokerAddress { get; set; }

        /// <summary>
        /// Gets or sets the broker's network port.
        /// </summary>
        public int BrokerPort { get; set; }

        /// <summary>
        /// Gets or sets the list of subscription topics for this client.
        /// </summary>
        public SubTopicList SubscriptionTopics { get; set; }

        /// <summary>
        /// Gets or sets the list of publication topics for this client.
        /// </summary>
        public PubTopicList PubTopics { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the MQTT client.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the username for MQTT broker authentication.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password for MQTT broker authentication.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the session should be cleaned upon connection.
        /// </summary>
        public bool CleanSession { get; set; }

        /// <summary>
        /// Gets or sets the keep alive period in seconds for the MQTT client.
        /// </summary>
        public int KeepAlivePeriod { get; set; }

        /// <summary>
        /// Initializes a new instance of the MqttClientModel class with a specific MQTT client configuration.
        /// Sets up the client service and initializes the client with the provided configuration.
        /// </summary>
        /// <param name="config">The MQTT client configuration to use.</param>
        public MqttClientModel(MqttClientConfig config) : base(config, "MqttClientModel")
        {
            Config = config;
            CleanSession = true; // Defaulting to a clean session for a new connection
            SetClientService(new MqttClientService(this)); // Initialize the MQTT client service with this model
            InitializeWithConfig(); // Apply the configuration to the client service
        }
    }
}
