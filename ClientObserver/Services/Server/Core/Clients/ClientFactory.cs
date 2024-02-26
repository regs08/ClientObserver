using System;
using ClientObserver.Models.Server.Core.Clients;
using ClientObserver.Models.Server.Core.Configs;
using ClientObserver.Infrastructure.Maps;

namespace ClientObserver.Services.Server.Core.Clients
{
    /// <summary>
    /// Provides a static method to dynamically create client instances based on their configuration types.
    /// Utilizes the ConfigMaps configuration to type mapping for determining the appropriate client type.
    /// </summary>
    public static class ClientFactory
    {
        /// <summary>
        /// Creates a client instance from a given configuration.
        /// This method looks up the configuration type in the ConfigMaps to find the corresponding client type,
        /// and then dynamically creates an instance of that client type, passing the configuration to its constructor.
        /// </summary>
        /// <param name="config">The configuration object based on which the client instance should be created.</param>
        /// <returns>A new instance of a class derived from BaseClientModel that corresponds to the given configuration.</returns>
        /// <exception cref="ArgumentException">Thrown if the configuration type is not supported by the current mapping.</exception>
        public static BaseClientModel CreateClientFromConfig(BaseConfig config)
        {
            // Example using a simple type mapping
            var configToClientMap = ConfigMaps.configTypeToClientTypeMap;

            Type clientType;
            if (configToClientMap.TryGetValue(config.GetType(), out clientType))
            {
                // Dynamically create an instance of the client type using the configuration object.
                return (BaseClientModel)Activator.CreateInstance(clientType, new object[] { config });
            }

            // If the configuration type is not found in the map, throw an exception.
            throw new ArgumentException("Unsupported config type");
        }
    }
}
