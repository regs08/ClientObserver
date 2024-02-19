﻿using ClientObserver.Models.Server.Core.Configs;
using ClientObserver.Models.Interfaces;
namespace ClientObserver.Models.Interfaces.Clients
{
    // IClientModel interface
    public interface IClientModel : IIdentifiableModel
    {
        bool ConnectionStatus { get; set; }
        void Connect();
    }

}