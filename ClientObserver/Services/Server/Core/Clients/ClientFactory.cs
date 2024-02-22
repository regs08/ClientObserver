using System;
using ClientObserver.Models.Server.Core.Clients;
using ClientObserver.Models.Server.Core.Configs;
using ClientObserver.Models.Interfaces.Clients;

namespace ClientObserver.Services.Server.Core.Clients
{
    public static class ClientFactory
    {
        public static BaseClientModel CreateClientFromConfig(BaseConfig config)
        {
            //todo need to update the clients once i have them written 
            // Example using a simple type mapping
            var configTypeToClientTypeMap = new Dictionary<Type, Type>
        {
            { typeof(MqttClientConfig), typeof(MqttClientModel) },
            { typeof(CloudConfig), typeof(CloudClient) },
            { typeof(ModelParamConfig), typeof(ModelParamClient)},
                {typeof(VideoStreamConfig), typeof(VideoStreamClient) }
            // Add more mappings here
        };

            Type clientType;
            if (configTypeToClientTypeMap.TryGetValue(config.GetType(), out clientType))
            {
                return (BaseClientModel)Activator.CreateInstance(clientType, new object[] { config });
            }

            throw new ArgumentException("Unsupported config type");
        }
    }

}

