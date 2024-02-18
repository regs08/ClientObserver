using ClientObserver.Models.Server.Framework.Configs;
using ClientObserver.Services.App.Core.Configs;

using System.Threading.Tasks;

namespace ClientObserver.Models.App.Core.Configs
{
    public class ConfigLoader
    {
        private ConfigLoaderService _configLoaderService;
        public List<string> LocalConfigPaths { get; private set; }


        public ConfigLoader()
        {
            _configLoaderService = new ConfigLoaderService();
            LocalConfigPaths=  new List<string> { "DefaultConfig.json", "GrapeModelConfig.json" };
        }

        public async Task<ServerConfigs> LoadConfigurationAsync(string path)
        {
            return await _configLoaderService.LoadConfigsFromJson(path);
        }
    }
}
