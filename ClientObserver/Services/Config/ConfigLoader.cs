using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using ClientObserver.Configs;

namespace ClientObserver.Services
{
    public class ConfigLoader
    {
        public ConfigLoader()
        {

        }

        // loads and validates configs from a json file 
        public ServerConfigs LoadConfigsFromJson(string json)
        {
            ServerConfigs serverConfigs = new();
            dynamic fullConfig = JsonConvert.DeserializeObject<dynamic>(json);

            serverConfigs.ServerName = fullConfig["ServerName"];

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

                    serverConfigs.GetType().GetProperty(configKey)?.SetValue(serverConfigs, config);
                }
                catch (JsonException jsonEx)
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
            // Dynamically deserialize based on the type
            
            return serverConfigs;
        }
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

