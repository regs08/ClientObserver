using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using ClientObserver.Configs;
using ClientObserver.Managers;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ClientObserver.Views;

namespace ClientObserver.ViewModels
{
    public class ConnectionSetupViewModel
    {
        private AppConfigManager _appConfigManager;
        
        public ObservableCollection<ServerConfigs> MyAvailableConfigs { get; set; }
        public ObservableCollection<ServerConfigs> MySelectedConfigs { get; set; }
        public ICommand NavigateCommand { get; private set; }

        public ConnectionSetupViewModel(AppConfigManager appConfigManager)
        {
            _appConfigManager = appConfigManager;
            MyAvailableConfigs = _appConfigManager.AvailableConfigs;
            MySelectedConfigs = _appConfigManager.SelectedConfigs; 
            NavigateCommand = new Command<BaseConfig>(ExecuteNavigateCommand);
        }

        private async void ExecuteNavigateCommand(BaseConfig config)
        {
            // Determine the type of config
            Type configType = config.GetType();

            // Use ConfigKeys to decide navigation based on config type
            if (ServerConfigs.ConfigKeys.TryGetValue(configType, out string configKey))
            {
                // Example of navigating based on configKey
                // You need to adjust the navigation logic to your application's structure
                Page targetPage = null;
                switch (configKey)
                {
                    case "MqttClientConfig":
                        targetPage = new MqttConnectionPageView(config); // Assuming you have this page and constructor
                        break;
                        // Add other cases for different config types
                }

                if (targetPage != null)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(targetPage);
                }
            }
        }
    }
}
