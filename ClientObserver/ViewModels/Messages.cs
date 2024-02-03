﻿using System;
using ClientObserver.Managers;
using ClientObserver.Configs;
namespace ClientObserver.ViewModels
{
    // Defines a message type for updating server configuration.
    // It carries a new instance of ServerConfig as payload.
    public record UpdateServerConfigMessage(ServerConfigs NewConfig);

    // Defines a message type intended to signal that the UI should be refreshed.
    // This message does not carry any additional data.
    public record RefreshUIMessage();
}
