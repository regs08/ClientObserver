using System.Collections.ObjectModel;
using System.Windows.Input;
using ClientObserver.Models.Server.Instance;
using ClientObserver.Models.Server.Framework.Configs;
using ClientObserver.Models.Server.Core.Clients;
using ClientObserver.Models.Server.Core.Configs;
using ClientObserver.Services.App;

using ClientObserver.Helpers.App;

using ClientObserver.Views.ServerConfigConnectionSetup;

namespace ClientObserver.ViewModels.ServerConfigConnectionSetup
{
    public class ConnectionSetupViewModel
    {


        public ObservableCollection<ServerInstance> appServers => AppServerManager.Instance.Servers;
        public ICommand NavigateCommand { get; private set; }

        public ConnectionSetupViewModel()
        {
            NavigateCommand = new Command<BaseClientModel>(ExecuteNavigateCommand);
        }

        private async void ExecuteNavigateCommand(BaseClientModel clientModel)
        {
            Console.WriteLine(clientModel.GetType());
            // Check if clientModel is of type MqttClientModel
            if (clientModel is MqttClientModel mqttClientModel)
            {
                // Since clientModel is MqttClientModel, we can directly use mqttClientModel which is already casted.
                MqttConnectionViewModel viewModel = new MqttConnectionViewModel(mqttClientModel);
                var page = new MqttConnectionView(viewModel);

                await Application.Current.MainPage.Navigation.PushAsync(page);
            }
        }
    }
}
