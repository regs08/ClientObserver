using System;
using System.Threading.Tasks;
using ClientObserver.Models.Server.Core.Clients;
using ClientObserver.Models.Server.Core.Configs;

namespace ClientObserver.Services.Server.Core.Clients.MqttClientService
{
    /// <summary>
    /// Manages MQTT client connections, providing functionality for initializing, authenticating,
    /// and finalizing connections to an MQTT broker based on specified configurations.
    /// </summary>
    public class MqttClientService : BaseClientService
    {
        public MqttConnectionService mqttConnectionService => (MqttConnectionService)ConnectionService;
        public MqttClientModel MqttClientModel => ClientModel as MqttClientModel;

        /// <summary>
        /// Initializes a new instance of the MqttClientService with the specified MQTT client model.
        /// </summary>
        /// <param name="clientModel">The client model containing MQTT connection configurations.</param>
        public MqttClientService(MqttClientModel clientModel)
            : base(clientModel, new MqttConnectionService(clientModel))
        {

            // Register event handler for MQTT client connection
            mqttConnectionService.mqttNetworkClient.ConnectedAsync += args =>
            {
                ClientModel.IsConnected.Value = true;
                return Task.CompletedTask;
            };
        }


        /// <summary>
        /// Applies configuration settings to the MQTT client model.
        /// </summary>
        /// <param name="config">The configuration to apply. Expected to be of type MqttClientConfig.</param>
        /// <exception cref="InvalidCastException">Thrown if the client model is not of the expected MQTT client type.</exception>
        /// <exception cref="ArgumentException">Thrown if the provided configuration is not of the expected MQTT client configuration type.</exception>
        public override void ApplyConfig(BaseConfig config)
        {
            if (config is MqttClientConfig mqttConfig)
            {
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
