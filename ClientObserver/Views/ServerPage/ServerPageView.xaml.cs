using ClientObserver.Services;
using ClientObserver.ViewModels;
namespace ClientObserver.Views
{

    public partial class ServerPageView : ContentPage
    {
        public ServerPageView(ServiceManager serviceManager)
        {
            InitializeComponent();
            BindingContext = new ServerPageViewModel(serviceManager);
        }
    }
}
