using System;
using System.Collections.ObjectModel;
using ClientObserver.Services;
using ClientObserver.Configs;
// todo update the view model to reflect the available configs
// Class responsible for managing all the config manager instances of the app
namespace ClientObserver.Managers
{
    public class AppConfigManager
    {
        private static AppConfigManager _instance; // The single instance

        private List<string> _localConfigPaths;

        // Class for loading configs
        private ConfigLoader _configLoader;

        private bool _isInitialized = false; // Flag to check if configs are loaded
        // List of available configs
        public ObservableCollection<ServerConfigs> AvailableConfigs { get; private set; }

        // List of selected configs, dynamically updated. These are the configs we build our connections to
        public ObservableCollection<ServerConfigs> SelectedConfigs { get; private set; }

        // Private constructor for singleton pattern
        private AppConfigManager()
        {
            _localConfigPaths = new List<string> { "DefaultConfig.json", "GrapeModelConfig.json" };
            _configLoader = new ConfigLoader();
            AvailableConfigs = new ObservableCollection<ServerConfigs>();
            SelectedConfigs = new ObservableCollection<ServerConfigs>();
        }

        // Public static method to access the singleton instance
        public static AppConfigManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppConfigManager();
                }
                return _instance;
            }
        }
        public async Task InitializeAsync()
        {
            if (!_isInitialized)
            {
                await LoadLocalConfigs();
                _isInitialized = true; // Set flag to true after loading configs for the first time
            }
        }
        public async Task LoadLocalConfigs()
        {
            Console.WriteLine("loading local configs"); 
            foreach (var jsonPath in _localConfigPaths)
            {
                ServerConfigs serverConfigs = await _configLoader.LoadConfigsFromJson(jsonPath);
                AddtoAvailableConfigs(serverConfigs);
            }
        }

        // method for add to selected 
        public void AddToSelectedConfigs(ServerConfigs serverConfigs)
        {

            if (serverConfigs.ValidateAllConfigs())
            {
                SelectedConfigs.Add(serverConfigs);
            }

        }
        public void AddtoAvailableConfigs(ServerConfigs serverConfigs)
        {
            if (!AvailableConfigs.Contains(serverConfigs))
            {

                if (serverConfigs.ValidateAllConfigs())
                {
                    AvailableConfigs.Add(serverConfigs);
                }
            }
        }

        public void RemoveFromSelectedConfigs(ServerConfigs serverConfigs)
        {
            if (SelectedConfigs.Contains(serverConfigs))
            {
                SelectedConfigs.Remove(serverConfigs);
            }
            else
            {
                Console.WriteLine("Configuration not found in selected configs.");
            }
        }

        public void RemoveFromAvailableConfigs(ServerConfigs serverConfigs)
        {
            if (AvailableConfigs.Contains(serverConfigs))
            {
                AvailableConfigs.Remove(serverConfigs);
            }
            else
            {
                Console.WriteLine("Configuration not found in available configs.");
            }
        }

    }
}

