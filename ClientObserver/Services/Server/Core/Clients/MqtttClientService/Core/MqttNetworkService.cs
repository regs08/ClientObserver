using System;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using ClientObserver.Models.Server.Core.Clients;

namespace ClientObserver.Services.Server.Core.Clients.MqttClientService
{
    /// <summary>
    /// Provides services for managing network interactions of an MQTT client, including connecting to the broker,
    /// subscribing to topics, and disconnecting.
    /// </summary>
	public class MqttNetworkService
    {
        public IMqttClient mqttNetworkClient;
        private MqttClientModel mqttClientParameters;

        /// <summary>
        /// Initializes a new instance of the MqttNetworkService with specified MQTT client model parameters.
        /// </summary>
        /// <param name="clientModel">The model containing MQTT client parameters.</param>
        public MqttNetworkService(MqttClientModel clientModel)
        {
            var factory = new MqttFactory();
            mqttNetworkClient = factory.CreateMqttClient();
            mqttClientParameters = clientModel;

            // Register a handler to update the connection status upon successful connection
            /*
            mqttNetworkClient.ConnectedAsync += args =>
            {
                mqttClientParameters.IsConnected.Value = true;
                return Task.CompletedTask;
            };
            */
        }

        /// <summary>
        /// Asynchronously connects to the MQTT broker using the configured client parameters.
        /// </summary>
        public async Task ConnectToMqttAsync()
        {
            if (mqttNetworkClient.IsConnected)
            {
                Console.WriteLine("Client already connected");
                return;
            }

            var options = new MqttClientOptionsBuilder()
                .WithClientId(mqttClientParameters.ClientId)
                .WithTcpServer(mqttClientParameters.BrokerAddress, mqttClientParameters.BrokerPort)
                .WithCleanSession(mqttClientParameters.CleanSession)
                .Build();

            Console.WriteLine("TRYING TO CONNECT");
            await mqttNetworkClient.ConnectAsync(options);
            await SubscribeToConfiguredTopicsAsync();
        }

        /// <summary>
        /// Asynchronously disconnects from the MQTT broker.
        /// </summary>
        public async Task DisconnectAsync()
        {
            await mqttNetworkClient.DisconnectAsync();
        }

        /// <summary>
        /// Asynchronously subscribes to a specific topic.
        /// </summary>
        /// <param name="topic">The topic to subscribe to.</param>
        public async Task SubscribeAsync(string topic)
        {
            if (!mqttNetworkClient.IsConnected)
            {
                throw new InvalidOperationException("MQTT client is not connected.");
            }

            await mqttNetworkClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(topic).Build());
        }

        /// <summary>
        /// Asynchronously subscribes the MQTT client to all configured topics.
        /// </summary>
        public async Task SubscribeToConfiguredTopicsAsync()
        {
            if (!mqttNetworkClient.IsConnected)
            {
                throw new InvalidOperationException("MQTT client is not connected.");
            }

            foreach (var topic in mqttClientParameters.SubscriptionTopics.Topics)
            {
                await SubscribeAsync(topic);
                Console.WriteLine($"Subscribed to topic: {topic}");
            }
        }
    }
}
