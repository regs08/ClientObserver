using System;
using System.Collections.ObjectModel;
using System.Linq;
using ClientObserver.Helpers.Server.Instance;
using ClientObserver.Models.Interfaces;
using ClientObserver.Models.Server.Framework;
using ClientObserver.Models.Server.Framework.Clients;
using ClientObserver.Models.Server.Framework.Configs;
using ClientObserver.Models.Server.Core.Configs;
using ClientObserver.Models.Server.Core.Clients;


namespace ClientObserver.Models.Server.Instance
{
    public class ServerInstance : IIdentifiableModel
    {
        // Can only have type of ServerEntityManager. ServerEntityManager has two types. Model Managers can only add one of each type

        public string Name { get;  set; }
        public ServerClients ServerClients { get; set; } = new ServerClients();
        public ServerConfigs ServerConfigs { get; set; } = new ServerConfigs();
        private readonly ServerFrameworkManager serverFrameworkManager = new();
        public ObservableCollection<ServerFrameworkEntity> serverCoreEntities => serverFrameworkManager.Models; // where the seperate configs are stored 

        public ServerInstance(string name)
        {
            Name = name;
        }
        public void AddConfig(BaseConfig config)
        {
            ServerConfigs.AddConfigModel(config);
        }

        public void AddClient(BaseClientModel clientModel)
        {
            ServerClients.AddClientModel(clientModel);
        }

        // Adds a ServerConfigs instance to the manager
        public void AddServerConfig(ServerConfigs serverConfig)
        {
            if (CheckNameOfFrameworkEntity(serverConfig))
            {
                // Ensure only one instance of each server type is added
                serverFrameworkManager.AddModel(serverConfig);
            }
        }

        // Adds a ServerClients instance to the manager
        public void AddServerClients(ServerClients serverClients)
        {
            if (CheckNameOfFrameworkEntity(serverClients))
            {
                // Ensure only one instance of each server type is added
                serverFrameworkManager.AddModel(serverClients);
            }

        }

        // Retrieves a server of a specific type
        public T GetServerProperty<T>() where T : ServerFrameworkEntity
        {
            return serverFrameworkManager.GetModel<T>();
        }
        private bool CheckNameOfFrameworkEntity(ServerFrameworkEntity serverCoreEntity)
        {
            if (Name == serverCoreEntity.Name)
            {
                return true;
            }
            throw new InvalidOperationException($"Name of {serverCoreEntity.GetType()}," +
                $" ({serverCoreEntity.Name})," +
                $" Must match {Name} of server entity!");

        }
    }
}