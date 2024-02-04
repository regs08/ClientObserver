using Newtonsoft.Json;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace ClientObserver.Configs
{
    /// <summary>
    /// Represents server configurations including MQTT client, video stream, model parameters, and cloud configurations.
    /// Implements IEnumerable to enable iteration over its configuration objects.
    /// </summary>
    public class ServerConfigs : IEnumerable<BaseConfig>
    {
        public string ServerName { get; set; }
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

        /// <summary>
        /// Sets the server name, ensuring it is not null or whitespace.
        /// </summary>
        /// <param name="name">The name of the server.</param>
        public void SetServerName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Server name cannot be null or whitespace.", nameof(name));

            ServerName = name;
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
        /// Formats server and configuration details for display.
        /// </summary>
        /// <returns>A formatted string of server and configuration details.</returns>


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
        public override string ToString()
        {
            return CombinedFormattedDisplay;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
