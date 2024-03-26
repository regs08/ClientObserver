using System;
using ClientObserver.Models.Interfaces.Connection;
using ClientObserver.Helpers.Exceptions;
namespace ClientObserver.Services.Core.Server.Core.Base
{
	public abstract class BaseConnectionService : IConnectionService
	{

        /// <summary>
        /// Property representing the steps involved in the connection process.
        /// </summary>
        public ConnectionStep[] ConnectionSteps
            {
                get
                {
                    return new ConnectionStep[]
                    {
                        InitializeConnection,
                        Authenticate,
                        FinalizeConnection
                    };
                }
            }

        /// <summary>
        /// Property representing the steps involved in the disconnection process.
        /// </summary>
        public DisconnectionStep[] DisconnectionSteps
        {
            get
            {
                return new DisconnectionStep[]
                {
                    DisconnectFromClient
                };
            }
        }
        /// <summary>
        /// Initiates the connection process by sequentially executing each defined connection step.
        /// Updates the client model's connection status based on the outcome of the steps.
        /// </summary>
        public async Task<bool> ConnectAsync()
        {
            foreach (var step in ConnectionSteps)
            {
                bool result = false;
                try
                {
                    result = await step();
                }
                catch (Exception ex) // Catch exceptions from the step execution
                {
                    // Optionally log the exception details here
                    Console.WriteLine($"Failed to execute connection step. Details: {ex.Message}");
                    return false;
                }

                if (!result)
                {
                    Console.WriteLine($"Failed to execute connection step");
                    return false;
                }
            }
            return true;

        }

        public async Task DisconnectAsync()
        {
            foreach (var step in DisconnectionSteps)
            {
                bool result = false;
                try
                {
                    result = await step();
                }
                catch (Exception ex) // Catch exceptions from the step execution
                {
                    // Optionally log the exception details here
                    throw new DisconnectionException($"Failed to execute disconnection step. Details: {ex.Message}");
                }

                if (!result)
                {
                    // In the case of disconnection failure, you might choose to simply log the error and continue,
                    // or throw an exception if failing to disconnect is critical.
                    // This decision depends on the application's requirements.
                    throw new DisconnectionException("Disconnection step failed without an exception.");
                }
            }
        }
        public abstract Task<bool> InitializeConnection();

        public abstract Task<bool> Authenticate();

        public abstract Task<bool> FinalizeConnection();

        public abstract Task<bool> DisconnectFromClient();
    
    }
	
}

