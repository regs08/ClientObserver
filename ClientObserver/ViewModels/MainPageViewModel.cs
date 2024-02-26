using System.Windows.Input;
using ClientObserver.Views;
using ClientObserver.Services;
using CommunityToolkit.Mvvm.Messaging;

using ClientObserver.Models.Server.Framework.Configs;
using ClientObserver.Services.App.Repos.Configs;
using ClientObserver.Models.App.Messages;
using ClientObserver.Services.App;
using ClientObserver.ViewModels.ServerConnectionSetup;

namespace ClientObserver.ViewModels
{
    /// <summary>
    /// ViewModel for the main page, handling navigation and server configuration management.
    /// </summary>
    public class MainPageViewModel
    {
        private AppServerManager appServerManager;

        /// <summary>
        /// Gets the ConfigurationRepository from the AppServerManager to manage server configurations.
        /// </summary>
        public ConfigurationRepository ConfigRepo => AppServerManager.Instance.ConfigRepo;

        /// <summary>
        /// Command to navigate to the configuration selection view.
        /// </summary>
        public ICommand SelectFromAvailableConfigsCommand { get; private set; }

        /// <summary>
        /// Command to navigate to the setup for ServerConfigConnection.
        /// </summary>
        public ICommand SetupServerConfigConnection { get; private set; }

        /// <summary>
        /// Command to navigate to the ServerPageView.
        /// </summary>
        public ICommand ServerPageViewCommand { get; private set; }

        /// <summary>
        /// Constructs the MainPageViewModel and initializes navigation commands and configuration management.
        /// </summary>
        public MainPageViewModel()
        {
            InitializeCommands();
            appServerManager = AppServerManager.Instance;
            RegisterMessages();
        }

        /// <summary>
        /// Initializes the application's configuration manager and loads local configurations.
        /// </summary>
        public async Task InitializeAppConfigManagerAsync()
        {
            await appServerManager.AppConfigService.InitializeAsync();
        }

        private void InitializeCommands()
        {
            SelectFromAvailableConfigsCommand = new Command(async () => await NavigateToConfigSelection());
            SetupServerConfigConnection = new Command(async () => await NavigateToSetUpServerConfigConnection());
            ServerPageViewCommand = new Command<ServerConfigs>(NavigateToServerPage);
        }

        private void RegisterMessages()
        {
            WeakReferenceMessenger.Default.Register<UpdateSelectedServerConfigMessage>(this, (recipient, message) =>
            {
                ServerConfigs config = message.NewConfig;
                ConfigRepo.AddToSelectedConfigs(config);
                appServerManager.CreateServerFromConfig(config);
            });
        }

        private async Task NavigateToConfigSelection()
        {
            var viewModel = new SelectConfigViewModel();
            var configPage = new SelectConfigView(viewModel);
            await Shell.Current.Navigation.PushAsync(configPage);
        }

        private async Task NavigateToSetUpServerConfigConnection()
        {
            var viewModel = new ConnectionSetupViewModel();
            var connectionSetUpPage = new ConnectionSetupView(viewModel);
            await Shell.Current.Navigation.PushAsync(connectionSetUpPage);
        }

        private async void NavigateToServerPage(ServerConfigs serverConfigs)
        {
            var serviceManager = new ServiceManager(serverConfigs);
            var viewModel = new ServerPageViewModel(serviceManager);
            var serverPage = new ServerPageView(viewModel);
            await Shell.Current.Navigation.PushAsync(serverPage);
        }
    }
}
