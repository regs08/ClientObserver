using Microsoft.Extensions.Logging;
using ClientObserver.Models.Interfaces.Messaging;
using ClientObserver.Models.Interfaces.Navigation;
using ClientObserver.Services.App.Messaging;
using ClientObserver.Services.Navigation;
using ClientObserver.ViewModels;
using ClientObserver.ViewModels.ServerDisplay;
using ClientObserver.Services.App;
using ClientObserver.Views;
using ClientObserver.Views.Display.Server;
using ClientObserver.Services.App.Core.Configs;
using ClientObserver.Factories.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace ClientObserver;

public static class MauiProgram
{
    public static IServiceProvider ServiceProvider { get; private set; }

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Register your services
        builder.Services.AddSingleton<AppConfigService>();
        builder.Services.AddSingleton<AppServerManager>();
        builder.Services.AddSingleton<IMessagingService, MessagingService>();
        builder.Services.AddSingleton<NavigationServiceMain>();
        builder.Services.AddSingleton<MainPageViewModelTest>();
        builder.Services.AddSingleton<MainPageTestView>();
        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<ViewModelFactory>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // Build the MauiApp
        var app = builder.Build();
        // Assign the built service provider to the static property
        ServiceProvider = app.Services;
        // Access ServiceProvider from app.Services
        App.ServiceProvider = app.Services;
        return app;
    }
}
