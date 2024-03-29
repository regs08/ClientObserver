﻿using System;
using ClientObserver.Models.Server.Framework.Configs;

namespace ClientObserver.Models.App.Messages
{
    // Defines a message type for updating server configuration.
    // It carries a new instance of ServerConfig as payload.
    public record UpdateSelectedServerConfigMessage(ServerConfigs NewConfig);
    public record UpdateAvailableServerConfigMessage(ServerConfigs NewConfig);

    // Defines a message type intended to signal that the UI should be refreshed.
    // This message does not carry any additional data.
    public record RefreshUIMessage();
}
