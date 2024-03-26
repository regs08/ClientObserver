using System;
using ClientObserver.Models.Server.Instance;
using ClientObserver.Services.App;
using ClientObserver.Models.Interfaces.ViewModel;
using ClientObserver.Models.Interfaces;
using ClientObserver.Models.Interfaces.Navigation;
using ClientObserver.Models.Server.Core.Clients;
using System.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using ClientObserver.Views.Display.Server.Core;

namespace ClientObserver.ViewModels.DeviceDisplay
{
	public class DeviceDisplayViewModel :IViewModel, IInitializable, INotifyPropertyChanged, IQueryAttributable
    {
        private readonly INavigationService navigationService;
        private readonly AppServerManager appServerManager;
        public event PropertyChangedEventHandler PropertyChanged;
        private ServerInstance serverInstance
                {
            get => serverInstance;
            set
            {
                if (serverInstance != value)
                {
                    serverInstance = value;
                    // If implementing INotifyPropertyChanged, notify that the property has changed here
                    OnPropertyChanged(nameof(ServerInstance));
                }
            }
        }
        public ServerInstance ServerInstance;
        public ICommand ConnectToDeviceCommand { get; set; }
        public ICommand NavigateToDeviceStreamCommand { get; set; }
        private bool mqttConnectionStatus;
        public bool MqttConnectionStatus
        {
            get => mqttConnectionStatus;
            set
            {
                if (mqttConnectionStatus != value)
                {
                    mqttConnectionStatus = value;
                    OnPropertyChanged(nameof(MqttConnectionStatus));
                }
            }
        }
        private string serverName;
        public string ServerName
        {
            get => serverName;
            set
            {
                if (serverName != value)
                {
                    serverName = value;
                    // If implementing INotifyPropertyChanged, notify that the property has changed here
                    OnPropertyChanged(nameof(ServerName));
                }
            }
        }
        public string Name { get; set; }
        public DeviceDisplayViewModel(AppServerManager appServerManager, INavigationService navigationService)
		{
            this.appServerManager = appServerManager;
            this.navigationService = navigationService;
            Name = "ServerDisplayViewModel";

            if (GetClientModel() is MqttClientModel clientModel)
            {
                MqttConnectionStatus = clientModel.IsConnected.Value;

            }
            else
            {
                MqttConnectionStatus = false;
            }
        }
        public void InitializeCommands()
        {
            ConnectToDeviceCommand = new AsyncRelayCommand(ExecuteConnectToDeviceCommand);
            NavigateToDeviceStreamCommand = new AsyncRelayCommand(ExecuteNavigateToDataStreamCommand);
            OnPropertyChanged(nameof(NavigateToDeviceStreamCommand));
            OnPropertyChanged(nameof(ConnectToDeviceCommand));

        }

        public void Initialize()
        {

        }

        public void RegisterMessages()
        {

        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("ServerName", out var value) && value is string serverName)
            {
                ServerName = serverName;
                LoadServerConfig(serverName); // Directly load configurations based on the server name
                if (GetClientModel() is MqttClientModel clientModel)
                {
                    MqttConnectionStatus = clientModel.IsConnected.Value;

                }
                else
                {
                    MqttConnectionStatus = false;
                }
            }
        }

        private void LoadServerConfig(string serverName)
        {
            if (appServerManager.ServerExists(serverName))
            {
                ServerInstance = appServerManager.GetServer(serverName);
                ServerName = ServerInstance.Name;
            }
            else
            {
                var serverConfigs = appServerManager.ConfigRepo.GetConfigFromAvaialbleConfigsWithName(serverName);
                ServerInstance = appServerManager.CreateServerFromConfig(serverConfigs);
                ServerName = ServerInstance.Name;
            }
            InitializeCommands();
        }

        private async Task ExecuteConnectToDeviceCommand()
        {
            // Your command logic here
            if (ServerInstance?.ServerClients?.GetClientModel<MqttClientModel>() is MqttClientModel mqttClientModel)
            {
               MqttConnectionStatus =  await mqttClientModel.Connect();
            }
            else
            {
                Console.WriteLine(ServerInstance.Name);
                Console.WriteLine("Unable to connect");
            }
        }
        private async Task ExecuteNavigateToDataStreamCommand()
        {
            if (ServerInstance?.ServerClients?.GetClientModel<VideoStreamClient>() is VideoStreamClient videoStreamClient)
            {
                //todo our navigation system isn't working properly. not sure to keep the hierarchy here or not... 
                var navigationPath = $"//MainPage/DeviceDisplayView/DataStreamView?ServerName={Uri.EscapeDataString(ServerName)}";
                await Shell.Current.GoToAsync(navigationPath);
            }
            else
            {
                Console.WriteLine($"{ServerInstance?.Name} - Unable to navigate to DataStreamView due to missing VideoStreamClient.");
            }
        }
        private MqttClientModel GetClientModel() 
        {
            if (ServerInstance?.ServerClients?.GetClientModel<MqttClientModel>() is MqttClientModel mqttClientModel)
            {
                return mqttClientModel;
            }
            return null;
        }



    }
}

