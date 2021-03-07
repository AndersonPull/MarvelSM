using System;
using System.Threading.Tasks;
using marvelsm.Modules.Shared.ViewModels;

namespace marvelsm.Modules.Shared.Services.Navigation
{
    public interface INavigationService
    {
        Task InitializeAsync();

        Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel;

        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel;

        Task NavigateToAsync(Type viewModelType);
    }
}
