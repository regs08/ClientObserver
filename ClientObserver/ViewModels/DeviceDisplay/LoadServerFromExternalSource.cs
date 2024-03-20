using System;

using ClientObserver.Models.Interfaces.ViewModel;
using ClientObserver.Models.Interfaces.Navigation;
using ClientObserver.Models.Interfaces.Messaging;

namespace ClientObserver.ViewModels.ServerDisplay
{
    public class LoadServerFromExternalSourceViewModel : IViewModel
    {
        public string Name { get; set; }


        public LoadServerFromExternalSourceViewModel()
        {
            Name = "LoadServerFromExternalSourceViewModel";
        }
        public void InitializeCommands()
        {

        }
        public void Initialize(INavigationService navigationService, IMessagingService messagingService)
        {

        }
        public void RegisterMessages()
        {

        }
    }
}

