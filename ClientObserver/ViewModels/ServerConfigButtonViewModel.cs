using System;
using System.Windows.Input;
using ClientObserver.Models;
using ClientObserver.Services;
// Used to dynamically add buttons to our main page. These buttons represent
// servers we can connect to 
namespace ClientObserver.ViewModels
{
    public class ServerConfigButtonViewModel
    {
        public string ServerName { get; set; }
        public ServiceManager ServerServices { get; private set;}
        public ICommand NavigateCommand { get; set; }

        public ServerConfigButtonViewModel(ConfigController config, ICommand navigateCommand)
        {
            ServerServices = new ServiceManager(config);
            ServerName = config.ServerName;
            NavigateCommand = navigateCommand;
        }
    }

}

