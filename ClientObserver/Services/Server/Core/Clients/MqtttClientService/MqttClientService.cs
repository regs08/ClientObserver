using System;
using ClientObserver.Models.Server.Core.Clients;
using ClientObserver.Models.Server.Core.Configs;

namespace ClientObserver.Services.Server.Core.Clients
{
    /// <summary>
    /// Represents a service for managing MQTT client connections, extending the base functionality
    /// to include MQTT-specific connection steps.
    /// </summary>
    public class MqttClientService : BaseClientService
    {       

        public  MqttClientService(MqttClientModel clientModel)
        {
            ClientModel = clientModel;

        }
        protected override ConnectionStep[] ConnectionSteps
        {
            get
            {
                return new ConnectionStep[]
                {
                // Define the steps necessary for connecting an MQTT client.
                // Example:
                InitializeConnection,
                Authenticate,
                FinalizeConnection
                };
            }
        }

        private bool InitializeConnection()
        {
            // Implementation for initializing the connection
            return true; // Return true if successful, false otherwise
        }

        private bool Authenticate()
        {
            // Implementation for authentication
            return true; // Return true if successful, false otherwise
        }

        private bool FinalizeConnection()
        {
            // Implementation for finalizing the connection
            return true; // Return true if successful, false otherwise
        }



        /// <summary>
        /// Applies configuration settings to the MQTT client model.
        /// </summary>
        /// <param name="config">The configuration to apply. Expected to be of type MqttClientConfig.</param>
        public override void ApplyConfig(BaseConfig config)
        {
            if (config is MqttClientConfig mqttConfig)
            {
                // Ensure ClientModel is of the expected type before attempting to use MQTT-specific properties.
                if (ClientModel is MqttClientModel mqttClientModel)
                {
                    // Now that we've safely casted it, we can access MQTT-specific properties.
                    mqttClientModel.BrokerAddress = mqttConfig.BrokerAddress;
                    mqttClientModel.BrokerPort = int.TryParse(mqttConfig.PortNumber, out int port) ? port : 1883;
                    mqttClientModel.SubscriptionTopics = mqttConfig.SubscriptionTopics;
                    mqttClientModel.PubTopics = mqttConfig.PublishTopics;
                    mqttClientModel.Username = mqttConfig.Username;
                    mqttClientModel.Password = mqttConfig.Password;
                }
                else
                {
                    throw new InvalidCastException("ClientModel is not of type MqttClientModel");
                }
            }
            else
            {
                throw new ArgumentException("Config must be of type MqttClientConfig", nameof(config));
            }
        }
    }
}
