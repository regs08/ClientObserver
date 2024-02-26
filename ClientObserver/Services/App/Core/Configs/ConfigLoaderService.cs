using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ClientObserver.Models.Server.Core.Configs;
using ClientObserver.Models.Server.Framework.Configs;
using ClientObserver.Infrastructure.Maps;

namespace ClientObserver.Services.App.Core.Configs
{
    /// <summary>
    /// Provides services for loading server configurations from local JSON files or external sources asynchronously.
    /// </summary>
    public class ConfigLoaderService
    {
        /// <summary>
        /// Asynchronously loads server configurations from a local JSON file.
        /// </summary>
        /// <param name="jsonFile">The path to the JSON file containing the server configurations.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the loaded <see cref="ServerConfigs"/>.</returns>
        public static async Task<ServerConfigs> LoadLocalConfigsAsync(string jsonFile)
        {
            dynamic fullConfig = await GetJsonContentFromLocalFile(jsonFile);
            ServerConfigs serverConfigs = GetServerConfigsFromDeserializedJson(fullConfig);
            return serverConfigs;
        }

        /// <summary>
        /// Asynchronously reads JSON content from a local file.
        /// </summary>
        /// <param name="jsonFile">The path to the JSON file.</param>
        /// <returns>A dynamic object representing the deserialized JSON content.</returns>
        private static async Task<dynamic> GetJsonContentFromLocalFile(string jsonFile)
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync(jsonFile);
                using var reader = new StreamReader(stream);
                var jsonContent = await reader.ReadToEndAsync();
                dynamic fullConfig = JsonConvert.DeserializeObject<dynamic>(jsonContent);
                return fullConfig;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading configuration file: {ex.Message}");
                throw new JsonReaderException($"Error reading configuration file: {jsonFile}");
            }
        }

        /// <summary>
        /// Constructs a <see cref="ServerConfigs"/> object from deserialized JSON content.
        /// </summary>
        /// <param name="fullConfig">The dynamic object containing the full configuration data.</param>
        /// <returns>An instance of <see cref="ServerConfigs"/> populated with the configuration data.</returns>
        private static ServerConfigs GetServerConfigsFromDeserializedJson(dynamic fullConfig)
        {
            // Implementation of converting dynamic JSON to ServerConfigs
            ServerConfigs serverConfigs = new();

            serverConfigs.Name = fullConfig["ServerName"];
            //iterate through our configs 
            foreach (var configPair in ConfigMaps.configToNameMap)
            {
                var configType = configPair.Key;
                var configKey = configPair.Value;
                var configJson = Convert.ToString(fullConfig[configKey]);

                try
                {
                    BaseConfig config = JsonConvert.DeserializeObject(configJson, configType);
                    config.Validate();

                    //serverConfigs.GetType().GetProperty(configKey)?.SetValue(serverConfigs, config);
                    serverConfigs.AddConfigModel(config);
                }
                catch (Newtonsoft.Json.JsonException jsonEx)
                {
                    // Handle JSON-related errors (e.g., deserialization issues)
                    Console.WriteLine($"Error deserializing config for '{configKey}': {jsonEx.Message}");
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    Console.WriteLine($"Error setting config for '{configKey}': {ex.Message}");
                }
            }
            return serverConfigs;
        }

            /// <summary>
            /// [Placeholder] Asynchronously loads JSON content from an external source.
            /// </summary>
            /// <param name="source">The source from which to load the JSON content.</param>
            /// <returns>A dynamic object representing the deserialized JSON content.</returns>
            private static async Task<dynamic> GetJsonContentFromSource(string source)
        {
            // Placeholder for actual implementation
            return null;
        }

        /// <summary>
        /// Asynchronously loads server configurations from an external source.
        /// </summary>
        /// <param name="source">The source from which to load the server configurations.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the loaded <see cref="ServerConfigs"/>.</returns>
        public static async Task<ServerConfigs> LoadConfigsFromSource(string source)
        {
            dynamic fullConfig = await GetJsonContentFromSource(source);
            ServerConfigs serverConfigs = GetServerConfigsFromDeserializedJson(fullConfig);
            return serverConfigs;
        }
    }
}
