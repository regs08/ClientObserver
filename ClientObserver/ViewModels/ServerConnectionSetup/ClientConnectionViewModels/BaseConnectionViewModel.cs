using System;
using System.ComponentModel;
using System.Windows.Input;
using ClientObserver.Models.Server.Core.Clients;
using ClientObserver.Models.Interfaces.Clients;
using Org.Apache.Http.Client.Params;

namespace ClientObserver.ViewModels.ServerConnectionSetup.ClientConnectionViewModels
{
    public class BaseConnectionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ConnectCommand { get; private set; }
        public ICommand DisconnectCommand { get; private set; }

        public BaseClientModel ClientModel { get; protected set; }
        public string ConnectButtonLabel => $"Connect to {ClientModel.Name}";
        public string DisconnectButtonLabel => $"Disconnect from {ClientModel.Name}";

        private bool _isConnected;
        public bool IsConnected
        {
            get => _isConnected;
            set
            {
                if (_isConnected != value)
                {
                    _isConnected = value;
                    OnPropertyChanged(nameof(IsConnected));
                    // Additional code to handle UI updates specifically for connection changes.
                }
            }
        }

        public BaseConnectionViewModel(BaseClientModel clientModel)
        {
            ClientModel = clientModel;
            ClientModel.IsConnected.ValueChanged += (sender, isConnected) =>
            {
                IsConnected = isConnected; // This will now trigger UI updates.
            };
            ConnectCommand = new Command(async () => await ClientModel.Connect());
            DisconnectCommand = new Command(async () => await ClientModel.Disconnect());
            // This might be in your ViewModel or Controller initialization

        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}

