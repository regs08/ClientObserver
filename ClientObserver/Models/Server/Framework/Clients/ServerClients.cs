using ClientObserver.Models.Server.Core.Clients;
using ClientObserver.Models.Server.Framework.Configs;
using ClientObserver.Models.Server.Core.Configs;
using ClientObserver.Helpers.Server.Framework.Clients;
using ClientObserver.Models.Interfaces.Clients;
using ClientObserver.Services.Server.Core.Clients;
using System.Collections.ObjectModel;

namespace ClientObserver.Models.Server.Framework.Clients 

{
    public class ServerClients : ServerFrameworkEntity
    {
        private ClientModelManager clientModelManager = new ClientModelManager();
        public ObservableCollection<BaseClientModel> ClientModels => clientModelManager.Models;

        public void GetClientsFromConfig(ServerConfigs configs)
        {
            foreach (BaseConfig config in configs.GetConfigs())
            {

                //'look here to go over how its adding.. as of now doesn't app
                BaseClientModel client = ClientFactory.CreateClientFromConfig(config);
                client.InitializeWithConfig();
                ClientModels.Add(client); 
                  
            }

        }
        // Method to add or update a client model. Only one instance per type is allowed.
        public void AddClientModel(BaseClientModel clientModel)
        {
            if (clientModel == null) throw new ArgumentNullException(nameof(clientModel));
            clientModelManager.AddModel(clientModel);
        }

        // Method to return non-null instances of BaseClientModel
        public IEnumerable<BaseClientModel> GetClientModels()
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
