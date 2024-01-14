using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ClientObserver.Views;

namespace ClientObserver.ViewModels
{


    public class MainPageViewModel
    {
        public ObservableCollection<ServerConfig> SelectedConfigs { get; private set; } = new ObservableCollection<ServerConfig>();
        private ConfigService configService;
        public ICommand LoadConfigCommand { get; private set; }
        public ICommand DeleteAllConfigsCommand { get; private set; } // New command for deleting configs

        public MainPageViewModel()
        {
            LoadConfigCommand = new Command(async () => await NavigateToConfigView());
            configService = new ConfigService();
            MessagingCenter.Subscribe<ServerConfigViewModel, ServerConfig>(this, "UpdateSelectedConfigs", (sender, config) =>
            {
                UpdateSelectedConfigs(config);
                MessagingCenter.Send(this, "RefreshUI");
            });
        }

        private void UpdateSelectedConfigs(ServerConfig config)
        {
            // Assuming you want to replace the entire list if it exists, or add it if it doesn't
            var existingConfig = SelectedConfigs.FirstOrDefault(c => c.ServerName == config.ServerName);
            if (existingConfig != null)
            {
                SelectedConfigs.Remove(existingConfig);
            }
            SelectedConfigs.Add(config);

            // Notify UI to refresh
            MessagingCenter.Send(this, "RefreshUI");
        }
        private async Task NavigateToConfigView()
        {
            var configPage = new ServerConfigView(configService);
            await Shell.Current.Navigation.PushAsync(configPage);
        }


    }
}