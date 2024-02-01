using ClientObserver.Models.Configs;
using System.Collections.Generic;
using System.Text;

namespace ClientObserver.Managers
{
    /// <summary>
    /// Manages multiple configuration instances.
    /// </summary>
    public class ConfigController
    {
        private List<BaseConfig> _configs = new List<BaseConfig>();

        /// <summary>
        /// Adds a configuration to the controller.
        /// </summary>
        /// <param name="config">The configuration to add.</param>
        public void AddConfig(BaseConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            string validationResult = config.Validate();
            if (!string.IsNullOrEmpty(validationResult))
                throw new InvalidOperationException($"Invalid config: {validationResult}");

            _configs.Add(config);
        }

        /// <summary>
        /// Validates all configurations.
        /// </summary>
        /// <returns>True if all configurations are valid, false otherwise.</returns>
        public bool ValidateConfigs()
        {
            foreach (var config in _configs)
            {
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
        public string FormatConfigsForDisplay()
        {
            var builder = new StringBuilder();
            foreach (var config in _configs)
            {
                builder.AppendLine(config.ToString()); // Assuming each config has a meaningful ToString override
            }
            return builder.ToString();
        }
    }
}
