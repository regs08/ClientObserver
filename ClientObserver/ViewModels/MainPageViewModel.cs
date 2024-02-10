using System.Collections.ObjectModel;
using System.Windows.Input;
using ClientObserver.Views;
using ClientObserver.Services;
using CommunityToolkit.Mvvm.Messaging;
using ClientObserver.Models.Clients;
using ClientObserver.Managers;
using ClientObserver.Models.Servers;
using ClientObserver.ViewModels.ServerConfigConnectionSetup;

namespace ClientObserver.ViewModels
{
    public class MainPageViewModel
    {
        // Collection to store the selected server configurations

        // Service for configuration management
        private AppConfigManager appConfigManager;
        private AppClientManager appClientManager;


        public ObservableCollection<ServerConfigs> SelectedConfigs => AppConfigManager.Instance.SelectedConfigs;
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
            appConfigManager = AppConfigManager.Instance;
            appClientManager = AppClientManager.Instance;

            // Register to listen for update messages for server configurations
            WeakReferenceMessenger.Default.Register<UpdateSelectedServerConfigMessage>(this, (recipient, message) =>
            {
                // Update the selected configurations when a message is received
                UpdateSelectedConfigs(message.NewConfig);
            });
        }

        public async Task InitializeAppConfigManagerAsync()
        {
            await AppConfigManager.Instance.InitializeAsync();
            // Now AppConfigManager is initialized, and local configs are loaded once.
        }
        // todo change method sig...doing alot more than updating selected configs
        // Updates the selected server configurations
        private void UpdateSelectedConfigs(ServerConfigs config)
        {
            // Checks if the configuration already exists and updates or adds accordingly
            var existingConfig = appConfigManager.SelectedConfigs.FirstOrDefault(c => c.ServerName == config.ServerName);
            if (existingConfig != null)
            {
                // Remove the existing configuration if found
                appConfigManager.RemoveFromSelectedConfigs(existingConfig);
            }
            // Add the new or updated configuration
            appConfigManager.AddToSelectedConfigs(config);

            // Creating the models for our client models
            // todo encapsualte this
            // create model from config 
            MqttClientModel mqttClientModel = new MqttClientModel(config.MqttClientConfig);
            //mqttClientModel.ApplyConfig();
            // create serverclients object (holds all clients for server 
            ServerClients serverClients = new ServerClients();
            serverClients.SetServerName(config.ServerName);
            serverClients.AddClientModel(mqttClientModel);
            // add to our singleton instance 
            appClientManager.AddSeverClients(serverClients);

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
            SelectConfigViewModel viewModel = new SelectConfigViewModel(appConfigManager);
            var configPage = new ConfigSelectionView(viewModel);
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
