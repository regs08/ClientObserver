using System;

namespace ClientObserver.Models.Events.ObservableProperties
{
    /// <summary>
    /// Represents a property that notifies observers of changes to its value. This class can be used to implement the observer pattern for properties of any type.
    /// </summary>
    /// <typeparam name="T">The type of the property value.</typeparam>
    public class ObservableProperty<T>
    {
        private T _value;

        /// <summary>
        /// Event triggered when the value of the property changes.
        /// </summary>
        public event EventHandler<T> ValueChanged;

        /// <summary>
        /// Gets or sets the value of the property. Setting a new value triggers the ValueChanged event if the new value is different from the old value.
        /// </summary>
        public T Value
        {
            get => _value;
            set
            {
                // Check if the value has actually changed to avoid unnecessary event triggers
                if (!Equals(_value, value))
                {
                    _value = value;
                    // Trigger the ValueChanged event, passing the new value
                    ValueChanged?.Invoke(this, _value);
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the ObservableProperty class with an optional initial value.
        /// </summary>
        /// <param name="initialValue">The initial value of the property. If not provided, defaults to the default value of type T.</param>
        public ObservableProperty(T initialValue = default)
        {
            _value = initialValue;
        }
    }
}
