using ClientObserver.Models.Server.Framework.Clients;
using ClientObserver.Helpers.BaseClasses;
using ClientObserver.Models.Server.Framework.Configs;
using ClientObserver.Models.Server.Framework;

using ClientObserver.Models.Interfaces;
using ClientObserver.Models.Server.Core; 
using System;

namespace ClientObserver.Helpers.Server.Instance
{
    public class ServerFrameworkManager : AbstractModelManager<ServerFrameworkEntity>, IIdentifiableModel 
    {
        public string Name { get; set; }

        // Adds a ServerConfigs instance to the manager
        public void AddServerConfig(ServerConfigs serverConfig)
        {
            if (CheckNameOfFrameworkEntity(serverConfig))
            {
                // Ensure only one instance of each server type is added
                AddModel(serverConfig);
            }
        }

        // Adds a ServerClients instance to the manager
        public void AddServerClients(ServerClients serverClients)
        {
            if (CheckNameOfFrameworkEntity(serverClients))
            {
                // Ensure only one instance of each server type is added
                AddModel(serverClients);
            }

        }

        // Retrieves a server of a specific type
        public T GetServerProperty<T>() where T : ServerFrameworkEntity
        {
            return GetModel<T>();
        }
        private bool CheckNameOfFrameworkEntity(ServerFrameworkEntity serverFrameworkEntity)
        {
            if (Name == serverFrameworkEntity.Name)
            {
                return true;
            }
            throw new InvalidOperationException($"Name of {serverFrameworkEntity.GetType()}," +
                $" ({serverFrameworkEntity.Name})," +
                $" Must match {Name} of server entity!");
            
        }


    }
}
