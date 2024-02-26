using System;
using System.Collections.ObjectModel;
using System.Linq;
using ClientObserver.Models.Server.Framework.Clients;
using ClientObserver.Helpers.App.Clients;

namespace ClientObserver.Services.App.Core.Clients
{
    /// <summary>
    /// Manages client entities within the application, providing functionalities to add, remove, and retrieve client models.
    /// This service acts as a bridge between the application's UI and the underlying client management logic.
    /// </summary>
    public class AppClientService
    {
        private AppClientManager appClientManager = new();

        /// <summary>
        /// Gets an observable collection of server clients managed by the application.
        /// This collection supports UI binding and updates, making it suitable for dynamic data presentation.
        /// </summary>
        public ObservableCollection<ServerClients> ServerClients => appClientManager.Entities;

        /// <summary>
        /// Retrieves a client model associated with the specified server name.
        /// </summary>
        /// <param name="serverName">The name of the server whose client model is to be retrieved.</param>
        /// <returns>The <see cref="ServerClients"/> model if found; otherwise, null.</returns>
        public ServerClients GetClientModel(string serverName)
        {
            return appClientManager.GetEntityByName(serverName);
        }

        /// <summary>
        /// Adds a new server client model to the application's managed collection.
        /// </summary>
        /// <param name="serverClients">The server client model to add.</param>
        public void AddSeverClients(ServerClients serverClients)
        {
            appClientManager.AddEntity(serverClients);
        }

        /// <summary>
        /// Removes a client model associated with the specified server name from the application's managed collection.
        /// </summary>
        /// <param name="serverName">The name of the server whose client model is to be removed.</param>
        public void RemoveClientModel(string serverName)
        {
            appClientManager.RemoveEntityByName(serverName);
        }
    }
}
