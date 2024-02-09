using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ClientObserver.Models.Clients;

namespace ClientObserver.Helpers.Server.Clients 
{
    public class ClientModelManager
    {
        private readonly ObservableCollection<BaseClientModel> _clientModels = new ObservableCollection<BaseClientModel>();
        private readonly Dictionary<Type, BaseClientModel> _typeIndex = new Dictionary<Type, BaseClientModel>();

        public ObservableCollection<BaseClientModel> ClientModels => _clientModels;

        public void AddClientModel(BaseClientModel clientModel)
        {
            if (clientModel == null) throw new ArgumentNullException(nameof(clientModel));

            var modelType = clientModel.GetType();
            if (_typeIndex.ContainsKey(modelType))
            {
                // Replace the existing model in the ObservableCollection
                var existingModel = _typeIndex[modelType];
                var index = _clientModels.IndexOf(existingModel);
                if (index != -1)
                {
                    _clientModels[index] = clientModel; // ObservableCollection does not have a direct replace method
                }
                else
                {
                    _clientModels.Add(clientModel);
                }
            }
            else
            {
                _clientModels.Add(clientModel);
            }
            // Update the type index either way
            _typeIndex[modelType] = clientModel;
        }

        public T GetClientModel<T>() where T : BaseClientModel
        {
            _typeIndex.TryGetValue(typeof(T), out var model);
            return model as T;
        }

        public bool RemoveClientModel<T>() where T : BaseClientModel
        {
            if (_typeIndex.TryGetValue(typeof(T), out var model))
            {
                _typeIndex.Remove(typeof(T));
                return _clientModels.Remove(model);
            }
            return false;
        }
    }
}
