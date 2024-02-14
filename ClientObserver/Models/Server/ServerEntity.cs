using System;
using System.Collections.ObjectModel;
using System.Linq;
using ClientObserver.Helpers.Server.Entity;
using ClientObserver.Models.Interfaces;
using ClientObserver.Models.Server.Core;

namespace ClientObserver.Models.Servers
{
    public class ServerEntity : IIdentifiableModel
    {
        // Can only have type of Serverbase. Serverbase has two types. Model Managers can only add one of each type

        public string Name { get;  set; }
        private readonly ServerEntityManager serverEntityManager = new();
        public ObservableCollection<ServerCoreEntity> serverCoreEntities => serverEntityManager.Models; // where the seperate configs are stored 

    }
}