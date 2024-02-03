
// todo rewrite this to either act as a service or manager for a given serverconfigs. well need to jsut pass the sever configs into the view models.


/*using ClientObserver.Models.Configs;
using System.Collections.Generic;
using System.Text;
// class is responsible for encapsualting the differrent configs of ourr app: mqtt, VideoStream, ModelParam, CloudConfig
namespace ClientObserver.Managers
{
    /// <summary>
    /// Manages multiple configuration instances.
    /// </summary>
    public class ServerConfigs
    {

        // For common configs were using Typed Accessors
        // So we can just call configController.MqttConfig ! 
        public MqttClientConfig MqttClientConfig { get; private set; }
        public VideoStreamConfig VideoStreamConfig { get; private set; }
        public ModelParamConfig ModelParamConfig { get; private set; }
        public CloudConfig CloudConfig { get; private set; }
        public string ServerName { get; private set; }

        /// <summary>
        /// Initializes configuration properties from a tuple containing server name and a dictionary of configurations.
        /// </summary>
        /// <param name="configTuple">Tuple with server name and configuration dictionary.</param>
        public void InitializeFromTuple(Tuple<string, Dictionary<Type, BaseConfig>> configTuple)
        {
            // Set ServerName from the tuple
            ServerName = configTuple.Item1;

            // Iterate through the dictionary to set configuration properties
            foreach (var configEntry in configTuple.Item2)
            {
                if (configEntry.Key == typeof(MqttClientConfig))
                {

                   AddConfig((MqttClientConfig)configEntry.Value);
                }
                else if (configEntry.Key == typeof(VideoStreamConfig))
                {
                    VideoStreamConfig = (VideoStreamConfig)configEntry.Value;
                }
                else if (configEntry.Key == typeof(ModelParamConfig))
                {
                    ModelParamConfig = (ModelParamConfig)configEntry.Value;
                }
                else if (configEntry.Key == typeof(CloudConfig))
                {
                    CloudConfig = (CloudConfig)configEntry.Value;
                }
                // Add other configurations as needed
            }
        }
        public void SetServerName(string serverName)
        {
            ServerName = serverName;
        }
        public void AddConfig(BaseConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            string validationResult = config.Validate();
            if (!string.IsNullOrEmpty(validationResult))
                throw new InvalidOperationException($"Invalid config: {validationResult}");

        }
        /// <summary>
        /// Retrieves a configuration of the specified type from the controller.
        /// </summary>
        /// <typeparam name="T">The type of configuration to retrieve. Must inherit from BaseConfig.</typeparam>
        /// <returns>
        /// An instance of the requested configuration type if it exists in the controller; otherwise, null.
        /// </returns>
        public T GetConfig<T>() where T : BaseConfig
        {
            // Try to retrieve the configuration from the dictionary.
            // The dictionary key is the Type of the configuration.
            if (_configDictionary.TryGetValue(typeof(T), out BaseConfig config))
            {
                // If found, cast the BaseConfig instance to the requested type (T) and return it.
                return config as T;
            }

            // If the configuration of type T is not found in the dictionary, return null.
            return null;
        }

        /// <summary>
        /// Validates all configurations.
        /// </summary>
        /// <returns>True if all configurations are valid, false otherwise.</returns>
        /// <summary>
        /// Validates all configurations stored in the controller.
        /// </summary>
        /// <returns>True if all configurations are valid, otherwise false.</returns>
        public bool ValidateConfigs()
        {
            foreach (var configPair in _configDictionary)
            {
                var config = configPair.Value;
                if (!string.IsNullOrEmpty(config.Validate()))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Formats the configurations for output.
        /// </summary>
        /// <returns>A formatted string of all configurations.</returns>
        /// <summary>
        /// Formats the configurations for output.
        /// </summary>
        /// <returns>A formatted string of all configurations.</returns>
        public string FormatConfigsForDisplay()
        {
            var builder = new StringBuilder();
            foreach (var configPair in _configDictionary)
            {
                var config = configPair.Value;
                builder.AppendLine(config.ToString()); // Assuming each config has a meaningful ToString override
            }
            return builder.ToString();
        }

    }
}
*/ 