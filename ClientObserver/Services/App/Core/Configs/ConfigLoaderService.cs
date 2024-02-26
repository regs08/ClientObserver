using System;
using Newtonsoft.Json;
using ClientObserver.Models.Server.Core.Configs;
using ClientObserver.Models.Server.Framework.Configs;
using ClientObserver.Infrastructure.Maps;

namespace ClientObserver.Services.App.Core.Configs
{
    public class ConfigLoaderService
    {
        public static async Task<ServerConfigs> LoadLocalConfigsAsync(string jsonFile)
        {
            dynamic fullConfig = await GetJsonContentFromLocalFile(jsonFile);
            ServerConfigs serverConfigs = GetServerConfigsFromDeserializedJson(fullConfig);
            return serverConfigs;
        }
        private static async Task<dynamic>GetJsonContentFromLocalFile(string jsonFile)
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

        private static ServerConfigs GetServerConfigsFromDeserializedJson(dynamic fullConfig)
        {
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
        /// to be implemented method that loads configs from an external source
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private static async Task<dynamic> GetJsonContentFromSource(string source)
        {
            return null; 
        }
        public static async Task<ServerConfigs> LoadConfigsFromSource(string source)
        {
            dynamic fullConfig = await GetJsonContentFromSource(source);
            ServerConfigs serverConfigs = GetServerConfigsFromDeserializedJson(fullConfig);
            return serverConfigs;
        }

    }
    }
    







