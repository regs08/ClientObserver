using ClientObserver.Models.Events.ObservableProperties;
using System.Threading.Tasks;

namespace ClientObserver.Models.Interfaces.Clients
{
    /// <summary>
    /// Defines the contract for client models, extending IIdentifiableModel with connection capabilities.
    /// This interface ensures that implementing clients can be uniquely identified, managed based on their name, and monitored for their connection status.
    /// </summary>
    public interface IClient : IIdentifiableModel
    {
        /// <summary>
        /// Gets an ObservableProperty of type bool representing the client's connection status.
        /// This property allows for the observation of the client's connection status changes, facilitating reactive UI updates or logic execution in response to these changes.
        /// </summary>
        ObservableProperty<bool> IsConnected { get; }

        /// <summary>
        /// Initiates a connection for the client. The specific implementation of this method should handle the logic required to establish the client's connection to its respective server or service.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation of connecting the client.</returns>
        Task Connect();
    }
}
