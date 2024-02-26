using System;
using ClientObserver.Models.Server.Core.Clients;
using ClientObserver.ViewModels.ServerConnectionSetup.ClientConnectionViewModels;

namespace ClientObserver.Infrastructure.Maps
{
    /// <summary>
    /// Provides a mapping between client model types and their corresponding view model types.
    /// This class facilitates the conversion or association of client models with their respective
    /// view models for UI representation and interaction.
    /// </summary>
    public class ClientMaps
    {
        /// <summary>
        /// Static dictionary holding the mapping between client types and their corresponding view model types.
        /// This map is utilized to dynamically associate client models with view models across the application,
        /// supporting a variety of client types such as MQTT, Cloud, Model Parameters, and Video Streams.
        /// </summary>
        public static Dictionary<Type, Type> clientToViewModelMap = new Dictionary<Type, Type>
        {
            // Maps MqttClientModel to MqttConnectionViewModel for MQTT client configurations.
            { typeof(MqttClientModel), typeof(MqttConnectionViewModel) },

            // Maps CloudClient to CloudClientConnectionViewModel for cloud client configurations.
            { typeof(CloudClient) , typeof(CloudClientConnectionViewModel) },

            // Maps ModelParamClient to ModelParamConnectionViewModel for model parameter client configurations.
            { typeof(ModelParamClient), typeof(ModelParamConnectionViewModel)},

            // Maps VideoStreamClient to VideoStreamConnectionViewModel for video stream client configurations.
            { typeof(VideoStreamClient), typeof(VideoStreamConnectionViewModel)}
        };
    }
}
