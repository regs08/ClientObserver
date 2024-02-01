
namespace ClientObserver.Models.Configs
{
    /// <summary>
    /// Represents the base class for all configuration types in the application.
    /// This abstract class provides common properties and functionality that can be inherited by specific configuration classes.
    /// </summary>
    public abstract class BaseConfig
    {
        /// <summary>
        /// Gets or sets the name of the configuration.
        /// </summary>
        public string Name { get; set; }

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
    }

}
