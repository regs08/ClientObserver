using ClientObserver.ViewModels;
namespace ClientObserver.Views;

public partial class ConnectionSetupView : ContentPage
{
	public ConnectionSetupView(ConnectionSetupViewModel viewModel)
	{
            InitializeComponent();
            BindingContext = viewModel;
	}
}