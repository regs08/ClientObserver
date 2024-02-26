using ClientObserver.Models.Events.ObservableProperties;

namespace ClientObserver.Models.Interfaces.Clients
{
    // IClientModel interface
    public interface IClient : IIdentifiableModel
    {
        public ObservableProperty<bool> IsConnected { get; } 
        Task Connect();
    }

}