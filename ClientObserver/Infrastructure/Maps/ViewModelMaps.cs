using System;
using ClientObserver.ViewModels.ServerConnectionSetup.ClientConnectionViewModels;
using ClientObserver.Views.ServerConnectionSetup.ClientConnectionViews;

namespace ClientObserver.Infrastructure.Maps
{
    /// <summary>
    /// Contains mappings between ViewModel types and View types for the server connection setup.
    /// This class facilitates the dynamic creation and association of Views with their corresponding ViewModels,
    /// enabling a decoupled architecture where navigation and UI logic are kept separate from the UI presentation layer.
    /// </summary>
    public class ViewModelMaps
    {
        /// <summary>
        /// A static dictionary mapping ViewModel types to their corresponding View types.
        /// This map is utilized by the application to instantiate views based on the ViewModel type dynamically,
        /// supporting a modular and maintainable approach to UI development where Views are decoupled from their ViewModels.
        /// </summary>
        public static Dictionary<Type, Type> viewModelTypeToViewTypeMap = new Dictionary<Type, Type>
        {
            { typeof(MqttConnectionViewModel), typeof(MqttConnectionView) },
            { typeof(CloudClientConnectionViewModel), typeof(CloudClientConnectionView) },
            { typeof(ModelParamConnectionViewModel), typeof(ModelParamConnectionView)},
            { typeof(VideoStreamConnectionViewModel), typeof(VideoStreamConnectionView) }
            // Additional mappings can be added here to accommodate new ViewModel-View pairs as the application grows.
        };
    }
}
