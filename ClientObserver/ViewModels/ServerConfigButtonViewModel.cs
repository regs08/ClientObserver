using System;
using System.Windows.Input;
using ClientObserver.Models;
using ClientObserver.Services;

namespace ClientObserver.ViewModels
{
    public class ServerConfigButtonViewModel
    {
        public string ServerName { get; set; }
        public ServiceManager ServerServices { get; private set;}
        public ICommand NavigateCommand { get; set; }

        public ServerConfigButtonViewModel(ServerConfig config, ICommand navigateCommand)
        {
            ServerServices = new ServiceManager(config);
            ServerName = config.ServerName;
            NavigateCommand = navigateCommand;
        }
    }

}

