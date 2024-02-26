using System;
using ClientObserver.Models.Server.Core.Clients;
using ClientObserver.Models.Server.Core.Configs;

namespace ClientObserver.Infrastructure.Maps
{
    /// <summary>
    /// Provides mapping between configuration types and their corresponding client types, and configuration types to their names.
    /// This class is used to dynamically resolve client types based on configuration objects and to obtain human-readable names for configurations.
    /// </summary>
    public class ConfigMaps
    {
        /// <summary>
        /// A static dictionary mapping configuration types to their corresponding client types.
        /// This map is used to instantiate client objects dynamically based on their configuration.
        /// </summary>
        public static Dictionary<Type, Type> configTypeToClientTypeMap = new Dictionary<Type, Type>
        {
            { typeof(MqttClientConfig), typeof(MqttClientModel) },
            { typeof(CloudConfig), typeof(CloudClient) },
            { typeof(ModelParamConfig), typeof(ModelParamClient)},
            { typeof(VideoStreamConfig), typeof(VideoStreamClient) }
            // Additional mappings can be added here as the system expands to support more client types.
        };

        /// <summary>
        /// A static dictionary mapping configuration types to their names as strings.
        /// This map is useful for logging, diagnostics, and user interface representations, providing a human-readable name for each configuration type.
        /// </summary>
        public static Dictionary<Type, string> configToNameMap = new Dictionary<Type, string>
        {
            { typeof(MqttClientConfig), "MqttClientConfig" },
            { typeof(VideoStreamConfig), "VideoStreamConfig" },
            { typeof(ModelParamConfig), "ModelParamConfig" },
            { typeof(CloudConfig), "CloudClientConfig" }
            // Additional mappings can be added here to include new configuration types.
        };
    }
}
