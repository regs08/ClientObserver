
using MQTTnet;
using MQTTnet.Client;
using System.Text;
using ClientObserver.Models.Server.Core.Configs;
using ClientObserver.Models.MessageEvents;
using ClientObserver.Models;
using Newtonsoft.Json.Linq;

namespace ClientObserver.Services
{
    public class MqttClientService
    {
        private IMqttClient mqttNetworkClient;
        private MqttClientModel mqttClientParameters;
        public MqttClientConfig Config;


        public event EventHandler<ImageMessageEventArgs> ImageReceived;
        public event EventHandler<ImageMessageEventArgs> TestImageReceived;
        public event EventHandler<TextMessageEventArgs> TextReceived;
        public event EventHandler<TextMessageEventArgs> PongReceived;
        public event EventHandler<LogMessageEventArgs> LogReceived;

        public MqttClientService(MqttClientConfig config)
        {
            Config = config;
            var factory = new MqttFactory();
            mqttClientParameters = new MqttClientModel(Config); 
            mqttNetworkClient = factory.CreateMqttClient();

            // Set up the message received handler
            mqttNetworkClient.ApplicationMessageReceivedAsync += HandleReceivedApplicationMessage;

        }

        private async Task HandleReceivedApplicationMessage(MqttApplicationMessageReceivedEventArgs e)
        {
            string topic = e.ApplicationMessage.Topic;
            byte[] payload = e.ApplicationMessage.PayloadSegment.ToArray();

            OnMqttMessageReceived(this, new MqttMessageEventArgs(topic, payload));

        }
        public async Task ConnectAsync()
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
            Console.WriteLine("Client already connected");
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

        public async Task<MqttClientPublishResult> PublishAsync(string topic, string payload)
        {
            if (mqttNetworkClient == null || !mqttNetworkClient.IsConnected)
            {
                throw new InvalidOperationException("MQTT client is not connected.");
            }

            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(Encoding.UTF8.GetBytes(payload))
                .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                .WithRetainFlag(false)
                .Build();

            return await mqttNetworkClient.PublishAsync(message);
        }
        // i think all messages should be sent as 'logs'
        // before categorizing the message into text, log, image we should first parse it. get relevent info from the
        // payload being sent e.g type
        // need to look into if this is better? too many messages coming in on the same topic?

        private void OnMqttMessageReceived(object sender, MqttMessageEventArgs e)
        {
            Console.WriteLine($"Message received on topic {e.Topic}");
            // Handle image messages
            if (e.Topic == "setup/image")
            {
                TestImageReceived?.Invoke(this, new ImageMessageEventArgs(e.Message));
                return;
            }
            if (e.Topic.EndsWith("/image"))
            {
                ImageReceived?.Invoke(this, new ImageMessageEventArgs(e.Message));
                return;
            }

            string messagePayload = Encoding.UTF8.GetString(e.Message);
            // Handle "pong" text messages
            if (messagePayload == "pong")
            {
                PongReceived?.Invoke(this, new TextMessageEventArgs(messagePayload));
                return;
            }
            if (e.Topic.EndsWith("vehicle/detections"))
            {
                Console.WriteLine("log Received");
                var jsonArray = JArray.Parse(messagePayload);
                LogReceived?.Invoke(this, new LogMessageEventArgs(jsonArray.ToString()));
            }

            else
            {
                // Handle as regular text message if array is empty
                TextReceived?.Invoke(this, new TextMessageEventArgs(messagePayload));
            }

        }
    }

}
