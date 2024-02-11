using System;
using System.Collections.ObjectModel;
using System.Linq;
using ClientObserver.Helpers.Server.Entity;
namespace ClientObserver.Models.Servers
{
    public class ServerEntity
    {
        public string Name { get; private set; }
        public ObservableCollection<ServerBase> ServerBases { get; private set; }

        public ServerEntity(params ServerBase[] serverBases)
        {
            ServerBases = new ObservableCollection<ServerBase>(serverBases);
            SetName(serverBases); // Ensure all ServerBase objects have the same name
        }

        private void SetName(ServerBase[] serverBases)
        {
            /*
            // Check if all ServerBase objects have the same name
            var uniqueNames = serverBases.Select(sb => sb.ServerName).Distinct().ToList();
            if (uniqueNames.Count != 1)
            {
                throw new ArgumentException("All ServerBase objects must have the same name.");
            }

            // If they do, set the Name property to this common name
            Name = uniqueNames.First();
            */

        }
        public ServerBase GetServerObjects()
        {
            return null;
        }
    }
}
