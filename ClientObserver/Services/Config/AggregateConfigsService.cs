// combines all available configs to provide default values for creating configs
// displays the unique values of all of them and provides the user with those as default choices 
// todo needs a further rework based on the new config rerwork 
using System.Collections.ObjectModel;
using ClientObserver.Models.TopicList;
using ClientObserver.Managers;
using ClientObserver.Configs;

namespace ClientObserver.Services
{
    public class AggregateConfigService
    {
        private ObservableCollection<ServerConfigs> _severConfigs;
        // ###
        // Video Stream
        public List<string> AvailableStreamIPs { get; private set; }
        public List<string> AvailableServerNames { get; private set; }
        public List<string> AvailableStreamPortNumbers { get; private set; }
        // ####

        // mqtt client 
        public SubTopicList AvailableSubTopics { get; private set; }
        public PubTopicList AvailablePubTopics { get; private set; }
        public List<string> AvailableMqttPortNumbers { get; private set; }
        // broker address 
        public List<string> AvailableIPs { get; private set; }
        // ###

        // Model Params 
        public List<string> AvailableLabels { get; private set; }
        public List<double> AvailableConfidenceThresholds { get; private set; }
        // ###

        public AggregateConfigService(ObservableCollection<ServerConfigs> serverConfigs)
        {
            _severConfigs = serverConfigs;
            AggregateData();
        }
        // Aggregates data from the available server configs 
        private void AggregateData()
        {
            AvailableIPs = _severConfigs?.Select(c => c.MqttClientConfig.BrokerAddress).Distinct().ToList() ?? new List<string>();
            AvailableMqttPortNumbers = _severConfigs?.Select(c => c.MqttClientConfig.PortNumber).Distinct().ToList() ?? new List<string>();
            AvailableStreamIPs = _severConfigs?.Select(c => c.VideoStreamConfig.StreamIP).Distinct().ToList() ?? new List<string>();
            AvailableServerNames = _severConfigs?.Select(c => c.ServerName).Distinct().ToList() ?? new List<string>();
            AvailableStreamPortNumbers = _severConfigs?.Select(c => c.VideoStreamConfig.StreamPortNumber).Distinct().ToList() ?? new List<string>();
            AvailableLabels = _severConfigs?.SelectMany(c => c.ModelParamConfig.AvailableLabels ?? new List<string>()).Distinct().ToList() ?? new List<string>();
            AvailableConfidenceThresholds = _severConfigs?.Select(c => c.ModelParamConfig.ConfidenceThreshold).Distinct().ToList() ?? new List<double>();

            AvailableSubTopics = new SubTopicList();
            AvailablePubTopics = new PubTopicList();

            var allUniqueSubTopics = _severConfigs?
                .Where(config => config.MqttClientConfig.SubscriptionTopics != null)
                .SelectMany(config => config.MqttClientConfig.SubscriptionTopics.Topics)
                .Distinct()
                .ToList();


            if (allUniqueSubTopics != null)
            {
                AvailableSubTopics.Topics = new ObservableCollection<string>(allUniqueSubTopics);
            }
            else
            {
                AvailableSubTopics.Topics = new ObservableCollection<string>();
            }


            var allUniquePubTopics = _severConfigs?
                .Where(config => config.MqttClientConfig.PublishTopics != null)
                .SelectMany(config => config.MqttClientConfig.PublishTopics.Topics)
                .Distinct()
                .ToList();

            if (allUniquePubTopics != null)
            {
                AvailablePubTopics.Topics = new ObservableCollection<string>(allUniquePubTopics);
            }
            else
            {

                AvailablePubTopics.Topics = new ObservableCollection<string>();
            }
        }


    }
}
