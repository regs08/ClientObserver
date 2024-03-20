using ClientObserver.ViewModels.ServerConnectionSetup;
namespace ClientObserver.Views.ServerConnectionSetup;

public partial class ConnectionSetupView : ContentPage
{
	public ConnectionSetupView(ConnectionSetupViewModel viewModel)
	{
            InitializeComponent();
            BindingContext = viewModel;
	}
}