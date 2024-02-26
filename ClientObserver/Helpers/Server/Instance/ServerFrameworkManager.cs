using ClientObserver.Models.Server.Framework.Clients;
using ClientObserver.Helpers.BaseClasses;
using ClientObserver.Models.Server.Framework.Configs;
using ClientObserver.Models.Server.Framework;

using ClientObserver.Models.Interfaces;
using System;

namespace ClientObserver.Helpers.Server.Instance
{
    /// <summary>
    /// Manages server framework entities, including server configurations and clients, ensuring unique instances are managed and accessible.
    /// </summary>
    public class ServerFrameworkManager : AbstractModelManager<ServerFrameworkEntity>, IIdentifiableModel
    {
        /// <summary>
        /// Gets or sets the name of the server framework.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Adds a server configuration instance to the manager after validating its name matches the manager's name, ensuring uniqueness.
        /// </summary>
        /// <param name="serverConfig">The server configuration to add.</param>
        public void AddServerConfig(ServerConfigs serverConfig)
        {
            if (CheckNameOfFrameworkEntity(serverConfig))
            {
                AddModel(serverConfig);
            }
        }

        /// <summary>
        /// Adds a server clients instance to the manager after validating its name matches the manager's name, ensuring uniqueness.
        /// </summary>
        /// <param name="serverClients">The server clients to add.</param>
        public void AddServerClients(ServerClients serverClients)
        {
            if (CheckNameOfFrameworkEntity(serverClients))
            {
                AddModel(serverClients);
            }
        }

        /// <summary>
        /// Retrieves a server entity of a specific type.
        /// </summary>
        /// <typeparam name="T">The type of server entity to retrieve, must inherit from ServerFrameworkEntity.</typeparam>
        /// <returns>The server entity of the specified type.</returns>
        public T GetServerProperty<T>() where T : ServerFrameworkEntity
        {
            return GetModel<T>();
        }

        /// <summary>
        /// Validates that the name of the given server framework entity matches the manager's name.
        /// </summary>
        /// <param name="serverFrameworkEntity">The server framework entity to check.</param>
        /// <returns>True if the names match; otherwise, throws an InvalidOperationException.</returns>
        private bool CheckNameOfFrameworkEntity(ServerFrameworkEntity serverFrameworkEntity)
        {
            if (Name == serverFrameworkEntity.Name)
            {
                return true;
            }
            throw new InvalidOperationException($"Name of {serverFrameworkEntity.GetType()}," +
                $" ({serverFrameworkEntity.Name})," +
                $" must match {Name} of server entity!");
        }
    }
}
