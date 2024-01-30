// config for connencting to our 'server' 
// todo break this up into mqtt config, video stream config, and detection config 
using System;
using Microsoft.Maui.Controls;
using ClientObserver.Models.TopicList;
using ClientObserver.Models.Configs;

namespace ClientObserver
{
    public class ServerConfig
    {
        public string ServerName { get; set; }

        public MqttClientConfig MqttClientConfig { get; set; }
        public VideoStreamConfig VideoStreamConfig { get; set; }
        public ModelParamConfig ModelParamConfig { get; set; }


        public ServerConfig(string serverName, MqttClientConfig mqttClientConfig, VideoStreamConfig videoStreamConfig, ModelParamConfig modelParamConfig)
        {
            ServerName = serverName;
            MqttClientConfig = mqttClientConfig;
            VideoStreamConfig = videoStreamConfig;
            ModelParamConfig = modelParamConfig;
        }
        public string VerifyConfigurations()
        {
            if (MqttClientConfig == null)
            {
                return "MqttClientConfig is not set.";
            }
            else
            {
                string mqttConfigNullProperty = MqttClientConfig.NullProperties();
                if (!string.IsNullOrEmpty(mqttConfigNullProperty))
                {
                    return $"MqttClientConfig: {mqttConfigNullProperty} is not set.";
                }
            }

            if (VideoStreamConfig == null)
            {
                return "VideoStreamConfig is not set.";
            }
            else
            {
                string videoConfigNullProperty = VideoStreamConfig.NullProperties();
                if (!string.IsNullOrEmpty(videoConfigNullProperty))
                {
                    return $"VideoStreamConfig: {videoConfigNullProperty} is not set.";
                }
            }

            if (ModelParamConfig == null)
            {
                return "ModelParamConfig is not set.";
            }
            else
            {
                string modelConfigNullProperty = ModelParamConfig.NullProperties();
                if (!string.IsNullOrEmpty(modelConfigNullProperty))
                {
                    return $"ModelParamConfig: {modelConfigNullProperty} is not set.";
                }
            }

            // If all configurations are complete
            return null;
        }
    
    public string FormattedDisplay
        {
            get
            {
                return FormatForDisplay();
            }
        }
        public string FormatForDisplay()
        {
            return $"Server Name: {ServerName}\n" +
                   $"BrokerAddress Address: {MqttClientConfig.BrokerAddress}\n" +
                   $"MQTT Port Number: {MqttClientConfig.PortNumber}\n" +
                   $"Subscription Topics: {MqttClientConfig.SubscriptionTopics}\n" +
                   $"Pub Topics: {MqttClientConfig.PublishTopics}\n" +
                   $"Stream IP Address: {VideoStreamConfig.StreamIP}\n" +
                   $"Video Stream Port Number: {VideoStreamConfig.StreamPortNumber}\n" +
                   $"Selected Labels: {string.Join(", ", ModelParamConfig.SelectedLabels)}\n" +
                   $"Available Labels: {string.Join(", ", ModelParamConfig.AvailableLabels)}\n" +
                   $"Confidence Threshold: {ModelParamConfig.ConfidenceThreshold}";
        }
        public bool IsValid()
        {
            // Validate string properties
            if (string.IsNullOrWhiteSpace(ServerName) ||
                string.IsNullOrWhiteSpace(MqttClientConfig.BrokerAddress) ||
                string.IsNullOrWhiteSpace(VideoStreamConfig.StreamIP) ||
                string.IsNullOrWhiteSpace(VideoStreamConfig.StreamPortNumber) ||
                string.IsNullOrWhiteSpace(MqttClientConfig.PortNumber))
            {
                return false;
            }

            // Validate port numbers as positive integers
            if (!IsPositiveInteger(VideoStreamConfig.StreamPortNumber) || !IsPositiveInteger(MqttClientConfig.PortNumber))
            {
                return false;
            }

            // Validate lists
            if (ModelParamConfig.SelectedLabels == null || !ModelParamConfig.SelectedLabels.Any() ||
                ModelParamConfig.AvailableLabels == null || !ModelParamConfig.AvailableLabels.Any())
            {
                return false;
            }

            // Validate topic lists
            if (MqttClientConfig.SubscriptionTopics == null || MqttClientConfig.SubscriptionTopics.Topics == null || !MqttClientConfig.SubscriptionTopics.Topics.Any() ||
                MqttClientConfig.PublishTopics == null || MqttClientConfig.PublishTopics.Topics == null || !MqttClientConfig.PublishTopics.Topics.Any())
            {
                return false;
            }

            // Validate numeric values
            if (ModelParamConfig.ConfidenceThreshold < 0)
            {
                return false;
            }

            return true;
        }

        private bool IsPositiveInteger(string value)
        {
            return int.TryParse(value, out int result) && result > 0;
        }
    }
}



