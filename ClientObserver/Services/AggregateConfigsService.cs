// combines all available configs to provide default values for creating configs
// displays the unique values of all of them and provides the user with those as default choices 

using System.Collections.ObjectModel;
using ClientObserver.Models.TopicList;
namespace ClientObserver.Services
{
    public class AggregateConfigService
    {
        private ObservableCollection<ServerConfig> _severConfigs;
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

        public AggregateConfigService(ObservableCollection<ServerConfig> serverConfigs)
        {
            _severConfigs = serverConfigs;
            AggregateData();
        }
        // Aggregates data from the available server configs 
        private void AggregateData()
        {
            AvailableIPs = _severConfigs?.Select(c => c.IP).Distinct().ToList() ?? new List<string>();
            AvailableStreamIPs = _severConfigs?.Select(c => c.StreamIP).Distinct().ToList() ?? new List<string>();
            AvailableServerNames = _severConfigs?.Select(c => c.ServerName).Distinct().ToList() ?? new List<string>();
            AvailableStreamPortNumbers = _severConfigs?.Select(c => c.StreamPortNumber).Distinct().ToList() ?? new List<string>();
            AvailableMqttPortNumbers = _severConfigs?.Select(c => c.MqttPortNumber).Distinct().ToList() ?? new List<string>();
            AvailableLabels = _severConfigs?.SelectMany(c => c.AvailableLabels ?? new List<string>()).Distinct().ToList() ?? new List<string>();
            AvailableConfidenceThresholds = _severConfigs?.Select(c => c.ConfidenceThreshold).Distinct().ToList() ?? new List<double>();

            AvailableSubTopics = new SubTopicList();
            AvailablePubTopics = new PubTopicList();

            var allUniqueSubTopics = _severConfigs?
                .Where(config => config.SubscriptionTopics != null)
                .SelectMany(config => config.SubscriptionTopics.Topics)
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
                .Where(config => config.PublishTopics != null)
                .SelectMany(config => config.PublishTopics.Topics)
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
