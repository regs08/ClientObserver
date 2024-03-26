using ClientObserver.Factories.ViewModel;

namespace ClientObserver.Views.Display.Server.Core;

public partial class DataStreamView : ContentPage
{
	public DataStreamView()
	{
		InitializeComponent();
        InitializeViewModel();

    }

    private void InitializeViewModel()
    {
        var viewModelFactory = App.ServiceProvider.GetRequiredService<ViewModelFactory>();
        var viewModel = viewModelFactory.CreateDataStreamViewModel();
        BindingContext = viewModel;
    }

}
