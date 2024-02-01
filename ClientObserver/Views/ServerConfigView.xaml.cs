using System.Collections.ObjectModel;
using ClientObserver.Services;
namespace ClientObserver.Views
{

    public partial class ServerConfigView : ContentPage
    {

        public ServerConfigView(ConfigService configService)
        {
            InitializeComponent();
            BindingContext = new ServerConfigViewModel(configService);
        }
        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null && e.Item is ConfigController selectedConfig)
            {
                bool answer = await DisplayAlert("Select Configuration", $"Do you want to select this configuration: {selectedConfig.ServerName}?", "Yes", "No");
                if (answer)
                {
                    // User selected 'Yes'
                    // Perform actions based on the selection, like updating the ViewModel
                    ((ServerConfigViewModel)BindingContext).AddToSelectedConfigs(selectedConfig);
                }
                // Deselect item
                ((ListView)sender).SelectedItem = null;
            }
        }

    }
}

