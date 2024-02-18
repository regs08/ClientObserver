using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ClientObserver.Models.Interfaces;

namespace ClientObserver.Helpers.BaseClasses
{
    /// <summary>
    /// Abstract class to manage a collection of NamedEntity objects or its subclasses by their unique names.
    /// </summary>
    public abstract class AbstractNamedEntityManager<T> : IEnumerable<T> where T : IIdentifiableModel
    {
        // Collection to hold all named entities managed by this class.
        protected readonly ObservableCollection<T> _entities = new ObservableCollection<T>();

        // Dictionary to quickly find named entities by their Name.
        protected readonly Dictionary<string, T> _nameIndex = new Dictionary<string, T>();

        /// <summary>
        /// Exposes the named entities managed by this class.
        /// </summary>
        public ObservableCollection<T> Entities => _entities;

        /// <summary>
        /// Adds a named entity to the manager. If an entity with the same name already exists, it can optionally handle updating logic.
        /// </summary>
        /// <param name="entity">The named entity to add.</param>
        public virtual void AddEntity(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (string.IsNullOrWhiteSpace(entity.Name)) throw new ArgumentException("Entity must have a name.", nameof(entity));

            if (!_nameIndex.ContainsKey(entity.Name))
            {
                _entities.Add(entity);
                _nameIndex[entity.Name] = entity;
            }
            else
            {
                // Optionally, handle logic for updating an existing named entity.
                // This part can be customized in subclasses to update entities rather than adding new ones.
            }
        }

        /// <summary>
        /// Retrieves a named entity by its name.
        /// </summary>
        /// <param name="name">The name of the entity to retrieve.</param>
        /// <returns>The named entity if found; otherwise, null.</returns>
        public virtual T GetEntityByName(string name)
        {
            _nameIndex.TryGetValue(name, out var entity);
            return entity;
        }

        /// <summary>
        /// Removes a named entity by its name from the manager.
        /// </summary>
        /// <param name="name">The name of the entity to remove.</param>
        /// <returns>True if the entity was successfully removed; otherwise, false.</returns>
        public virtual bool RemoveEntityByName(string name)
        {
            if (_nameIndex.ContainsKey(name))
            {
                var entity = _nameIndex[name];
                _nameIndex.Remove(name);
                return _entities.Remove(entity);
            }
            return false;
        }
        /// <summary>
        /// Returns an enumerator that iterates through the Entities collection.
        /// </summary>
        /// <returns>An enumerator for the Entities collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _entities.GetEnumerator();
        }

        /// <summary>
        /// Explicit implementation of the non-generic GetEnumerator method.
        /// This method is required because IEnumerable<T> inherits from IEnumerable.
        /// </summary>
        /// <returns>An enumerator for the Entities collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
