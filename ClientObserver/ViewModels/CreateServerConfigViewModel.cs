using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ClientObserver.Models.Configs;
using ClientObserver.Models.TopicList;
using ClientObserver.Services;
using CommunityToolkit.Mvvm.Messaging;
// view model respnsible for creating a custom config.
// todo split and edit this to reflect changes in the server config 
namespace ClientObserver.ViewModels
{
    public class CreateServerConfigViewModel : INotifyPropertyChanged
    {
        #region Fields


        #endregion

        #region Properties

        public ObservableCollection<ServerConfig> AvailableConfigs { get; private set; }
        public ConfigService MyConfigService;
        public AggregateConfigService AvailableConfigData { get; private set; }
        public UserEntry MyUserEntry { get; private set; }

        // Properties used to reflect changes made by user in the view
        public PubTopicList MyAvailablePubTopics { get; private set; }
        public SubTopicList MyAvailableSubTopics { get; private set; }
        public ObservableCollection<string> MyAvailableLabels { get; private set; }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructor

        public CreateServerConfigViewModel(ConfigService configService)
        {
            InitializeViewModel(configService);
        }

        #endregion
        #region Private Methods

        private void InitializeViewModel(ConfigService configService)

        {
            MyConfigService = configService;
            // aggregates data from the configs 
            MyConfigService.IntializeAggregateConfigService();
            AvailableConfigData = MyConfigService.AggregatedData;

            AvailableConfigs = MyConfigService.AvailableConfigs ?? throw new ArgumentNullException(nameof(MyConfigService.AvailableConfigs));

            MyUserEntry = new UserEntry();

            MyAvailableLabels = new ObservableCollection<string>(AvailableConfigData.AvailableLabels);
            MyAvailablePubTopics = AvailableConfigData.AvailablePubTopics;
            MyAvailableSubTopics = AvailableConfigData.AvailableSubTopics;

            CreateConfigCommand = new Command(ExecuteCreateConfig);

        }
        private void ExecuteCreateConfig()
        {
            ServerConfig config = MyUserEntry.CreateServerConfig();
            if (config.IsValid())
            {
                Console.Write(config.FormattedDisplay);
                WeakReferenceMessenger.Default.Send(new UpdateServerConfigMessage(config));
            }
            // Here you can add logic to handle the newly created config, like adding it to a list, etc.
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
        #region Commands
        public ICommand CreateConfigCommand { get; private set; }

        #endregion
        public void AddEntryToSelectedList(string entry, string entryType)
        {
            if (!string.IsNullOrEmpty(entry))
            {
                if (entryType == "SubTopic")
                {
                    // Has checks for null and if contains topic
                    MyAvailableSubTopics.AddTopic(entry);
                    // explicitly update UI 
                    OnPropertyChanged(nameof(MyAvailableSubTopics));
                }
                else if (entryType == "PubTopic")
                {
                    MyAvailablePubTopics.AddTopic(entry);
                    // explicitly update UI 
                    OnPropertyChanged(nameof(MyAvailablePubTopics));
                }
                else if(entryType == "Label")
                {
                    MyAvailableLabels.Add(entry);
                    // explicitly update UI 
                    OnPropertyChanged(nameof(MyAvailableLabels));
                }
            }
        }


    }

public class UserEntry : INotifyPropertyChanged
    {
        private List<string> _selectedLabels = new List<string>();
        private List<string> _selectedSubTopics = new List<string>();
        private List<string> _selectedPubTopics = new List<string>();
        private string _selectedIP;
        private string _selectedStreamIP;
        private string _selectedServerName;
        private string _selectedStreamPortNumber;
        private string _selectedMqttPortNumber;
        private double _selectedConfThreshold;
        private string _newLabel;

        public event PropertyChangedEventHandler PropertyChanged;

        public string SelectedLabelsText
        {
            get => string.Join(", ", _selectedLabels);
        }

