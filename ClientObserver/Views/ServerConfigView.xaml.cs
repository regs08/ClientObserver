using ClientObserver.Configs;
using ClientObserver.Managers;
namespace ClientObserver.Views

{

    public partial class ServerConfigView : ContentPage
    {

        public ServerConfigView(ServerConfigViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null && e.Item is ServerConfigs selectedConfig)
            {
                bool answer = await DisplayAlert("Select Configuration", $"Do you want to select this configuration: {selectedConfig.ServerName}?", "Yes", "No");
                if (answer)
                {
                    // User selected 'Yes'
                    // Perform actions based on the selection, like updating the ViewModel
                    ((ServerConfigViewModel)BindingContext).SelectConfig(selectedConfig);
                }
                // Deselect item
                ((ListView)sender).SelectedItem = null;
            }
        }
        public async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if there is a newly selected item
            if (e.CurrentSelection.FirstOrDefault() is ServerConfigs selectedConfig)
            {
                bool answer = await DisplayAlert("Select Configuration", $"Do you want to select this configuration: {selectedConfig.ServerName}?", "Yes", "No");
                if (answer)
                {
                    // User selected 'Yes'
                    // Perform actions based on the selection, like updating the ViewModel
                    ((ServerConfigViewModel)BindingContext).SelectConfig(selectedConfig);
                }

                // Deselect item
                ((CollectionView)sender).SelectedItem = null;
            }
        }
    }
}

