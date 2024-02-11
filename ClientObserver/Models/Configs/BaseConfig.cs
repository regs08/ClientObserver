using System.Text;
using Newtonsoft.Json;
using ClientObserver.Models.Interfaces;

namespace ClientObserver.Configs
{
    /// <summary>
    /// Represents the base class for all configuration types in the application.
    /// This abstract class provides common properties and functionality that can be inherited by specific configuration classes.
    /// </summary>
    public abstract class BaseConfig : IIdentifiableModel
    {
        /// <summary>
        /// Gets or sets the name of the configuration.
        /// </summary>
    
        public string Name { get; set; }

        protected BaseConfig(string name)
        {
            Name = name;
        }
        /// <summary>
        /// Provides a formatted display string of the configuration's details.
        /// </summary>
        public string FormattedDisplay => FormatForDisplay();

        /// <summary>
        /// Method to format the display string. Can be overridden in derived classes.
        /// </summary>
        protected virtual string FormatForDisplay()
        {
            return $"Configuration Name: {Name}";
        }
        /// <summary>
        /// Validates the configuration properties.
        /// This method uses the NullPropertyChecker to dynamically check for null or empty string properties.
        /// It can be overridden in derived classes to implement specific validation logic.
        /// </summary>
        /// <returns>
        /// A string representing the name of the first property found to be null or empty, or null if all properties are valid.
        /// </returns>
        public virtual string Validate()
        {
            // Todo implemtn logic for testing non strings 
            // Use NullPropertyChecker to validate properties. This method checks each property of the configuration class
            // and returns the name of the first property that is null or an empty string.
            return NullPropertyChecker.CheckForNullProperties(this);
        }

        public override string ToString()
        {
            return $"Configuration Name: {Name}";
        }


        //public string FormattedDisplay => FormatForDisplay();
    }
}


