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
        private List<string> _localConfigPaths;

        // Class for loading configs 
        private ConfigLoader _configLoader;
        // list of available configs 
        public ObservableCollection<ServerConfigs> AvailableConfigs { get; private set; }
        // list of selected configs, dynamically updated. These are the configs we build our connections to
        public ObservableCollection<ServerConfigs> SelectedConfigs { get; private set; }

        public AppConfigManager()
        {
            _localConfigPaths = new List<string> { "DefaultConfig.json", "GrapeModelConfig.json" };

            AvailableConfigs = new ObservableCollection<ServerConfigs>();
            SelectedConfigs = new ObservableCollection<ServerConfigs>();
        }
        public void LoadLocalConfigs()
        {
            foreach (var jsonPath in _localConfigPaths)
            {
                ServerConfigs serverConfigs = _configLoader.LoadConfigsFromJson(jsonPath);
                AvailableConfigs.Add(serverConfigs);
            }
        }
	}
}

