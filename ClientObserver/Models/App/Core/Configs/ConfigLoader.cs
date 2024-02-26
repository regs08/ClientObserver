using ClientObserver.Models.Server.Framework.Configs;
using ClientObserver.Services.App.Core.Configs;
using System.Threading.Tasks;

namespace ClientObserver.Models.App.Core.Configs
{
    /// <summary>
    /// Responsible for loading server configurations from local JSON files.
    /// This class maintains a list of paths to local configuration files and provides functionality to load these configurations asynchronously.
    /// </summary>
    public class ConfigLoader
    {
        /// <summary>
        /// Gets the list of local configuration file paths.
        /// </summary>
        public List<string> LocalConfigPaths { get; private set; }

        /// <summary>
        /// Initializes a new instance of the ConfigLoader class, setting up default paths for local configuration files.
        /// </summary>
        public ConfigLoader()
        {
            // Initializes the list with default configuration file paths
            LocalConfigPaths = new List<string> { "DefaultConfig.json", "GrapeModelConfig.json" };
        }

        /// <summary>
        /// Asynchronously loads server configurations from a specified local file path.
        /// </summary>
        /// <param name="path">The path to the configuration file.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the loaded <see cref="ServerConfigs"/>.</returns>
        public static async Task<ServerConfigs> LoadLocalConfigurations(string path)
        {
            // Calls the ConfigLoaderService to load configurations asynchronously from the specified path
            return await ConfigLoaderService.LoadLocalConfigsAsync(path);
        }
    }
}
