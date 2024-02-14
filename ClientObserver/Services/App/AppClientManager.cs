using System;
using System.Collections.ObjectModel;
using System.Linq;
using ClientObserver.Models.Server.Managers;
using ClientObserver.Models.Servers;
// todo need to rewrite how this adds client models throwing an error when it searches for a key that doesnt exist 
namespace ClientObserver.Managers
{
    public class AppClientManager
    {
        // ObservableCollection to store client models
        public ObservableCollection<ServerClients> ServerClients { get; private set; }

        private static AppClientManager _instance;
        private static readonly object _lock = new object();

        public static AppClientManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new AppClientManager();
                        }
                    }
                }
                return _instance;
            }
        }

        private AppClientManager()
        {
            ServerClients = new ObservableCollection<ServerClients>();
        }

        // Method to get a client model by server name
        public ServerClients GetClientModel(string serverName)
        {
            var serverClient = ServerClients.FirstOrDefault(sc => sc.Name == serverName);
            if (serverClient != null)
            {
                return serverClient;
            }
            return null;
                //throw new KeyNotFoundException($"No clients found for server: {serverName}");
        }

        // Method to add a client model
        public void AddSeverClients(ServerClients serverClients)
        {
            var existingServerClient = GetClientModel(serverClients.Name);
            if (existingServerClient == null)
            {
                ServerClients.Add(serverClients);
            }
            else
            {
                // Optionally, update the existing entry or handle duplicates as needed
            }
        }

        // Method to remove a client model by server name
        public void RemoveClientModel(string serverName)
        {
            var serverClient = GetClientModel(serverName);
            if (serverClient != null)
            {
                ServerClients.Remove(serverClient);
            }
            else
            {
                // Handle the case where the server name does not exist
            }
        }
    }
}
