using System;
using System.Windows.Input;
using ClientObserver.Models.Server.Core.Clients;
namespace ClientObserver.ViewModels.ServerConnectionSetup.ClientConnectionViewModels
{
	public class MqttConnectionViewModel
	{
        public ICommand ConnectCommand { get; private set; }
        public ICommand SendPingCommand { get; private set; }
		public MqttClientModel ClientModel { get; private set; }
        // Holds the latest text received from MQTT
        public string ReceivedPing { get; private set; }

        public MqttConnectionViewModel(MqttClientModel clientModel)			
		{
			ClientModel = clientModel;
            ClientModel.IsConnected.ValueChanged += (sender, isConnected) =>
            {
                // Update the UI to reflect the connection status
                if (isConnected)
                {
                    // Code to update UI indicating that the connection is established
                    Console.WriteLine("Connected to MQTT broker.");
                }
                else
                {
                    // Code to update UI indicating that the connection is lost
                    Console.WriteLine("Disconnected from MQTT broker.");
                }
            };

            ConnectCommand = new Command(async () => await ClientModel.Connect());
            // This might be in your ViewModel or Controller initialization

        }

    }
}

