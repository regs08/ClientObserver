using ClientObserver.Models.Server.Core.Configs;
using ClientObserver.Helpers.Server.Framework.Configs;
using System;
using System.Collections.ObjectModel;

namespace ClientObserver.Models.Server.Framework.Configs
{
    /// <summary>
    /// Manages a collection of server configuration models, providing functionalities to add, retrieve, and remove configuration models.
    /// This class ensures that only unique instances of configuration types are maintained, supporting the dynamic configuration of server-related functionalities.
    /// </summary>
    public class ServerConfigs : ServerFrameworkEntity
    {
        private readonly ConfigModelManager configModelManager = new();

        /// <summary>
        /// Gets an observable collection of BaseConfig models that represent different server configurations.
        /// </summary>
        public ObservableCollection<BaseConfig> ConfigModels => configModelManager.Models;

        /// <summary>
        /// Adds a new configuration model to the collection or updates an existing one. Ensures that only one instance of any configuration type is present.
        /// </summary>
        /// <param name="config">The configuration model to add or update.</param>
        /// <exception cref="ArgumentNullException">Thrown if the provided configuration model is null.</exception>
        public void AddConfigModel(BaseConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            configModelManager.ValidateAllConfigs();
            configModelManager.AddModel(config);
        }

        /// <summary>
        /// Retrieves all configuration models as an enumerable collection.
        /// </summary>
        /// <returns>An enumerable collection of BaseConfig instances.</returns>
        public IEnumerable<BaseConfig> GetConfigs()
        {
            return configModelManager.Models;
        }

        /// <summary>
        /// Retrieves a specific configuration model by its type.
        /// </summary>
        /// <typeparam name="T">The type of configuration model to retrieve.</typeparam>
        /// <returns>The configuration model of the specified type.</returns>
        public T GetConfigModel<T>() where T : BaseConfig
        {
            return configModelManager.GetModel<T>();
        }

        /// <summary>
        /// Removes a specific configuration model by its type.
        /// </summary>
        /// <typeparam name="T">The type of the configuration model to remove.</typeparam>
        /// <returns>True if the model was successfully removed; otherwise, false.</returns>
        public bool RemoveConfigModel<T>() where T : BaseConfig
        {
            return configModelManager.RemoveModel<T>();
        }

        /// <summary>
        /// Returns a string that represents the current object, providing a combined formatted display of the server's name and its configurations.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return configModelManager.CombinedFormattedDisplay(Name);
        }
    }
}
