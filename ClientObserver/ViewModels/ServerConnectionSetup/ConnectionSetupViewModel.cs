using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ClientObserver.Models.Server.Instance;
using ClientObserver.Models.Server.Core.Clients;
using ClientObserver.Services.App;
using ClientObserver.Infrastructure.Maps;
using ClientObserver.ViewModels.ServerConnectionSetup.ClientConnectionViewModels;

namespace ClientObserver.ViewModels.ServerConnectionSetup
{
    /// <summary>
    /// ViewModel responsible for the setup and navigation to specific client connection configurations.
    /// It dynamically navigates to the appropriate configuration view based on the selected server instance.
    /// </summary>
    public class ConnectionSetupViewModel
    {
        public ObservableCollection<ServerInstance> AppServers => AppServerManager.Instance.Servers;

        /// <summary>
        /// Command to navigate to the detailed configuration view for a selected server instance.
        /// </summary>
        public ICommand NavigateCommand { get; private set; }

        public ConnectionSetupViewModel()
        {
            NavigateCommand = new Command<BaseClientModel>(ExecuteNavigateCommand);
        }

        /// <summary>
        /// Executes the navigation to the specific client configuration view based on the client model type.
        /// </summary>
        /// <param name="clientModel">The client model for which to navigate and set up configurations.</param>
        private async void ExecuteNavigateCommand(BaseClientModel clientModel)
        {
            var clientType = clientModel.GetType();

            // Attempt to get the corresponding ViewModel type for the client model.
            if (ClientMaps.clientToViewModelMap.TryGetValue(clientType, out var viewModelType))
            {
                // Create an instance of the ViewModel.
                var viewModel = Activator.CreateInstance(viewModelType, new object[] { clientModel });

                // Attempt to get the corresponding View type for the ViewModel.
                if (ViewModelMaps.viewModelTypeToViewTypeMap.TryGetValue(viewModelType, out var viewType))
                {
                    // Create an instance of the View, passing in the ViewModel.
                    var page = Activator.CreateInstance(viewType, new object[] { viewModel }) as Page;
                    if (page != null)
                    {
                        // Navigate to the page.
                        await Application.Current.MainPage.Navigation.PushAsync(page);
                    }
                }
            }
        }
    }
}
