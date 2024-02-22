using ClientObserver.Models.Server.Core.Clients;
using ClientObserver.Models.Server.Core.Configs;
using ClientObserver.Models.Interfaces.Clients; 
namespace ClientObserver.Services.Server.Core.Clients
{
    // BaseClientService without the direct generic constraint to a model
    public abstract class BaseClientService : IClientService
    {
        protected IClient ClientModel { get; set; }
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

