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
        protected delegate Task<bool> ConnectionStep();


        /// <summary>
        /// Abstract property representing the steps involved in the connection process.
        /// Derived classes must provide an implementation, detailing the specific steps.
        /// </summary>
        protected abstract ConnectionStep[] ConnectionSteps { get; }

        /// <summary>
        /// Initiates the connection process by sequentially executing each defined connection step.
        /// Updates the client model's connection status based on the outcome of the steps.
        /// </summary>
        public async Task ConnectAsync()
        {
            foreach (var step in ConnectionSteps)
            {
                if (!await step()) // Await the async operation and check the result
                {
                    return;
                }
            }
        }

        public abstract void ApplyConfig(BaseConfig config);
    }
}

