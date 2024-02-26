using System.Collections.ObjectModel;
using System;
using ClientObserver.Services.App.Core.Configs;
using ClientObserver.Models.Server.Framework.Clients;
using ClientObserver.Models.Server.Framework.Configs;

using ClientObserver.Services.App.Repos.Configs;
using ClientObserver.Helpers.App;
using ClientObserver.Models.Server.Instance;
using ClientObserver.Models.Server.Core.Clients;
using ClientObserver.Models.Server.Core.Configs;
namespace ClientObserver.Services.App
{
    /// <summary>
    /// Manages server instances, their clients, and configurations. Implements the Singleton pattern.
    /// </summary>
    public class AppServerManager
    {
        private static AppServerManager _instance;
        private AppServerManagerHelper appServerManagerHelper = new();
        public AppConfigService AppConfigService = new();

        private AppServerManager()
        {
        }

        /// <summary>
        /// Singleton instance of AppServerManager.
        /// Ensures that only one instance of AppServerManager exists throughout the application lifecycle.
        /// </summary>
        public static AppServerManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppServerManager();
                }
                return _instance;
            }
        }

        // Provides access to configuration repository.
        public ConfigurationRepository ConfigRepo => AppConfigService.ConfigRepo;

        // Maintains a collection of server instances.
        public ObservableCollection<ServerInstance> Servers => appServerManagerHelper.Entities;
        /// <summary>
        /// Creates a server from a passed in server config 
        /// </summary>
        /// <param name="config"></param>
        public void CreateServerFromConfig(ServerConfigs config)
        {
            ServerInstance server = new(name: config.Name);
            server.AddServerConfig(config);
            server.GetClientsFromConfig();
            appServerManagerHelper.AddEntity(server);
        }
        // --- Adders ---
        /// <summary>
        /// Creates and adds a server with just a name 
        /// </summary>
        /// <param name="serverName"></param>
        public void CreateAndAddServer(string serverName)
        {
            ServerInstance server = new(name: serverName);
            appServerManagerHelper.AddEntity(server);
        }
        /// <summary>
        /// Adds a new server instance to the collection.
        /// </summary>
        /// <param name="server">The server instance to add.</param>
        public void AddServer(ServerInstance server)
        {
            appServerManagerHelper.AddEntity(server);
        }
        /// <summary>
        /// Adds a collection of server clients to a specified server.
        /// </summary>
        /// <param name="serverName">Name of the server.</param>
        /// <param name="serverClients">Server clients to add.</param>
        public void AddServerClientsToServer(string serverName, ServerClients serverClients)
        {
            var server = GetServer(serverName);
            server?.AddServerClients(serverClients);
        }

        /// <summary>
        /// Associates a client model with a specified server.
        /// </summary>
        /// <param name="serverName">Name of the server.</param>
        /// <param name="client">Client model to add.</param>
        public void AddClientToServer(string serverName, BaseClientModel client)
        {
            var server = GetServer(serverName);
            server?.AddClient(client);
        }

        /// <summary>
        /// Adds server configurations to a specified server.
        /// </summary>
        /// <param name="serverName">Name of the server.</param>
        /// <param name="configs">Server configurations to add.</param>
        public void AddServerConfigsToServer(string serverName, ServerConfigs configs)
        {
            var server = GetServer(serverName);
            server?.AddServerConfig(configs);
        }

        /// <summary>
        /// Adds a configuration to a specified server.
        /// </summary>
        /// <param name="serverName">Name of the server.</param>
        /// <param name="config">Configuration to add.</param>
        public void AddConfigToServer(string serverName, BaseConfig config)
        {
            var server = GetServer(serverName);
            server?.AddConfig(config);
        }

        // --- Removers ---

        /// <summary>
        /// Removes a server from the collection by its name.
        /// </summary>
        /// <param name="serverName">Name of the server to remove.</param>
        public void RemoveServer(string serverName)
        {
            appServerManagerHelper.RemoveEntityByName(serverName);
        }

        // --- Getters ---

        /// <summary>
        /// Retrieves a server instance by its name.
        /// </summary>
        /// <param name="serverName">Name of the server to retrieve.</param>
        /// <returns>The server instance if found; otherwise, null.</returns>
        public ServerInstance GetServer(string serverName)
        {
            return appServerManagerHelper.GetEntityByName(serverName);
        }

        /// <summary>
        /// Retrieves server clients associated with a specified server.
        /// </summary>
        /// <param name="serverName">Name of the server.</param>
        /// <returns>Server clients if found; otherwise, null.</returns>
        public ServerClients GetServerClients(string serverName)
        {
            ServerInstance server = GetServer(serverName);
            return server?.GetServerProperty<ServerClients>();
        }

        /// <summary>
        /// Retrieves a client model of a specific type from a specified server.
        /// </summary>
        /// <typeparam name="T">Type of the client model.</typeparam>
        /// <param name="serverName">Name of the server.</param>
        /// <returns>Client model if found; otherwise, null.</returns>
        public T GetClientModelFromServer<T>(string serverName) where T : BaseClientModel
        {
            ServerClients serverClients = GetServerClients(serverName);
            return serverClients?.GetClientModel<T>();
        }

        /// <summary>
        /// Retrieves server configurations associated with a specified server.
        /// </summary>
        /// <param name="serverName">Name of the server.</param>
        /// <returns>Server configurations if found; otherwise, null.</returns>
        public ServerConfigs GetServerConfigs(string serverName)
        {
            ServerInstance server = GetServer(serverName);
            return server?.GetServerProperty<ServerConfigs>();
        }

        /// <summary>
        /// Retrieves a configuration model of a specific type from a specified server.
        /// </summary>
        /// <typeparam name="T">Type of the configuration model.</typeparam>
        /// <param name="serverName">Name of the server.</param>
        /// <returns>Configuration model if found; otherwise, null.</returns>
        public T GetConfigFromServer<T>(string serverName) where T : BaseConfig
        {
            ServerConfigs serverConfigs = GetServerConfigs(serverName);
            return serverConfigs?.GetConfigModel<T>();
        }
    }
}
