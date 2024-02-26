using System;
using System.Threading.Tasks;
using ClientObserver.Models.Server.Framework.Configs;
using ClientObserver.Models.App.Core.Configs;
using ClientObserver.Services.App.Repos.Configs;

namespace ClientObserver.Services.App.Core.Configs
{
    /// <summary>
    /// Service responsible for loading and managing application configurations, ensuring the application is initialized with the necessary settings.
    /// </summary>
    public class AppConfigService
    {
        private ConfigLoader ConfigLoader;
        private bool _isInitialized = false;

        /// <summary>
        /// Gets the repository for managing configuration data throughout the application lifecycle.
        /// </summary>
        public ConfigurationRepository ConfigRepo { get; private set; }

        /// <summary>
        /// Initializes a new instance of the AppConfigService class, setting up the configuration loader and repository.
        /// </summary>
        public AppConfigService()
        {
            ConfigLoader = new ConfigLoader();
            ConfigRepo = new ConfigurationRepository();
        }

        /// <summary>
        /// Asynchronously initializes the AppConfigService, loading local configurations if not already initialized.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task InitializeAsync()
        {
            if (!_isInitialized)
            {
                await LoadLocalConfigs();
                _isInitialized = true;
            }
        }

        /// <summary>
        /// Loads configurations from local files asynchronously and adds them to the available configurations in the repository.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation of loading configurations.</returns>
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
