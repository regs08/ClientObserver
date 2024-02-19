using ClientObserver.Models.Server.Core.Clients;
using ClientObserver.Models.Server.Core.Configs;
using ClientObserver.Models.Interfaces.Clients; 
namespace ClientObserver.Services.Server.Core.Clients
{
    // BaseClientService without the direct generic constraint to a model
    public abstract class BaseClientService : IClientService
    {
        protected IClientModel ClientModel { get; set; }
        /// <summary>
        /// Delegate definition for a connection step. Must return a boolean indicating the success of the step.
        /// </summary>
        protected delegate bool ConnectionStep();


        /// <summary>
        /// Abstract property representing the steps involved in the connection process.
        /// Derived classes must provide an implementation, detailing the specific steps.
        /// </summary>
        protected abstract ConnectionStep[] ConnectionSteps { get; }

        /// <summary>
        /// Initiates the connection process by sequentially executing each defined connection step.
        /// Updates the client model's connection status based on the outcome of the steps.
        /// </summary>
        public void Connect()
        {
            foreach (var step in ConnectionSteps)
            {
                if (!step()) // If any step fails,
                {
                    ClientModel.ConnectionStatus = false; // indicate failure and exit the loop.
                    return;
                }
            }
            ClientModel.ConnectionStatus = true; // If all steps succeed, indicate success.
        }

        public abstract void ApplyConfig(BaseConfig config);
    }
}

/*
using ClientObserver.Models.Server.Core.Configs;
using ClientObserver.Models.Interfaces.Clients;
using ClientObserver.Models.Server.Core.Clients;

namespace ClientObserver.Services.Server.Core.Clients
{
    /// <summary>
    /// Abstract base class for client services, implementing the IClientService interface.
    /// Provides common functionality and enforces the implementation of connection logic.
    /// </summary>
    /// <typeparam name="TModel">The type of client model this service will manage, constrained to types that implement IClientModel.</typeparam>
    public abstract class BaseClientService
    {
        /// <summary>
        /// The name of the client service. Intended to provide an identifiable label for the service.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The client model associated with this service. Contains client-specific information and status.
        /// Protected set allows modification within derived classes.
        /// </summary>
        public IClientModel ClientModel;
        /// <summary>
        /// Delegate definition for a connection step. Must return a boolean indicating the success of the step.
        /// </summary>
        protected delegate bool ConnectionStep();

        /// <summary>
        /// Constructs a new instance of the BaseClientService class.
        /// </summary>
        /// <param name="name">The name of the service.</param>
        /// <param name="clientModel">The client model associated with this service.</param>
        protected BaseClientService(string name, IClientModel clientModel)
        {
            Name = name;
            ClientModel = clientModel;
        }

        /// <summary>
        /// Abstract property representing the steps involved in the connection process.
        /// Derived classes must provide an implementation, detailing the specific steps.
        /// </summary>
        protected abstract ConnectionStep[] ConnectionSteps { get; }


        /// <summary>
        /// Abstract method for applying a configuration to the client model.
        /// Derived classes must provide an implementation to specify how the configuration is applied.
        /// </summary>
        /// <param name="config">The configuration to apply to the client model.</param>
        public abstract void ApplyConfig(BaseConfig config);
    }
}
*/