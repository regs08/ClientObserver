using ClientObserver.Models.Server.Core.Configs;
using ClientObserver.Helpers.Server.Framework.Configs;
using System.Collections.ObjectModel;

namespace ClientObserver.Models.Server.Framework.Configs
{
    public class ServerConfigs : ServerFrameworkEntity
    {
        private readonly ConfigModelManager configModelManager = new ();
        public ObservableCollection<BaseConfig> ConfigModels => configModelManager.Models; // where the seperate configs are stored 

        // used for reading from json a type map 
        public static readonly Dictionary<Type, string> ConfigKeys = new Dictionary<Type, string>
    {
        { typeof(MqttClientConfig), "MqttClientConfig" },
        { typeof(VideoStreamConfig), "VideoStreamConfig" },
        { typeof(ModelParamConfig), "ModelParamConfig" },
        { typeof(CloudConfig), "CloudClientConfig" }
    };
        public ServerConfigs()
        {
        }

        // Method to add or update a client model. Only one instance per type is allowed.
        public void AddConfigModel(BaseConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            configModelManager.ValidateAllConfigs();
            configModelManager.AddModel(config);
        }

        // Method to return non-null instances of BaseClientModel
        public IEnumerable<BaseConfig> GetConfigs()
        {
            // Leverage the ObservableCollection directly for binding or convert it to a list for other purposes
            return configModelManager.Models;
        }

        // Method to retrieve a specific client model by type
        public T GetConfigModel<T>() where T : BaseConfig
        {
            return configModelManager.GetModel<T>();
        }

        // Method to remove a client model by type
        public bool RemoveConfigModel<T>() where T : BaseConfig
        {
            return configModelManager.RemoveModel<T>();
        }
        public override string ToString()
        {
            return configModelManager.CombinedFormattedDisplay(Name);
        }
    }
}