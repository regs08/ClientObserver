using System;
using System.Collections.Generic;
using ClientObserver.Models.Clients;
using ClientObserver.Helpers.Server.Clients;
using System.Collections.ObjectModel;

namespace ClientObserver.Models.Servers
{
    public class ServerClients : ServerBase
    {
        private ClientModelManager clientModelManager = new ClientModelManager();
        public ObservableCollection<BaseClientModel> ClientModels => clientModelManager.Models;

        public ServerClients()
        {
        }

        // Method to add or update a client model. Only one instance per type is allowed.
        public void AddClientModel(BaseClientModel clientModel)
        {
            if (clientModel == null) throw new ArgumentNullException(nameof(clientModel));
            clientModelManager.AddModel(clientModel);
        }

        // Method to return non-null instances of BaseClientModel
        public IEnumerable<BaseClientModel> GetNonNullModels()
        {
            // Leverage the ObservableCollection directly for binding or convert it to a list for other purposes
            return clientModelManager.Models;
        }

        // Method to retrieve a specific client model by type
        public T GetClientModel<T>() where T : BaseClientModel
        {
            return clientModelManager.GetModel<T>();
        }

        // Method to remove a client model by type
        public bool RemoveClientModel<T>() where T : BaseClientModel
        {
            return clientModelManager.RemoveModel<T>();
        }
    }
}
