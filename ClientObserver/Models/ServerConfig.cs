using System;
using Microsoft.Maui.Controls;
using ClientObserver.Models.TopicList;
// todo use Uri obecjt for videoIp address 
namespace ClientObserver
{
    public class ServerConfig
    {
        public string ServerName { get; set; }
        public string IP { get; set; }
        public string StreamIP { get; set; }
        public string StreamPortNumber { get; set; }
        public string MqttPortNumber { get; set; }
        public List <string> SelectedLabels { get; set;}
        public List <string> AvailableLabels { get; set; }
        public SubTopicList SubscriptionTopics { get; set; }
        public PubTopicList PublishTopics { get; set; }
        public double ConfidenceThreshold { get; set;}

        public string VideoStreamUrl
        {
            get
            {
                return $"http://{StreamIP}:{StreamPortNumber}/video";
            }
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
                   $"IP Address: {IP}\n" +
                   $"Stream IP Address: {StreamIP}\n" +
                   $"MQTT Port Number: {MqttPortNumber}\n" +
                   $"Video Stream Port Number: {StreamPortNumber}\n" +
                   $"Selected Labels: {string.Join(", ", SelectedLabels)}\n" +
                   $"Available Labels: {string.Join(", ", AvailableLabels)}\n" +
                   $"Subscription Topics: {SubscriptionTopics.ToString()}\n" +
                   $"Pub Topics: {PublishTopics.ToString()}\n" +
                   $"Confidence Threshold: {ConfidenceThreshold}";
        }
        public bool IsValid()
        {
            // Validate string properties
            if (string.IsNullOrWhiteSpace(ServerName) ||
                string.IsNullOrWhiteSpace(IP) ||
                string.IsNullOrWhiteSpace(StreamIP) ||
                string.IsNullOrWhiteSpace(StreamPortNumber) ||
                string.IsNullOrWhiteSpace(MqttPortNumber))
            {
                return false;
            }

            // Validate port numbers as positive integers
            if (!IsPositiveInteger(StreamPortNumber) || !IsPositiveInteger(MqttPortNumber))
            {
                return false;
            }

            // Validate lists
            if (SelectedLabels == null || !SelectedLabels.Any() ||
                AvailableLabels == null || !AvailableLabels.Any())
            {
                return false;
            }

            // Validate topic lists
            if (SubscriptionTopics == null || SubscriptionTopics.Topics == null || !SubscriptionTopics.Topics.Any() ||
                PublishTopics == null || PublishTopics.Topics == null || !PublishTopics.Topics.Any())
            {
                return false;
            }

            // Validate numeric values
            if (ConfidenceThreshold < 0)
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



