using System;
using Newtonsoft.Json;
using ClientObserver.Models.Server.Framework.Configs;
using ClientObserver.Models.Server.Core.Configs;

namespace ClientObserver.Services.Configs; 

public class ConfigLoaderService
{
    public ConfigLoaderService()
    {

    }

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
/*
 * 
public List<Dictionary<Type, BaseConfig>> LoadLocalConfigs()
{
    List<Dictionary<Type, BaseConfig>> localConfigs = new();  
    foreach (var configPath in _localConfigPaths)
    {
        var configs = new Dictionary<Type, BaseConfig>();
        // Deserialize the entire JSON config once
        var fullConfig = JsonConvert.DeserializeObject<dynamic>(configPath);

        // Gets the servername 
        configs['ServerName'] = fullConfig["ServerName"];
        // Add each specific config to the dictionary
        // Ensure each specific configuration type is loaded correctly

        configs[typeof(ModelParamConfig)] = JsonConvert.DeserializeObject<ModelParamConfig>(Convert.ToString(fullConfig["ModelParamConfig"]));
        configs[typeof(MqttClientConfig)] = JsonConvert.DeserializeObject<MqttClientConfig>(Convert.ToString(fullConfig["MqttClientConfig"]));
        configs[typeof(VideoStreamConfig)] = JsonConvert.DeserializeObject<VideoStreamConfig>(Convert.ToString(fullConfig["VideoStreamConfig"]));

        localConfigs.Add(configs);
    }
    // If CloudConfig becomes part of your JSON structure, load it similarly
    // Example:
    // configs[typeof(CloudConfig)] = JsonConvert.DeserializeObject<CloudConfig>(Convert.ToString(fullConfig["CloudConfig"]));

    return localConfigs;

}

*/ 

