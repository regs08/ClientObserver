using ClientObserver.Views;
using ClientObserver.ViewModels;
using ClientObserver.Services;
using Microsoft.Maui.Controls;
namespace ClientObserver
{

    public partial class MainPage : ContentPage
    {
        private Dictionary<string, Button> serverConfigButtons = new Dictionary<string, Button>();

        public MainPage()
        {

            InitializeComponent();
            BindingContext = new MainPageViewModel();
            MessagingCenter.Subscribe<MainPageViewModel>(this, "RefreshUI", (sender) =>
            {
                UpdateUIWithSelectedConfigs();
            });

        }

        private void UpdateUIWithSelectedConfigs()
        {
            var viewModel = BindingContext as MainPageViewModel;
            foreach (var config in viewModel.SelectedConfigs)
            {
                Console.Write(config.ServerName);
                if (!serverConfigButtons.TryGetValue(config.ServerName, out var button))
                {
                    button = new Button
                    {

                        Text = config.ServerName,
                        Command = new Command(async () =>
                        {
                            // Retrieve services from the ServiceManager

                            ServiceManager serverServices = new ServiceManager(config);

                            // Pass the services to the ServerPageView
                            var page = new ServerPageView(serverServices);
                            await Shell.Current.Navigation.PushAsync(page);
                        })
                    };

                    serverConfigButtons[config.ServerName] = button;
                    myButtonContainer.Children.Add(button);
                }
            }

            // Optional: Remove buttons for configs that are no longer selected
            var configsToRemove = serverConfigButtons.Keys.Except(viewModel.SelectedConfigs.Select(c => c.ServerName)).ToList();
            foreach (var configName in configsToRemove)
            {
                if (serverConfigButtons.TryGetValue(configName, out var buttonToRemove))
                {
                    myButtonContainer.Children.Remove(buttonToRemove);
                    serverConfigButtons.Remove(configName);
                }
            }
        }
    }


}

