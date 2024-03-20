using ClientObserver.Models.Server.Core.Clients;
namespace ClientObserver.ViewModels.ServerConnectionSetup.ClientConnectionViewModels
{
	public class MqttConnectionViewModel : BaseConnectionViewModel
	{
        public MqttClientModel MqttClientModel => ClientModel as MqttClientModel;


        public MqttConnectionViewModel(MqttClientModel clientModel) : base(clientModel)
        {
        }
 

    }
}

