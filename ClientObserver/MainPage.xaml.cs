using ClientObserver.ViewModels;
using ClientObserver.Helpers.DI;
using ClientObserver.Factories.ViewModel;
namespace ClientObserver;

public partial class MainPage : ContentPage
{
    MainPageViewModel viewModel;

    public MainPage(ViewModelFactory viewModelFactory)
    {
        InitializeComponent();
        viewModel = viewModelFactory.CreateMainPageViewModel();
        BindingContext = viewModel;

    }



    protected override async void OnAppearing()
        {
            base.OnAppearing();
            viewModel.InitializeAppConfigManagerAsync();
        }
    }

    