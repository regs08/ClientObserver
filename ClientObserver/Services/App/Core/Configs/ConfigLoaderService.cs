using System;
using Newtonsoft.Json;
using ClientObserver.Models.Server.Core.Configs;
using ClientObserver.Models.Server.Framework.Configs;

namespace ClientObserver.Services.App.Core.Configs
{
    public class ConfigLoaderService
    {
        public ConfigLoaderService()
        {

        }
        "Need to split this up so we can devour these configs and make them into models then to our server framework and then.... "
        // loads and validates configs from a json file 
        public async Task<ServerConfigs> LoadConfigsFromJson(string jsonFile)
        {
            ServerConfigs serverConfigs = new();
            try
            {

                using var stream = await FileSystem.OpenAppPackageFileAsync(jsonFile);
                using var reader = new StreamReader(stream);
                var jsonContent = await reader.ReadToEndAsync();

                dynamic fullConfig = JsonConvert.DeserializeObject<dynamic>(jsonContent);

                serverConfigs.Name = fullConfig["ServerName"];
                //iterate through our configs 
                foreach (var configPair in ServerConfigs.ConfigKeys)
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
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error reading configuration file: {ex.Message}");
                // Handle errors related to file access or reading
            }
            return serverConfigs;
        }
    }

}





