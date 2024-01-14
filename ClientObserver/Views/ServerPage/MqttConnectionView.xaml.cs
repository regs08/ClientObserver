using ClientObserver.Services;
namespace ClientObserver.Views;
public partial class MqttConnectionView : ContentPage
{
	public MqttConnectionView(MqttClientService mqttClientService)
	{
		InitializeComponent();
        BindingContext = new MqttConnectionViewModel(mqttClientService);
    }
}
