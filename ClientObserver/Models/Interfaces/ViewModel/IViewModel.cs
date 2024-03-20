using System;
using ClientObserver.Models.Interfaces.Messaging;
using ClientObserver.Models.Interfaces.Navigation;
using ClientObserver.Services.App;
namespace ClientObserver.Models.Interfaces.ViewModel
{
    //todo will probably need to make an interface for the service manager. 
   public interface IViewModel
    {
    void InitializeCommands();
    void RegisterMessages();
    string Name { get; set; }
        // void Initialize(INavigationService navigationService, IMessagingService messagingService);
    }


}

