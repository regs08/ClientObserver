using System;
using System.Collections.ObjectModel;
using System.Linq;
using ClientObserver.Models.Server.Framework.Clients;
using ClientObserver.Helpers.App.Clients;


namespace ClientObserver.Services.App.Core.Clients
{
    public class AppClientService
    {
        private AppClientManager appClientManager = new();

        // ObservableCollection to store client models
        public ObservableCollection<ServerClients> ServerClients => appClientManager.Entities; 


        // Method to get a client model by server name
        public ServerClients GetClientModel(string serverName)
        {
            return appClientManager.GetEntityByName(serverName);
   
        }

        // Method to add a client model
        public void AddSeverClients(ServerClients serverClients)
        {
            appClientManager.AddEntity(serverClients);
        }

        // Method to remove a client model by server name
        public void RemoveClientModel(string serverName)
        {
            appClientManager.RemoveEntityByName(serverName);
        }
    }
}
