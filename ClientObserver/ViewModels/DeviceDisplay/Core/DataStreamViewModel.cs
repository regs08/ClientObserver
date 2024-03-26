using System;
using ClientObserver.Services.App;
using ClientObserver.Models.Interfaces.ViewModel;
using ClientObserver.Models.Server.Core.Clients;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ClientObserver.Views.Display.Server;
using System.ComponentModel;

namespace ClientObserver.ViewModels.DeviceDisplay.Core
{
	public class DataStreamViewModel :IViewModel, IQueryAttributable
    {
        public ICommand GoBackCommand { get; set; }

        private readonly AppServerManager appServerManager;
        private VideoStreamClient _client; 
        private VideoStreamClient Client
        {
            get => _client;
            set
            {
                if (_client != value)
                {
                    _client = value;
                    OnPropertyChanged(nameof(Client));
                    OnPropertyChanged(nameof(VideoStreamUrl)); 

                }
            }
        }
        public string VideoStreamUrl
        {
            get => Client?.VideoStreamUri?.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string ServerName;
        
        public string Name { get; set; }
		public DataStreamViewModel(AppServerManager appServerManager)
		{
            this.appServerManager = appServerManager;

            Name = "DataStreamViewModel";
            GoBackCommand = new AsyncRelayCommand(ExecuteGoBackCommand);

        }
        public void InitializeCommands()
		{
            GoBackCommand = new AsyncRelayCommand(ExecuteGoBackCommand);
            OnPropertyChanged(nameof(GoBackCommand));

        }
        public void RegisterMessages()
		{

		}
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("ServerName", out var value) && value is string serverName)
            {
                ServerName = serverName;
                Initialize();
            }
        }
        private void GetClient()
        {
            if (appServerManager?.GetClientModelFromServer<VideoStreamClient>(ServerName) is VideoStreamClient videoStreamClient)
            {
                Client = videoStreamClient;
            }
        }
        private async Task ExecuteGoBackCommand()
        {
            var navigationPath = $"{nameof(DeviceDisplayView)}?ServerName={Uri.EscapeDataString(ServerName)}";
            await Shell.Current.GoToAsync(navigationPath);

        }
        private async void InitializeClient()
        {
            GetClient();
            //StreamUrl = Client.VideoStreamUri.ToString(); 

            await Client.Connect();
        }
        private void Initialize()
        {
            InitializeClient();
            InitializeCommands();
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

