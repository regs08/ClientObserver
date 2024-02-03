using System.Collections.ObjectModel;
using System.Windows.Input;
using ClientObserver.ViewModels;
using ClientObserver.Views;
using CommunityToolkit.Mvvm.Messaging;
using ClientObserver.Configs;
namespace ClientObserver
{
    // Manages server configurations, including listing available configurations and handling selection.
    public class ServerConfigViewModel
    {
        // Service for configuration management
        private ConfigService _configService;

        // Holds the list of available server configurations
        public ObservableCollection<ServerConfigs> AvailableConfigs { get; private set; }

        // Command to navigate to the configuration creation view
        public ICommand CreateConfigCommand { get; private set; }

        // Holds the list of user-selected server configurations
        public ObservableCollection<ServerConfigs> SelectedConfigs { get; private set; }

        // Constructor initializes the ViewModel with a configuration service
        public ServerConfigViewModel(ConfigService configService)
        {
            _configService = configService ?? throw new ArgumentNullException(nameof(configService));
            try
            {
                // Ensures the config service has initialized its list of available configurations
                if (_configService.AvailableConfigs == null)
                {
                    throw new InvalidOperationException("ConfigService did not initialize AvailableConfigs");
                }

                // Initializes properties with data from the config service
                AvailableConfigs = _configService.AvailableConfigs;
                SelectedConfigs = _configService.SelectedConfigs;

                // Initializes the command to create a new configuration
                CreateConfigCommand = new Command(async () => await NavigateToConfigCreationView());

                // Registers to receive messages when server configurations are updated
                WeakReferenceMessenger.Default.Register<UpdateServerConfigMessage>(this, (recipient, message) =>
                {
                    UpdateAvailableConfigs(message.NewConfig);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in ServerConfigViewModel constructor: {ex.Message}");
            }
        }

        // Adds a configuration to the list of selected configurations if it's not already selected
        public void AddToSelectedConfigs(ServerConfigs config)
        {
            if (config != null && !SelectedConfigs.Contains(config))
            {
                _configService.AddToSelectedConfigs(config);
                // Sends a message to notify other parts of the application about the update
                WeakReferenceMessenger.Default.Send(new UpdateServerConfigMessage(config));
            }
        }

        // Updates the list of available configurations with new or updated configurations
        private void UpdateAvailableConfigs(ServerConfigs config)
        {
            Console.Write($"Message to update config received: {config.FormatForDisplay}");
            var existingConfig = AvailableConfigs.FirstOrDefault(c => c.ServerName == config.ServerName);
            if (existingConfig != null)
            {
                AvailableConfigs.Remove(existingConfig);
            }
            AvailableConfigs.Add(config);

            // Sends a message to request a UI refresh
            WeakReferenceMessenger.Default.Send(new RefreshUIMessage());
        }

        // Navigates to the configuration creation view
        private async Task NavigateToConfigCreationView()
        {
            CreateServerConfigViewModel viewModel = new(_configService);
            var createConfigPage = new CreateConfigView(viewModel);
            await Shell.Current.Navigation.PushAsync(createConfigPage);
        }
    }
}
