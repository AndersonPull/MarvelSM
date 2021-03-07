using System;
using marvelsm.Modules.Dashboard.ViewModels;
using marvelsm.Modules.Shared.Services.Navigation;
using Unity;

namespace marvelsm.Modules.Shared.ViewModels
{
    public class LocatorViewModel
    {

        private readonly IUnityContainer _container;

        private static readonly LocatorViewModel _instance = new LocatorViewModel();

        public static LocatorViewModel Instance
        {
            get { return _instance; }
        }
        public LocatorViewModel()
        {
            _container = new UnityContainer();

            //Registro de Interfaces
            _container.RegisterType<INavigationService, NavigationService>();

            //registro de ViewModel
            _container.RegisterType<DashboardViewModel>();

        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }
    }
}
