using ClientObserver.Models.Servers;

namespace ClientObserver.Models.Servers
{

    public class ServerEntity
    {
        public string Name { get; set; }
        public ServerConfigs ServerConfigs { get; set; }
        public ServerClients ServerClients { get; set; }

        public ServerEntity(string name, ServerConfigs serverConfigs, ServerClients serverClients)
        {
            Name = name;
            ServerConfigs = serverConfigs;
            ServerClients = serverClients;
        }
    }
}

