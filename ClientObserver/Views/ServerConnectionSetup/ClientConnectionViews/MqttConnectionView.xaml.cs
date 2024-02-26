using ClientObserver.ViewModels.ServerConnectionSetup.ClientConnectionViewModels;

namespace ClientObserver.Views.ServerConnectionSetup.ClientConnectionViews
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