using System;
using MQTTnet;
using MQTTnet.Client;
using ClientObserver.Models.Server.Core.Clients;

namespace ClientObserver.Services.Server.Core.Clients.MqtttClientService
{
	public class MqttNetworkService
	{
        public IMqttClient mqttNetworkClient;
        private MqttClientModel mqttClientParameters;
        public MqttNetworkService(MqttClientModel clientModel)
		{
            var factory = new MqttFactory();
            mqttNetworkClient = factory.CreateMqttClient();

            mqttClientParameters = clientModel;
            mqttNetworkClient.ConnectedAsync += (args) =>
            {
                mqttClientParameters.IsConnected.Value = true;
                return Task.CompletedTask;
            };
        }
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

            // Connect to the broker
            Console.WriteLine("TRYING TO CONNECT");
            await mqttNetworkClient.ConnectAsync(options);
            await SubscribeToConfiguredTopicsAsync();

        }
        public async Task DisconnectAsync()
        {
            await mqttNetworkClient.DisconnectAsync();
        }

        public async Task SubscribeAsync(string topic)
        {
            if (mqttNetworkClient == null || !mqttNetworkClient.IsConnected)
            {
                throw new InvalidOperationException("MQTT client is not connected.");
            }

            // Subscribe to the topic
            await mqttNetworkClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(topic).Build());
        }

        public async Task SubscribeToConfiguredTopicsAsync()
        {
            if (mqttNetworkClient == null || !mqttNetworkClient.IsConnected)
            {
                throw new InvalidOperationException("MQTT client is not connected.");
            }

            foreach (var topic in mqttClientParameters.SubscriptionTopics.Topics)
            {
                await SubscribeAsync(topic);
                Console.WriteLine(topic);

            }
        }
    }
}

