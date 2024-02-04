using ClientObserver.Configs;

namespace ClientObserver.Views;

public partial class MqttConnectionPageView : ContentPage
{
	public MqttConnectionPageView(BaseConfig config)
	{
		InitializeComponent();
		BindingContext = config;
	}
}
