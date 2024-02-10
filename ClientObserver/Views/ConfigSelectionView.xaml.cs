﻿using ClientObserver.Models.Servers;

namespace ClientObserver.Views
{
    public partial class ConfigSelectionView : ContentPage
    {
        public ConfigSelectionView(SelectConfigViewModel viewModel)
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
                    ((SelectConfigViewModel)BindingContext).SelectConfig(selectedConfig);
                }
                // Deselect item
                ((ListView)sender).SelectedItem = null;
            }
        }
    }
}

