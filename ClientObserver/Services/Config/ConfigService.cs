using System.Collections.ObjectModel;
using System.Text.Json;
using ClientObserver.Services;
using ClientObserver.Managers;
using ClientObserver.Configs;

// Config service responsible for loading available and providing a list of selcted configs
// intialized on main page and is responsible for adding new configs,
// and tracking selected configs from the user 

namespace ClientObserver
{
    public class ConfigService
    {
        private List<string> _localConfigPaths;
        private ConfigLoader _configLoader;
        // list of available configs, dynamically updated. defautls are loadded locally from resources 
        public ObservableCollection<ServerConfigs> AvailableConfigs { get; private set; }
        // list of selected configs, dynamically updated 
        public ObservableCollection<ServerConfigs> SelectedConfigs { get; private set; }

        // Aggregates data from available conigs 
        public AggregateConfigService AggregatedData;
        public ConfigService()
        {
            ConfigLoader configLoader = _configLoader; 
            _localConfigPaths = new List<string> { "DefaultConfig.json", "GrapeModelConfig.json" };

            AvailableConfigs = new ObservableCollection<ServerConfigs>();
            SelectedConfigs = new ObservableCollection<ServerConfigs>();
            //_configLoader.LoadConfigsFromJson();
            //LoadConfigsFromSource();
         
        }
        public void IntializeAggregateConfigService()
        {
            if (AvailableConfigs != null)
            {
                try
                {
                    AggregatedData = new AggregateConfigService(AvailableConfigs);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to intialize AggregateConfigService: {ex.Message}");
                }
            }
        }

        private async void LoadConfigsFromSource()
        {
            // Assuming 'loadLocal' is a flag to determine the source of configs
            bool loadLocal = true; // You can make this configurable

            if (loadLocal)
            {
               await LoadLocalConfigs();
            }
            else
            {
                // Future implementation for other sources
            }

        }
        private async Task LoadLocalConfigs()
        {

            // Logic to load configs from local files
            // Example: Reading JSON files from a local directory
            // List of file names in the Resources/ Raw folder
            var fileNames = new List<string> { "DefaultConfig.json", "GrapeModelConfig.json" };

            foreach (var fileName in _localConfigPaths)
            {
                try
                {
                    Configs.ServerConfigs serverConfigs = _configLoader.LoadConfigsFromJson(fileName);

                }
                catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
            }

        }
    
        public void AddToAvailableConfigs(ServerConfigs config)
        {
            // Logic to add config
            if (config != null)
            {
                AvailableConfigs.Add(config);
            }
        }
        public void AddToSelectedConfigs(ServerConfigs config)
        {
            // Logic to add config
            if (config != null)
            {
                SelectedConfigs.Add(config);
            }
        }
        public void RemoveConfig(ServerConfigs config)
        {
            // Logic to remove config
            if (config != null && AvailableConfigs.Contains(config))
            {
                AvailableConfigs.Remove(config);
            }
        }

    }
}
