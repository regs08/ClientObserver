using System;
using System.Windows.Input;
using ClientObserver.Services.App;
using CommunityToolkit.Mvvm.Input;
using ClientObserver.Models.Interfaces.Messaging;
using ClientObserver.Models.Server.Framework.Configs;
using ClientObserver.Models.App.Messages;
using ClientObserver.Services.App.Repos.Configs;
using ClientObserver.Models.Interfaces.Navigation;
using ClientObserver.Models.Interfaces.ViewModel;
using ClientObserver.ViewModels.ServerDisplay;
using ClientObserver.ViewModels.DeviceDisplay;


namespace ClientObserver.ViewModels
{
    public class MainPageViewModel : IViewModel
    {
        private readonly INavigationService navigationService;
        private readonly IMessagingService messagingService;
        private readonly AppServerManager appServerManager;
        public string Name { get; set; }

        public ConfigurationRepository ConfigRepo => appServerManager.ConfigRepo;

        // Commands are now of type AsyncCommand (from CommunityToolkit.Mvvm.Input)
        public ICommand LoadServersFromExternalSourceCommand { get; set; }
        public ICommand ServersDisplayViewCommand { get; set; }


        public MainPageViewModel(AppServerManager appServerManager,
            INavigationService navigationService,
            IMessagingService messagingService)
        {
            this.appServerManager = appServerManager;
            this.navigationService = navigationService;
            this.messagingService = messagingService ?? throw new ArgumentNullException(nameof(messagingService));

            InitializeCommands();
            RegisterMessages();

        }
        public void InitializeCommands()
        {
            ServersDisplayViewCommand = new AsyncRelayCommand<ServerConfigs>(async (serverConfigs) =>
            {
                // Assuming ServerConfigs has a ServerName property you wish to pass
                var serverName = serverConfigs.Name;

                // Pass serverName as a parameter
                await navigationService.NavigateAsync<DeviceDisplayViewModel>(new Dictionary<string, object>
        {
                { "ServerName", serverName }
            });
            });


            LoadServersFromExternalSourceCommand = new AsyncRelayCommand(async () =>
            {
                // No parameters required for navigating to LoadServerFromExternalSourceView
                await navigationService.NavigateAsync<LoadServerFromExternalSourceViewModel>();
            });
        }


        public void RegisterMessages()
        {
            messagingService.Register<MainPageViewModel, UpdateSelectedServerConfigMessage>(this, (recipient, message) =>
            {
                ServerConfigs config = message.NewConfig;
                appServerManager.ConfigRepo.AddToSelectedConfigs(config);
                appServerManager.CreateServerFromConfig(config);
            });
        }

        public void Dispose()
        {
            messagingService.UnregisterAll(this);
        }
        public async void InitializeAppConfigManagerAsync()
        {
            await appServerManager.AppConfigService.InitializeAsync();
        }

    }
}

