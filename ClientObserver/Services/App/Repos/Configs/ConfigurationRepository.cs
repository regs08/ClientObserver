using System.Collections.ObjectModel;
using ClientObserver.Models.Server.Framework.Configs;
using ClientObserver.Helpers.App.Configs;

namespace ClientObserver.Services.App.Repos.Configs
{
    public class ConfigurationRepository
    {
        private AppConfigManager availableConfigManager = new ();
        private AppConfigManager selectedConfigManager = new();

        public ObservableCollection<ServerConfigs> AvailableConfigs => availableConfigManager.Entities;
        public ObservableCollection<ServerConfigs> SelectedConfigs => selectedConfigManager.Entities;

        public void AddToAvailableConfigs(ServerConfigs serverConfigs)
        {
            availableConfigManager.AddEntity(serverConfigs);
        }

        public void AddToSelectedConfigs(ServerConfigs serverConfigs)
        {
            selectedConfigManager.AddEntity(serverConfigs);
        }

        public void RemoveFromSelectedConfigs(ServerConfigs serverConfigs)
        {
            selectedConfigManager.RemoveEntityByName(serverConfigs.Name);
        }

        public void RemoveFromAvailableConfigs(ServerConfigs serverConfigs)
        {
            availableConfigManager.RemoveEntityByName(serverConfigs.Name);

        }
        public ServerConfigs GetConfigFromAvaialbleConfigsWithName(string serverName)
        {
           return availableConfigManager.GetEntityByName(serverName);

        }
        public ServerConfigs GetConfigFromSelectedConfigsWithName(string serverName)
        {
            return selectedConfigManager.GetEntityByName(serverName);

        }
    }
}