        public List<string> SelectedLabels
        {
            get => _selectedLabels;
            set
            {
                _selectedLabels = value;
                OnPropertyChanged(nameof(SelectedLabels));
                OnPropertyChanged(nameof(SelectedLabelsText));

            }
        }
        public string SelectedSubTopicsText
        {
            get => string.Join(", ", _selectedSubTopics);
        }
        public List<string> SelectedSubTopics
        {
            get => _selectedSubTopics;
            set
            {
                _selectedSubTopics = value;
                OnPropertyChanged(nameof(SelectedSubTopics));
                OnPropertyChanged(nameof(SelectedSubTopicsText));

            }
        }
        public string SelectedPubTopicsText
        {
            get => string.Join(", ", _selectedPubTopics);
        }
        public List<string> SelectedPubTopics
        {
            get => _selectedPubTopics;
            set
            {
                _selectedPubTopics = value;
                OnPropertyChanged(nameof(SelectedPubTopics));
                OnPropertyChanged(nameof(SelectedPubTopicsText));

            }
        }

        public string SelectedIP
        {
            get => _selectedIP;
            set
            {
                if (_selectedIP != value)
                {
                    _selectedIP = value;
                    OnPropertyChanged(nameof(SelectedIP));
                }
            }
        }

        public string SelectedStreamIP
        {
            get => _selectedStreamIP;
            set
            {
                if (_selectedStreamIP != value)
                {
                    _selectedStreamIP = value;
                    OnPropertyChanged(nameof(SelectedStreamIP));
                }
            }
        }

        public string SelectedServerName
        {
            get => _selectedServerName;
            set
            {
                if (_selectedServerName != value)
                {
                    _selectedServerName = value;
                    OnPropertyChanged(nameof(SelectedServerName));
                }
            }
        }

        public string SelectedStreamPortNumber
        {
            get => _selectedStreamPortNumber;
            set
            {
                if (_selectedStreamPortNumber != value)
                {
                    _selectedStreamPortNumber = value;
                    OnPropertyChanged(nameof(SelectedStreamPortNumber));
                }
            }
        }

        public string SelectedMqttPortNumber
        {
            get => _selectedMqttPortNumber;
            set
            {
                if (_selectedMqttPortNumber != value)
                {
                    _selectedMqttPortNumber = value;
                    OnPropertyChanged(nameof(SelectedMqttPortNumber));
                }
            }
        }

        public double SelectedConfidenceThreshold
        {
            get => _selectedConfThreshold;
            set
            {
                if (_selectedConfThreshold != value)
                {
                    _selectedConfThreshold = value;
                    OnPropertyChanged(nameof(SelectedConfidenceThreshold));
                }
            }
        }

        public string NewLabel
        {
            get => _newLabel;
            set
            {
                if (_newLabel != value)
                {
                    _newLabel = value;
                    OnPropertyChanged(nameof(NewLabel));
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ServerConfig CreateServerConfig()
        {
            // Create an instance of MqttClientConfig
            var mqttClientConfig = new MqttClientConfig
            {
                BrokerAddress = _selectedIP,
                PortNumber = _selectedMqttPortNumber,
                SubscriptionTopics = new SubTopicList { Topics = new ObservableCollection<string>(_selectedSubTopics) },
                PublishTopics = new PubTopicList { Topics = new ObservableCollection<string>(_selectedPubTopics) }
            };

            // Create an instance of VideoStreamConfig
            var videoStreamConfig = new VideoStreamConfig
            {
                StreamIP = _selectedStreamIP,
                StreamPortNumber = _selectedStreamPortNumber
            };

            // Create an instance of ModelParamConfig
            var modelParamConfig = new ModelParamConfig
            {
                SelectedLabels = new List<string>(_selectedLabels),
                AvailableLabels = new List<string>(_selectedLabels), // Assuming this is correct, though you might need a different source for AvailableLabels
                ConfidenceThreshold = _selectedConfThreshold
            };

            // Create and return the ServerConfig instance
            return new ServerConfig(_selectedServerName, mqttClientConfig, videoStreamConfig, modelParamConfig);

        }

    }

}
