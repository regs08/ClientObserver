using System.Collections.ObjectModel;
using System.Windows.Input;
using ClientObserver.Views;
using ClientObserver.Services;
using ClientObserver.ViewModels;
using CommunityToolkit.Mvvm.Messaging;

namespace ClientObserver.ViewModels
{
    public class MainPageViewModel
    {
        // Collection to store the selected server configurations
        public ObservableCollection<ServerConfig> SelectedConfigs { get; private set; } = new ObservableCollection<ServerConfig>();

        // Service for configuration management
        private ConfigService configService;

        // Collection of ViewModels for each server configuration button
        public ObservableCollection<ServerConfigButtonViewModel> ServerConfigButtons { get; private set; } = new ObservableCollection<ServerConfigButtonViewModel>();

        // Command to load configuration view
        public ICommand LoadConfigCommand { get; private set; }

        // Constructor
        public MainPageViewModel()
        {
            // Initialize command to navigate to the configuration view
            LoadConfigCommand = new Command(async () => await NavigateToConfigView());

            // Instantiate the config service
            configService = new ConfigService();

            // Register to listen for update messages for server configurations
            WeakReferenceMessenger.Default.Register<UpdateServerConfigMessage>(this, (recipient, message) =>
            {
                // Update the selected configurations when a message is received
                UpdateSelectedConfigs(message.NewConfig);
            });
        }

        // Updates the selected server configurations
        private void UpdateSelectedConfigs(ServerConfig config)
        {
            // Checks if the configuration already exists and updates or adds accordingly
            var existingConfig = SelectedConfigs.FirstOrDefault(c => c.ServerName == config.ServerName);
            if (existingConfig != null)
            {
                // Remove the existing configuration if found
                SelectedConfigs.Remove(existingConfig);
            }
            // Add the new or updated configuration
            SelectedConfigs.Add(config);

            // Refresh the UI for server configuration buttons
            UpdateServerConfigButtons();
        }

        // Updates the collection of server configuration buttons
        private void UpdateServerConfigButtons()
        {
            foreach (var config in SelectedConfigs)
            {
                // Check if a button ViewModel for the current config already exists
                var existingVm = ServerConfigButtons.FirstOrDefault(vm => vm.ServerName == config.ServerName);
                if (existingVm == null)
                {
                    // Create a new ViewModel for the server config button if it doesn't exist
                    var newVm = new ServerConfigButtonViewModel(config, new Command(async () =>
                    {
                        // Define the action for the button, such as navigation to a server-specific page
                        ServiceManager serverServices = new ServiceManager(config);
                        var page = new ServerPageView(serverServices);
                        await Shell.Current.Navigation.PushAsync(page);
                    }));

                    // Add the new ViewModel to the collection
                    ServerConfigButtons.Add(newVm);
                }
                // If the ViewModel already exists, no action is taken to avoid duplicates
            }
        }

        // Navigates to the server configuration view
        private async Task NavigateToConfigView()
        {
            // Create and navigate to the server configuration page
            var configPage = new ServerConfigView(configService);
            await Shell.Current.Navigation.PushAsync(configPage);
        }
    }
}
