using System;
using System.Threading.Tasks;
using ClientObserver.Models.Server.Core.Configs;

namespace ClientObserver.Models.Interfaces.Clients
{
    /// <summary>
    /// Defines a contract for client services that require connection and configuration functionalities.
    /// Implementing this interface ensures that a client service can connect to a server or network resource asynchronously and apply a given configuration.
    /// </summary>
    public interface IClientService
    {
        /// <summary>
        /// Applies a specified configuration to the client service.
        /// This method is responsible for configuring the client based on the provided <see cref="BaseConfig"/>.
        /// </summary>
        /// <param name="config">The configuration to apply to the client service.</param>
        void ApplyConfig(BaseConfig config);
        Task<bool> ConnectAsync();
        Task DisconnectAsync();

    }
}
