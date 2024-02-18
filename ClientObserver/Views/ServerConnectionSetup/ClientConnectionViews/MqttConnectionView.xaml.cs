using ClientObserver.ViewModels.ServerConfigConnectionSetup;

namespace ClientObserver.Views.ServerConfigConnectionSetup
{

public partial class MqttConnectionView : ContentPage
{
	public MqttConnectionView(MqttConnectionViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
}