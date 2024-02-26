namespace ClientObserver.Models.Interfaces
{
    /// <summary>
    /// Defines a contract for models that are identifiable by a name. 
    /// This interface ensures that implementing models can be uniquely identified and referenced within the application.
    /// </summary>
    public interface IIdentifiableModel
    {
        /// <summary>
        /// Gets or sets the name of the model. This property serves as the unique identifier for the model.
        /// </summary>
        string Name { get; set; }
    }
}
