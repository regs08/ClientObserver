using System;
using ClientObserver.ViewModels;
using ClientObserver.ViewModels.DeviceDisplay.Core;
using ClientObserver.Models.Interfaces.Messaging;
using ClientObserver.Services.Navigation;
using ClientObserver.Services.App;
using ClientObserver.ViewModels.DeviceDisplay;

namespace ClientObserver.Factories.ViewModel
{
    public class ViewModelFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AppServerManager appServerManager;
        
        public ViewModelFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            appServerManager = _serviceProvider.GetRequiredService<AppServerManager>();

        }

        public MainPageViewModel CreateMainPageViewModel()
        {
            // Use the serviceProvider to resolve the MainPageViewModel's dependencies
            var navigationService = _serviceProvider.GetRequiredService<NavigationServiceMain>();
            var messagingService = _serviceProvider.GetRequiredService<IMessagingService>();
            return new MainPageViewModel(navigationService: navigationService,
                messagingService: messagingService,
                appServerManager:appServerManager);
        }

        public DeviceDisplayViewModel DeviceDisplayViewModel()
        {
            var navigationService = _serviceProvider.GetRequiredService<NavigationServiceMain>();

            return new DeviceDisplayViewModel(appServerManager:appServerManager, navigationService: navigationService);
        }
        public DataStreamViewModel CreateDataStreamViewModel()
        {
            return new DataStreamViewModel(appServerManager: appServerManager);
        }
    }

}

