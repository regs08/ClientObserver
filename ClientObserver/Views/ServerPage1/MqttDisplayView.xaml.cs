using ClientObserver.Services;
using ClientObserver.ViewModels.ServerPage;

namespace ClientObserver.Views.ServerPage;
public partial class MqttDisplayView : ContentPage
{
	public MqttDisplayView(MqttClientService mqttClientService)
	{
		InitializeComponent();
        BindingContext = new MqttDisplayViewModel(mqttClientService);
    }
}
