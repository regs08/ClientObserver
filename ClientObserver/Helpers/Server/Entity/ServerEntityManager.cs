using ClientObserver.Models.Servers;
using ClientObserver.Helpers.BaseClasses;
using ClientObserver.Models.Server.Managers;

using ClientObserver.Models.Server.Core; 
using System;

namespace ClientObserver.Helpers.Server.Entity
{
    public class ServerEntityManager : AbstractModelManager<ServerCoreEntity> // Assuming AbstractModelManager is adaptable for this use
       //we could pass in a new list of  properties like accepted types.. then in  our add model we can check if the type is in there or not 
    {
        public ServerEntityManager() : base() { }

        // Adds a ServerConfigs instance to the manager
        public void AddServerConfig(ServerConfigs serverConfig)
        {
            // Ensure only one instance of each server type is added
            AddModel(serverConfig);
        }

        // Adds a ServerClients instance to the manager
        public void AddServerClients(ServerClients serverClients)
        {
            // Ensure only one instance of each server type is added
            AddModel(serverClients);
        }

        // Retrieves a server of a specific type
        public T GetServerProperty<T>() where T : ServerCoreEntity
        {
            return GetModel<T>();
        }


    }
}
