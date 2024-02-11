using ClientObserver.Configs;
using ClientObserver.Models.Interfaces;

namespace ClientObserver.Models.Clients
{
    public abstract class BaseClientModel : IIdentifiableModel
    {
        // Implement the Name property from IIdentifiableModel
        // Adjusted to have a protected set to allow modification within subclasses or assembly
        public string Name { get;  set; }

        public bool ConnectionStatus { get; set; } = false;
        public BaseConfig Config { get; protected set; }

        // Constructor to initialize Name, ConnectionStatus, and Config
        public BaseClientModel(BaseConfig config, string name = "BaseClientModel")
        {
            Name = name;
            Config = config;
        }

        // Method to assign properties from config
        public abstract void ApplyConfig();

        // Additional methods or properties as needed
    }
}
