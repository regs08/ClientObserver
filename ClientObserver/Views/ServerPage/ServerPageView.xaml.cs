using ClientObserver.Services;
using ClientObserver.ViewModels;
namespace ClientObserver.Views
{

    public partial class ServerPageView : ContentPage
    {
        public ServerPageView(ServerPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
