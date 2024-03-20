using System;
using ClientObserver.Models.Server.Instance;
using ClientObserver.Services.App;
using ClientObserver.Models.Interfaces.ViewModel;
using ClientObserver.Models.Interfaces;
using ClientObserver.Models.Server.Core.Clients;
using System.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace ClientObserver.ViewModels.DeviceDisplay
{
	public class DeviceDisplayViewModel :IViewModel, IInitializable, INotifyPropertyChanged, IQueryAttributable
    {
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
        public DeviceDisplayViewModel(AppServerManager appServerManager)
		{
            this.appServerManager = appServerManager;
            Name = "ServerDisplayViewModel";
        }
        public void InitializeCommands()
        {

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
            }
        }

        private void LoadServerConfig(string serverName)
        {
            var serverConfigs = appServerManager.ConfigRepo.GetConfigFromAvaialbleConfigsWithName(serverName);
            ServerInstance = appServerManager.CreateServerFromConfig(serverConfigs);
            ServerName = ServerInstance.Name;
            // Trigger any other initialization steps here
            UpdateCommands();
            //RegisterMessages();
        }
        public void UpdateCommands()
        {
            ConnectToDeviceCommand = new AsyncRelayCommand(ExecuteConnectToDeviceCommand);
            OnPropertyChanged(nameof(ConnectToDeviceCommand));
        }
        private async Task ExecuteConnectToDeviceCommand()
        {
            // Your command logic here
            if (ServerInstance?.ServerClients?.GetClientModel<MqttClientModel>() is MqttClientModel mqttClientModel)
            {
                await mqttClientModel.Connect();
            }
            else
            {
                Console.WriteLine(ServerInstance.Name);
                Console.WriteLine("Unable to connect");
            }
        }
    }
}

