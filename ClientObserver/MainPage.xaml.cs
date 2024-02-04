using ClientObserver.Views;
using ClientObserver.ViewModels;
using ClientObserver.Services;
using Microsoft.Maui.Controls;
namespace ClientObserver
{

    public partial class MainPage : ContentPage
    {
        private MainPageViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            _viewModel = new MainPageViewModel();
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.InitializeAppConfigManagerAsync();
        }
    }

}