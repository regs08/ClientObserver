
using System.Collections.ObjectModel;
using System.Windows.Input;
using ClientObserver.ViewModels;
using ClientObserver.Views;

namespace ClientObserver
{
    public class ServerConfigViewModel
    {
        private ConfigService _configService;
        public ObservableCollection<ServerConfig> AvailableConfigs { get; private set; }
        public ICommand CreateConfigCommand { get; private set; }

        public ObservableCollection<ServerConfig> SelectedConfigs { get; private set; }

        public ServerConfigViewModel(ConfigService configService)
        {
            _configService = configService ?? throw new ArgumentNullException(nameof(configService));
            try
            {
                if (_configService.AvailableConfigs == null)
                {
                    throw new InvalidOperationException("ConfigService did not initialize AvailableConfigs");
                }

                AvailableConfigs = _configService.AvailableConfigs;
                SelectedConfigs = _configService.SelectedConfigs;
                CreateConfigCommand = new Command(async () => await NavigateToConfigCreationView());

                MessagingCenter.Subscribe<CreateServerConfigViewModel, ServerConfig>(this, "UpdateAvailableConfigs", (sender, config) =>
                {
                    UpdateAvailableConfigs(config);
                    MessagingCenter.Send(this, "RefreshUI");
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in ServerConfigViewModel constructor: {ex.Message}");
            }
        }
    

        public void AddToSelectedConfigs(ServerConfig config)
        {
            if (config != null && !SelectedConfigs.Contains(config))
            {
                _configService.AddToSelectedConfigs(config);
                // Notify MainPageViewModel that SelectedConfigs has been updated
                MessagingCenter.Send(this, "UpdateSelectedConfigs", config);
            }
        }
        private void UpdateAvailableConfigs(ServerConfig config)
        {
            Console.Write($"Messahe to update config received{config.FormattedDisplay} {AvailableConfigs}");
            var existingConfig = AvailableConfigs.FirstOrDefault(c => c.ServerName == config.ServerName);
            if (existingConfig != null)
            {
                AvailableConfigs.Remove(existingConfig);
            }
            AvailableConfigs.Add(config);

            // Notify UI to refresh
            MessagingCenter.Send(this, "RefreshUI");
        }
        private async Task NavigateToConfigCreationView()
        {
            CreateServerConfigViewModel viewModel = new(_configService);
            var createConfigPage = new CreateConfigView(viewModel);
            await Shell.Current.Navigation.PushAsync(createConfigPage);
        }
    }
}

