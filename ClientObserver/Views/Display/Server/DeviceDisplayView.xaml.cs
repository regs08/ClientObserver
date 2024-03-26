
using ClientObserver.Factories.ViewModel;
namespace ClientObserver.Views.Display.Server;

public partial class DeviceDisplayView : ContentPage
{
    public DeviceDisplayView()
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




