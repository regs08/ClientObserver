using ClientObserver.Models.Server.Core.Configs;
using ClientObserver.Helpers.BaseClasses;

namespace ClientObserver.Helpers.Server.Core.Configs
{
    // This concrete class inherits from AbstractModelManager<BaseClientModel>
    // and uses its implementation directly without any modification.
    public class ConfigModelManager : AbstractModelManager<BaseConfig>
    {
        // No additional logic is required here for now.
        // You can use the inherited AddModel, GetModel, and RemoveModel methods as they are.
        /// <summary>
        /// Formats server and configuration details for display.
        /// </summary>
        /// <returns>A formatted string of server and configuration details.</returns>
        //property to aggregate FormattedDisplay properties
        public bool ValidateAllConfigs()
        {
            if (CheckModels())
            {
            foreach (var config in Models)
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
            return false;
        }
        /// <summary>
        /// Calls the configs format for display method to return a readble string 
        /// </summary>
        /// <param name="serverName"></param>
        /// <returns></returns>
        public string CombinedFormattedDisplay(string serverName)
        {
            if (CheckModels())
            {
               var displayString = $"Server Name: {serverName}\n";
                foreach (var config in Models)
                {
                    displayString += config.FormattedDisplay + "\n";
                }
                return displayString.TrimEnd('\n'); // Remove the last newline character for cleanliness
            }
            return null;

            }
        }

    }
