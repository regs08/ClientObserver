using ClientObserver.Managers;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ClientObserver.Models.Servers;
using ClientObserver.Models.Server.Core.Clients;
using ClientObserver.Models.Server.Managers;
using ClientObserver.Views.ServerConfigConnectionSetup;

namespace ClientObserver.ViewModels.ServerConfigConnectionSetup
{
    public class ConnectionSetupViewModel
    {
        private readonly AppConfigManager appConfigManager;
        private readonly AppClientManager appClientManager;

        public ObservableCollection<ServerClients> appServerClients => AppClientManager.Instance.ServerClients;

        public ObservableCollection<ServerConfigs> MySelectedConfigs { get; set; }
        public ICommand NavigateCommand { get; private set; }

        public ConnectionSetupViewModel()
        {
            appConfigManager = AppConfigManager.Instance;
            appClientManager = AppClientManager.Instance;

            MySelectedConfigs = appConfigManager.SelectedConfigs;
            NavigateCommand = new Command<BaseClientModel>(ExecuteNavigateCommand);
        }

        private async void ExecuteNavigateCommand(BaseClientModel clientModel)
        {

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
