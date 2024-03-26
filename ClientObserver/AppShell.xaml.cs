using ClientObserver.Views;
using ClientObserver.Views.Display.Server.Core;
using ClientObserver.Views.Display.Server;
namespace ClientObserver;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(DeviceDisplayView), typeof(DeviceDisplayView));
        Routing.RegisterRoute(nameof(DataStreamView), typeof(DataStreamView));



    }
}

