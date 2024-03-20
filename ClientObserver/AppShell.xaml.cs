using ClientObserver.Views;
using ClientObserver.Views.Display.Server;
namespace ClientObserver;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(MainPageTestView), typeof(MainPageTestView));
        Routing.RegisterRoute(nameof(ServerDisplayView), typeof(ServerDisplayView));


    }
}

