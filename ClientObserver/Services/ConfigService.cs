using System.Collections.ObjectModel;
using System.Text.Json;
using ClientObserver.Services;
// Config service responsible for loading available and providing a list of selcted configs 
namespace ClientObserver
{
    public class ConfigService
    {
        public ObservableCollection<ServerConfig> AvailableConfigs { get; private set; }
        public ObservableCollection<ServerConfig> SelectedConfigs { get; private set; }
        public AggregateConfigService AggregatedData;
        public ConfigService()
        {
            AvailableConfigs = new ObservableCollection<ServerConfig>();
            SelectedConfigs = new ObservableCollection<ServerConfig>();
            LoadConfigsFromSource();
         
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

            foreach (var fileName in fileNames)
            {
                try
                {
                    var stream = await FileSystem.OpenAppPackageFileAsync(fileName);
                    using (var reader = new StreamReader(stream))
                    {
                        var jsonContent = await reader.ReadToEndAsync();
                        ServerConfig serverConfig = JsonSerializer.Deserialize<ServerConfig>(jsonContent);
                        if (serverConfig != null)
                        {
                            AddToAvailableConfigs(serverConfig);
                        }
                    }
                }
                catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
            }

        }
    
        public void AddToAvailableConfigs(ServerConfig config)
        {
            // Logic to add config
            if (config != null)
            {
                AvailableConfigs.Add(config);
            }
        }
        public void AddToSelectedConfigs(ServerConfig config)
        {
            // Logic to add config
            if (config != null)
            {
                SelectedConfigs.Add(config);
            }
        }
        public void RemoveConfig(ServerConfig config)
        {
            // Logic to remove config
            if (config != null && AvailableConfigs.Contains(config))
            {
                AvailableConfigs.Remove(config);
            }
        }

    }
}
