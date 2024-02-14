using System;
using ClientObserver.Models.Server.Core.Clients;
using ClientObserver.Models.Server.Core.Configs;

namespace ClientObserver.Services.Clients
{

    /// <summary>
    /// Represents a service for managing MQTT client connections, extending the base functionality
    /// to include MQTT-specific connection steps.
    /// </summary>
    public class MqttClientService : BaseClientService<MqttClientConfig>
    {
        // Backing field to hold the specific model type.
        private MqttClientModel _clientModel;

        public override BaseClientModel ClientModel
        {
            get => _clientModel;
            set => _clientModel = (MqttClientModel)value; // Cast when setting the value.
        }
        /// <summary>
        /// Defines the connection steps specific to the MQTT client service.
        /// </summary>
        protected override ConnectionStep[] ConnectionSteps => new ConnectionStep[]
        {
            ValidateBrokerAddress, // First step: Validate the MQTT broker address.
            EstablishConnection    // Second step: Establish a connection to the MQTT broker.
        };

        public MqttClientService() : base(name: "MqttClientModel")
        {
        }

        /// <summary>
        /// Validates the broker address specified in the MQTT client model.
        /// </summary>
        /// <returns>True if the address is valid, otherwise false.</returns>
        private bool ValidateBrokerAddress()
        {
            // Implement validation logic here.
            // This example always returns true for simplicity.
            return true;
        }

        /// <summary>
        /// Attempts to establish a connection with the MQTT broker.
        /// </summary>
        /// <returns>True if the connection is successfully established, otherwise false.</returns>
        private bool EstablishConnection()
        {
            // Implement connection logic here.
            // This example always returns true for simplicity.
            return true;
        }

        public override void ApplyConfig(MqttClientConfig config)
        {
            _clientModel.BrokerAddress = config.BrokerAddress;
            _clientModel.BrokerPort = int.TryParse(config.PortNumber, out int port) ? port : 1883;
            _clientModel.SubscriptionTopics = config.SubscriptionTopics;
            _clientModel.PubTopics = config.PublishTopics;
            _clientModel.Username = config.Username;
            _clientModel.Password = config.Password;
        }

    }
}
