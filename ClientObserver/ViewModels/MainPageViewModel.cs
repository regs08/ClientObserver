using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ClientObserver.Views;
using ClientObserver.Services;

namespace ClientObserver.ViewModels
{


    public class MainPageViewModel
    {
        public ObservableCollection<ServerConfig> SelectedConfigs { get; private set; } = new ObservableCollection<ServerConfig>();
        private ConfigService configService;
        public ObservableCollection<ServerConfigButtonViewModel> ServerConfigButtons { get; private set; } = new ObservableCollection<ServerConfigButtonViewModel>();

        public ICommand LoadConfigCommand { get; private set; }

        public MainPageViewModel()
        {
            LoadConfigCommand = new Command(async () => await NavigateToConfigView());
            configService = new ConfigService();
            MessagingCenter.Subscribe<ServerConfigViewModel, ServerConfig>(this, "UpdateSelectedConfigs", (sender, config) =>
            {
                UpdateSelectedConfigs(config);
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
            UpdateServerConfigButtons();
        }

        private void UpdateServerConfigButtons()
        {
            foreach (var config in SelectedConfigs)
            {
                // Check if a ViewModel for the current config already exists
                var existingVm = ServerConfigButtons.FirstOrDefault(vm => vm.ServerName == config.ServerName);
                if (existingVm == null)
                {
                    // If it doesn't exist, create a new ViewModel and add it to the collection
                    var newVm = new ServerConfigButtonViewModel(config, new Command(async () =>
                    {
                        // Navigation logic or other action
                        ServiceManager serverServices = new ServiceManager(config);
                        var page = new ServerPageView(serverServices);
                        await Shell.Current.Navigation.PushAsync(page);
                    }));

                    ServerConfigButtons.Add(newVm);
                }
                // If the ViewModel already exists, no action is taken.
            }
        }

        private async Task NavigateToConfigView()
        {
            var configPage = new ServerConfigView(configService);
            await Shell.Current.Navigation.PushAsync(configPage);
        }


    }
}