using System;
using ClientObserver.Models.Interfaces.ViewModel;

namespace ClientObserver.Models.Interfaces.Navigation
{
    public interface INavigationService
    {
        Task NavigateAsync<TViewModel>(object parameters = null) where TViewModel : IViewModel;
    }
}

