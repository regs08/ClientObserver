using System;
using ClientObserver.Models.Server.Core.Clients;
using ClientObserver.Models.Server.Core.Configs;
namespace ClientObserver.Services.Clients
{
    /// <summary>
    /// Provides an abstract base for client services, encapsulating common properties
    /// and the connection process logic.
    /// </summary>
    public abstract class BaseClientService<TConfig> where TConfig : BaseConfig
    {
        /// <summary>
        /// The name of the client service.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The client model associated with this service, containing client-specific information and status.
        /// </summary>
        public abstract BaseClientModel ClientModel { get; set; }

        /// <summary>
        /// Delegate defining a connection step that returns a boolean value, indicating success or failure.
        /// </summary>
        protected delegate bool ConnectionStep();

        /// <summary>
        /// Constructs a new instance of a client service with the specified name and client model.
        /// </summary>
        /// <param name="name">The name of the service.</param>
        /// <param name="clientModel">The client model to be used by the service.</param>
        protected BaseClientService(string name)
        {
            Name = name;
        }

        /// <summary>
        /// An abstract property that derived classes must implement to define the specific steps
        /// involved in the connection process.
        /// </summary>
        protected abstract ConnectionStep[] ConnectionSteps { get; }

        /// <summary>
        /// Initiates the connection process by executing each defined connection step in sequence.
        /// Updates the connection status of the client model based on the outcome of the steps.
        /// </summary>
        public void Connect()
        {
            foreach (var step in ConnectionSteps)
            {
                if (!step()) // If any step fails,
                {
                    ClientModel.ConnectionStatus = false; // indicate failure and exit.
                    return;
                }
            }
            ClientModel.ConnectionStatus = true; // If all steps succeed, indicate success.
        }
        public abstract void ApplyConfig(TConfig config);
    }
}
