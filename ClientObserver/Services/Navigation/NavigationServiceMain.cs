
using ClientObserver.ViewModels.DeviceDisplay;
using ClientObserver.ViewModels.ServerConnectionSetup;
using ClientObserver.Views.Display.Server;
using ClientObserver.Views.ServerConnectionSetup;
using ClientObserver.Views.Display.Server.Core;
using ClientObserver.ViewModels.DeviceDisplay.Core;
namespace ClientObserver.Services.Navigation
{
    public class NavigationServiceMain : NavigationServiceBase
    {
        protected override Dictionary<Type, Type> ViewModelToViewMapping { get; } = new Dictionary<Type, Type>
    {
        { typeof(DeviceDisplayViewModel), typeof(DeviceDisplayView) },
        { typeof(ConnectionSetupViewModel), typeof(ConnectionSetupView) },
        { typeof(DataStreamViewModel ), typeof(DataStreamView)}
    };

    }

}
