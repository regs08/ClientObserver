using System.Collections.ObjectModel;
using System.Windows.Input;
using ClientObserver.Views;
using ClientObserver.Services;
using CommunityToolkit.Mvvm.Messaging;
using ClientObserver.Models.Server.Core.Clients;
using ClientObserver.Models.Server.Core.Configs;
using ClientObserver.Models.Server.Framework.Configs;
using ClientObserver.Services.App.Repos.Configs;
using ClientObserver.Models.App.Messages;
using ClientObserver.Services.App;
using ClientObserver.ViewModels.ServerConfigConnectionSetup;
using ClientObserver.Services.Server.Core.Clients;

namespace ClientObserver.ViewModels
{
    public class MainPageViewModel
    {
        // Collection to store the selected server configurations

        // Service for configuration management
        //private AppConfigManager appConfigManager;
        //private AppClientManager appClientManager;
        private AppServerManager appServerManager;
        public ConfigurationRepository ConfigRepo => AppServerManager.Instance.ConfigRepo;
        // Command to select configuration view
        public ICommand SelectFromAvailableConfigsCommand { get; private set; }
        // Command to navigate to the setup ServerConfigConnection
        public ICommand SetupServerConfigConenction { get; private set; }
        //Command to navigate to the ServerPageView
        public ICommand ServerPageViewCommand { get; private set; }

        // Constructor
        public MainPageViewModel()
        {
            // Initialize command to navigate to the configuration view
            SelectFromAvailableConfigsCommand = new Command(async () => await NavigateToConfigSelection());
            SetupServerConfigConenction = new Command(async () => await NavigateToSetUpServerConfigConnection());
            ServerPageViewCommand = new Command<ServerConfigs>(NavigateToServerPage);

            // Instantiate the config service. Local configs are loaded on intialization
            appServerManager = AppServerManager.Instance;
            // Register to listen for update messages for server configurations
            WeakReferenceMessenger.Default.Register<UpdateSelectedServerConfigMessage>(this, (recipient, message) =>
            {
                // Update the selected configurations when a message is received
                ServerConfigs config = message.NewConfig;

                ConfigRepo.AddToSelectedConfigs(config);
                appServerManager.CreateServerFromConfig(config);


            });
        }

        public async Task InitializeAppConfigManagerAsync()
        {
            await appServerManager.appConfigService.InitializeAsync();
            // Now AppConfigManager is initialized, and local configs are loaded once.
        }


        private async void NavigateToServerPage(ServerConfigs serverConfigs)
        {
            ServiceManager serviceManager = new ServiceManager(serverConfigs);
            var viewModel = new ServerPageViewModel(serviceManager);
            var serverPage = new ServerPageView(viewModel);
            await Shell.Current.Navigation.PushAsync(serverPage);
        }
        // Navigates to the server configuration view
        private async Task NavigateToConfigSelection()
        {
            // Create and navigate to the server configuration page
           SelectConfigViewModel viewModel = new SelectConfigViewModel();
            var configPage = new SelectConfigView (viewModel);
            await Shell.Current.Navigation.PushAsync(configPage);
        }
        private async Task NavigateToSetUpServerConfigConnection()
        {
            ConnectionSetupViewModel viewModel = new ConnectionSetupViewModel();
            var connectionSetUpPage = new ConnectionSetupView(viewModel);
            await Shell.Current.Navigation.PushAsync(connectionSetUpPage);
        }
    }
}
