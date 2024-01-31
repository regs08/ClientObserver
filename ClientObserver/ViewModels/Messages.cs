using System;
namespace ClientObserver.ViewModels
{
    public record UpdateServerConfigMessage(ServerConfig NewConfig);
    public record RefreshUIMessage();

}

