﻿using System.Collections.ObjectModel;
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
                UpdateSelectedConfigs(message.NewConfig);
            });
        }

        public async Task InitializeAppConfigManagerAsync()
        {
            await appServerManager.appConfigService.InitializeAsync();
            // Now AppConfigManager is initialized, and local configs are loaded once.
        }
        // todo change method sig...doing alot more than updating selected configs
        // Updates the selected server configurations
        private void UpdateSelectedConfigs(ServerConfigs config)
        {
            
            // Add the new or updated configuration
            'wed like to have other methods that our subscrived to AddToSelected Configs or Update Selected Configs
                'to devour this config and make our clients _> serverClients and configs -> serverConfgis -> serverinsatnce
                ' first we will need to seperate the logic from the loader and then use those configs to first make a serverconfigs,
                ' thgen iterate through serverconfigs and with a newly created dictionary mapping we can map the configs to the clients
                ' will need to look into using generaics maybe? '
            ConfigRepo.AddToSelectedConfigs(config);
            // todo encapsualte this would be nice to do this all from a config 
            string serverName = config.Name;
            appServerManager.CreateAndAddServer(serverName);
            MqttClientModel mqttClientModel = new MqttClientModel(config.GetConfigModel<MqttClientConfig>());
            appServerManager.AddClientToServer(serverName: serverName, mqttClientModel);
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
