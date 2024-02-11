using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ClientObserver.Models.Interfaces;

namespace ClientObserver.Helpers.Server
{
    /// <summary>
    /// Abstract class to manage a collection of models that implement the IIdentifiableModel interface.
    /// </summary>
    /// <typeparam name="TModel">The type of model this manager handles, constrained to types implementing IIdentifiableModel.</typeparam>
    public abstract class AbstractModelManager<TModel> : IEnumerable<TModel> where TModel : IIdentifiableModel
    {
        // Collection to hold all models managed by this class.
        protected readonly ObservableCollection<TModel> _models = new ObservableCollection<TModel>();

        // Dictionary to quickly find models by their Type.
        protected readonly Dictionary<Type, TModel> _typeIndex = new Dictionary<Type, TModel>();

        /// <summary>
        /// Exposes the models managed by this class.
        /// </summary>
        public ObservableCollection<TModel> Models => _models;

        /// <summary>
        /// Adds a model to the manager. If the model's type already exists, it can optionally handle updating logic.
        /// </summary>
        /// <param name="model">The model to add.</param>
        public virtual void AddModel(TModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var modelType = model.GetType();
            if (!_typeIndex.ContainsKey(modelType))
            {
                _models.Add(model);
                _typeIndex[modelType] = model;
            }
            else
            {
                // Optionally, handle logic for updating an existing model.
                // This part can be customized in subclasses to update models rather than adding new ones.
            }
        }

        /// <summary>
        /// Retrieves a model of a specific type.
        /// </summary>
        /// <typeparam name="T">The type of model to retrieve, must be a class that implements TModel.</typeparam>
        /// <returns>The model if found; otherwise, null.</returns>
        public virtual T GetModel<T>() where T : class, TModel
        {
            _typeIndex.TryGetValue(typeof(T), out var model);
            return model as T;
        }

        /// <summary>
        /// Removes a model of a specific type from the manager.
        /// </summary>
        /// <typeparam name="T">The type of model to remove, must be a class that implements TModel.</typeparam>
        /// <returns>True if the model was successfully removed; otherwise, false.</returns>
        public virtual bool RemoveModel<T>() where T : class, TModel
        {
            var type = typeof(T);
            if (_typeIndex.ContainsKey(type))
            {
                var model = _typeIndex[type];
                _typeIndex.Remove(type);
                return _models.Remove(model);
            }
            return false;
        }
        /// <summary>
        /// Returns an enumerator that iterates through the Models collection.
        /// </summary>
        /// <returns>An enumerator for the Models collection.</returns>
        public IEnumerator<TModel> GetEnumerator()
        {
            return _models.GetEnumerator();
        }

        /// <summary>
        /// Explicit implementation of the non-generic GetEnumerator method.
        /// This method is required because IEnumerable<TModel> inherits from IEnumerable.
        /// </summary>
        /// <returns>An enumerator for the Models collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public bool CheckModels()
        {
            if (Models != null)
            {
                return true;
            }
            throw new InvalidOperationException("Models are empty !! ");
        }
    }
}
