using ClientObserver.Views;
using ClientObserver.ViewModels;
using ClientObserver.Services;
using Microsoft.Maui.Controls;
namespace ClientObserver
{

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {

            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }
        }
    }


