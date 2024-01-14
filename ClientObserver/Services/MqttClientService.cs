﻿using MQTTnet;
using MQTTnet.Client;
using System.Text;
using ClientObserver.Models;
using ClientObserver.Models.MessageEvents;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ClientObserver.Services
{
    public class MqttClientService
    {
        private IMqttClient _mqttClient;
        private MqttClientModel _mqttClientModel;
        public ServerConfig Config;


        public event EventHandler<ImageMessageEventArgs> ImageReceived;
        public event EventHandler<ImageMessageEventArgs> TestImageReceived;
        public event EventHandler<TextMessageEventArgs> TextReceived;
        public event EventHandler<TextMessageEventArgs> PongReceived;
        public event EventHandler<LogMessageEventArgs> LogReceived;

        public MqttClientService(ServerConfig config)
        {
            Config = config;
            var factory = new MqttFactory();
            _mqttClientModel = new MqttClientModel(Config); 
            _mqttClient = factory.CreateMqttClient();

            // Set up the message received handler
            _mqttClient.ApplicationMessageReceivedAsync += HandleReceivedApplicationMessage;

        }

        private async Task HandleReceivedApplicationMessage(MqttApplicationMessageReceivedEventArgs e)
        {
            string topic = e.ApplicationMessage.Topic;
            byte[] payload = e.ApplicationMessage.Payload;

            OnMqttMessageReceived(this, new MqttMessageEventArgs(topic, payload));
        }



        public async Task ConnectAsync()
        {
            if (_mqttClient.IsConnected)
            {
                Console.WriteLine("Client already connected");
                return;
            }
            var options = new MqttClientOptionsBuilder()
                .WithClientId(_mqttClientModel.ClientId)
                .WithTcpServer(_mqttClientModel.BrokerAddress, _mqttClientModel.BrokerPort)
                .WithCleanSession(_mqttClientModel.CleanSession)
                .Build();

            // Connect to the broker
            await _mqttClient.ConnectAsync(options);
            await SubscribeToConfiguredTopicsAsync();

        }

        public async Task DisconnectAsync()
        {
            await _mqttClient.DisconnectAsync();
        }

        public async Task SubscribeAsync(string topic)
        {
            if (_mqttClient == null || !_mqttClient.IsConnected)
            {
                throw new InvalidOperationException("MQTT client is not connected.");
            }

            // Subscribe to the topic
            await _mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(topic).Build());
        }

        public async Task SubscribeToConfiguredTopicsAsync()
        {
            if (_mqttClient == null || !_mqttClient.IsConnected)
            {
                throw new InvalidOperationException("MQTT client is not connected.");
            }

            foreach (var topic in _mqttClientModel.SubscriptionTopics.Topics)
            {
                await SubscribeAsync(topic);
                Console.WriteLine(topic);

            }
        }

        public async Task<MqttClientPublishResult> PublishAsync(string topic, string payload)
        {
            if (_mqttClient == null || !_mqttClient.IsConnected)
            {
                throw new InvalidOperationException("MQTT client is not connected.");
            }

            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(Encoding.UTF8.GetBytes(payload))
                .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                .WithRetainFlag(false)
                .Build();

            return await _mqttClient.PublishAsync(message);
        }

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
