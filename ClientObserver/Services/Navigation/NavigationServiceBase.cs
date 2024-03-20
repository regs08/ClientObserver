using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClientObserver.Models.Interfaces.Navigation;
using ClientObserver.Models.Interfaces.ViewModel;
using Microsoft.Maui.Controls;

namespace ClientObserver.Services.Navigation
{
    public abstract class NavigationServiceBase : INavigationService
    {
        protected abstract Dictionary<Type, Type> ViewModelToViewMapping { get; }

        public async Task NavigateAsync<TViewModel>(object parameters = null) where TViewModel : IViewModel
        {
            if (!ViewModelToViewMapping.TryGetValue(typeof(TViewModel), out var viewType))
            {
                throw new InvalidOperationException($"No view found for {typeof(TViewModel).Name}");
            }

            var routeName = GetRouteNameFromViewType(viewType);
            var route = routeName;

            // Assuming parameters is something that can be directly converted to query string
            if (parameters != null)
            {
                var queryString = SerializeParameters(parameters);
                route += $"?{queryString}"; 
            }

            await Shell.Current.GoToAsync(route);
        }
        //"either the navigation serice isnt passing in the correct data or the view model is reading it wring
       // "Add/Remove servers, two way communication. finish rpi stuff" 

        private string GetRouteNameFromViewType(Type viewType)
        {
            // Here, we assume the route name is the same as the class name of the view
            // Adjust as necessary for your naming conventions
            return viewType.Name;
        }

        private string SerializeParameters(object parameters)
        {
            if (parameters is not Dictionary<string, object> paramDict) return "";

            return string.Join("&", paramDict.Select(kv => $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value.ToString())}"));
        }

    }
}
