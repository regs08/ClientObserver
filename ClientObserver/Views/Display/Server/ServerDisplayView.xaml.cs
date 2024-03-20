using ClientObserver.ViewModels.DeviceDisplay;
using ClientObserver.Models.Interfaces; 
using ClientObserver.Factories.ViewModel;
namespace ClientObserver.Views.Display.Server;

public partial class ServerDisplayView : ContentPage
{
    public ServerDisplayView()
    {
        InitializeComponent();
        InitializeViewModel();

    }

    private void InitializeViewModel()
    {
        var viewModelFactory = App.ServiceProvider.GetRequiredService<ViewModelFactory>();
        var viewModel = viewModelFactory.DeviceDisplayViewModel();
        BindingContext = viewModel;
    }


}




