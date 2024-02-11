using System.Collections.ObjectModel;
using System.Windows.Input;
using ClientObserver.ViewModels;
using ClientObserver.Views;
using CommunityToolkit.Mvvm.Messaging;
using ClientObserver.Configs;
using ClientObserver.Managers;
using ClientObserver.Models.Servers;


namespace ClientObserver
{
    // Manages server configurations, including listing available configurations and handling selection.
    public class SelectConfigViewModel
    {
        // Service for configuration management
        private AppConfigManager _appConfigManager;

        // Holds the list of available server configurations
        public ObservableCollection<ServerConfigs> AvailableConfigs { get; set; }

        // Holds the list of user-selected server configurations
        public ObservableCollection<ServerConfigs> SelectedConfigs { get; private set; }

        // Command to navigate to the configuration creation view
        public ICommand CreateConfigCommand { get; private set; }

        // Constructor initializes the ViewModel with a configuration service
        public SelectConfigViewModel(AppConfigManager appConfigManager)
        {
            _appConfigManager = appConfigManager ?? throw new ArgumentNullException(nameof(appConfigManager));
            try
            {
                // Ensures the config service has initialized its list of available configurations
                if (_appConfigManager.AvailableConfigs == null)
                {
                    throw new InvalidOperationException("ConfigService did not initialize AvailableConfigs");
                }

                // Initializes properties with data from the config service
                AvailableConfigs = _appConfigManager.AvailableConfigs;
                SelectedConfigs = _appConfigManager.SelectedConfigs;

                // Initializes the command to create a new configuration
                CreateConfigCommand = new Command(async () => await NavigateToConfigCreationView());

                // Registers to receive messages when server configurations are updated
                WeakReferenceMessenger.Default.Register<UpdateAvailableServerConfigMessage>(this, (recipient, message) =>
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
        public void SelectConfig(ServerConfigs config)
        {
            if (config != null && !SelectedConfigs.Contains(config))
            {
                // Sends a message to notify other parts of the application about the update
                WeakReferenceMessenger.Default.Send(new UpdateSelectedServerConfigMessage(config));
            }
        }

        // Updates the list of available configurations with new or updated configurations
        private void UpdateAvailableConfigs(ServerConfigs serverConfigs)
        {
            //Console.Write($"Message to update config received: {serverConfigs.FormattedDisplay}");
            var existingConfig = AvailableConfigs.FirstOrDefault(c => c.Name == serverConfigs.Name);
            if (existingConfig != null)
            {
                _appConfigManager.RemoveFromAvailableConfigs(existingConfig);
            }
            _appConfigManager.AddtoAvailableConfigs(serverConfigs);

            // Sends a message to request a UI refresh
            WeakReferenceMessenger.Default.Send(new RefreshUIMessage());
        }


        // Navigates to the configuration creation view
        private async Task NavigateToConfigCreationView()
        {
            CreateServerConfigViewModel viewModel = new(_appConfigManager);
            var createConfigPage = new CreateConfigView(viewModel);
            await Shell.Current.Navigation.PushAsync(createConfigPage);
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSelection = e.CurrentSelection.FirstOrDefault(); // Assuming single selection mode
            if (currentSelection is ServerConfigs selectedConfig)
            {
                // Handle the selection
                // e.g., navigate to a details page or display a message
            }
        }

    }
}
