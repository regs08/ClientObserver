using ClientObserver.Configs;

namespace ClientObserver.Models.Clients
{
    public abstract class BaseClientModel
    {
        // Make the setter of Name private
        public string Name { get; private set; }
        public bool ConnectionStatus { get; set; } = false;
        public BaseConfig Config { get; private set; }

        // Constructor to initialize Name, ConnectionStatus, and Config
        public BaseClientModel(BaseConfig config, string name = "BaseClientModel")
        {
            Name = name;
            Config = config;
        }

        // Method to assign properties from config 
        public abstract void ApplyConfig();
    }
}
