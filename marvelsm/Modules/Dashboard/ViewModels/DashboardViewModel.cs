using System;
using marvelsm.Modules.Shared.Services.Navigation;
using marvelsm.Modules.Shared.ViewModels;

namespace marvelsm.Modules.Dashboard.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        INavigationService _serviceNavigation;


        public DashboardViewModel(INavigationService serviceNavigation)
        {
            _serviceNavigation = serviceNavigation;
        }
    }
}
