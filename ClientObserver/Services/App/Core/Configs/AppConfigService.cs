using System;
using System.Threading.Tasks;
using ClientObserver.Models.Server.Framework.Configs;
using ClientObserver.Models.App.Core.Configs;
using ClientObserver.Services.App.Repos.Configs;

namespace ClientObserver.Services.App.Core.Configs
{
    /// <summary>
    /// Used to  load and maintain Configs across the app 
    /// </summary>
    public class AppConfigService
    {
        
        private ConfigLoader ConfigLoader;
        private bool _isInitialized = false;

        // Utilize ConfigurationRepository for managing configs
        public ConfigurationRepository ConfigRepo { get; private set; }

        public AppConfigService()
        {
            ConfigLoader = new ConfigLoader();
            ConfigRepo = new ConfigurationRepository();
        }

        public async Task InitializeAsync()
        {
            if (!_isInitialized)
            {
                 await LoadLocalConfigs();
                _isInitialized = true;
            }
        }

        private async Task LoadLocalConfigs()
        {
            Console.WriteLine("Loading local configs");
            foreach (var jsonPath in ConfigLoader.LocalConfigPaths)
            {
                ServerConfigs serverConfigs = await ConfigLoader.LoadLocalConfigurations(jsonPath);
                ConfigRepo.AddToAvailableConfigs(serverConfigs);
            }
        }
    }
}
