
using ClientObserver.ViewModels.DeviceDisplay;
using ClientObserver.ViewModels.ServerConnectionSetup;
using ClientObserver.Views.Display.Server;
using ClientObserver.Views.ServerConnectionSetup;
namespace ClientObserver.Services.Navigation
{
    public class NavigationServiceMain : NavigationServiceBase
    {
        protected override Dictionary<Type, Type> ViewModelToViewMapping { get; } = new Dictionary<Type, Type>
    {
        { typeof(DeviceDisplayViewModel), typeof(ServerDisplayView) },
        // Ensure the mapping is between ViewModel and View types
        { typeof(ConnectionSetupViewModel), typeof(ConnectionSetupView) }
    };

    }

}
