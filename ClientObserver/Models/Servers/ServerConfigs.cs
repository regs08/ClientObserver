using Newtonsoft.Json;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using ClientObserver.Configs;
using ClientObserver.Models.Servers;

namespace ClientObserver.Models.Servers;

// todo I think we should organize our BAse config classes into that of config details 
/// <summary>
/// Represents server configurations including MQTT client, video stream, model parameters, and cloud configurations.
/// Implements IEnumerable to enable iteration over its configuration objects.
/// </summary>
public class ServerConfigs : ServerManagerBase, IEnumerable<BaseConfig>
{
    public MqttClientConfig MqttClientConfig { get; set; }
    public VideoStreamConfig VideoStreamConfig { get; set; }
    public ModelParamConfig ModelParamConfig { get; set; }
    public CloudConfig CloudConfig { get; set; }
   
    /// <summary>
    /// A dictionary mapping configuration object types to their JSON keys. Used for dynamic deserialization.
    /// </summary>
    public static readonly Dictionary<Type, string> ConfigKeys = new Dictionary<Type, string>
    {
        { typeof(MqttClientConfig), "MqttClientConfig" },
        { typeof(VideoStreamConfig), "VideoStreamConfig" },
        { typeof(ModelParamConfig), "ModelParamConfig" },
        { typeof(CloudConfig), "CloudConfig" }
    };

    // Stores non null properties in a helper class 
    public List<ConfigDetail> ConfigDetails
    {
        get
        {
            var details = new List<ConfigDetail>();

            foreach (var config in GetBaseConfigValues())
            {
                if (config.Value != null)
                {
                    details.Add(new ConfigDetail
                    {
                        Name = ConfigKeys[config.Key],
                        Config = config.Value
                    });
                }
            }

            return details;
        }
    }

    /// <summary>
    /// Formats server and configuration details for display.
    /// </summary>
    /// <returns>A formatted string of server and configuration details.</returns>
    //property to aggregate FormattedDisplay properties
    public string CombinedFormattedDisplay
    {
        get
        {
            var displayString = $"Server Name: {ServerName}\n";
            foreach (var config in this)
            {
                displayString += config.FormattedDisplay + "\n";
            }
            return displayString.TrimEnd('\n'); // Remove the last newline character for cleanliness
        }
    }


    /// <summary>
    /// Adds a new configuration object if one does not already exist for its type.
    /// </summary>
    /// <param name="newConfig">The new configuration object to add.</param>
    public void AddConfig(BaseConfig newConfig)
    {
        var configType = newConfig.GetType();
        if (ConfigKeys.TryGetValue(configType, out var propertyName))
        {
            var property = this.GetType().GetProperty(propertyName);
            if (property != null && property.GetValue(this) != null)
                throw new InvalidOperationException($"Configuration for {propertyName} already exists.");

            newConfig.Validate();
            property.SetValue(this, newConfig);
        }
        else
        {
            throw new ArgumentException($"Configuration type {configType.Name} is not supported.", nameof(newConfig));
        }
    }

    // Validates all configurations and ensures they are correctly set up
    public bool ValidateAllConfigs()
    {
        foreach (var config in this)
        {
            var validationMessage = config.Validate();
            if (validationMessage != null)
            {
                // If the validation message is not null, it means there is an error
                throw new InvalidOperationException($"Validation failed for {config.GetType().Name}: {validationMessage}");
            }
        }
        // If all configs are valid, return true
        return true;
    }

    /// <summary>
    /// Gets the configuration values based on the defined ConfigKeys dict. Searches the class properties. sees if it has a non-null.
    /// adds it to the dict if its not null 
    /// </summary>
    /// <returns>A dictionary with types as keys and actual configuration objects or null as values.</returns>
    public Dictionary<Type, BaseConfig> GetBaseConfigValues()
    {
        var result = new Dictionary<Type, BaseConfig>();

        foreach (var entry in ConfigKeys)
        {
            var property = this.GetType().GetProperty(entry.Value);
            if (property != null)
            {
                var configValue = property.GetValue(this) as BaseConfig;
                result.Add(entry.Key, configValue);
            }
            else
            {
                // If the property doesn't exist on the object, add the key with a null value.
                result.Add(entry.Key, null);
            }
        }

        return result;
    }


    public override string ToString()
    {
        return CombinedFormattedDisplay;
    }

    /// <summary>
    /// Provides an enumerator for iterating over BaseConfig objects.
    /// </summary>
    /// <returns>An IEnumerator of BaseConfig.</returns>
    public IEnumerator<BaseConfig> GetEnumerator()
    {
        if (MqttClientConfig != null) yield return MqttClientConfig;
        if (VideoStreamConfig != null) yield return VideoStreamConfig;
        if (ModelParamConfig != null) yield return ModelParamConfig;
        if (CloudConfig != null) yield return CloudConfig;
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
// Helper class to hold configuration detail
public class ConfigDetail
{
    public string Name { get; set; }
    public BaseConfig Config { get; set; }
}
