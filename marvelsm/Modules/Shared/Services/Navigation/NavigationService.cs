using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using marvelsm.Modules.Dashboard.ViewModels;
using marvelsm.Modules.Dashboard.Views;
using marvelsm.Modules.Shared.ViewModels;
using Xamarin.Forms;

namespace marvelsm.Modules.Shared.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        protected readonly Dictionary<Type, Type> _mappings;

        protected Application CurrentApplication
        {
            get { return Application.Current; }
        }

        public NavigationService()
        {
            _mappings = new Dictionary<Type, Type>();
            CreatePageViewModelMappings();
        }

        public Task InitializeAsync()
            => NavigateToAsync<DashboardViewModel>();

        public Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
            => InternalNavigateToAsync(typeof(TViewModel), null);

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel
            => InternalNavigateToAsync(typeof(TViewModel), parameter);

        public Task NavigateToAsync(Type viewModelType)
            => InternalNavigateToAsync(viewModelType, null);

        public Task NavigateToAsync(Type viewModelType, object parameter)
            => InternalNavigateToAsync(viewModelType, parameter);

        public void NavPage(Page page)
            => Application.Current.MainPage = new NavigationPage(page);

        public void NavAsyncPage(Page page)
            => Device.BeginInvokeOnMainThread(async () =>
            {
                await Application.Current.MainPage.Navigation.PushAsync(page);
            });

        private static NavigationService instance = null;
        private static readonly object padlock = new object();

        public static NavigationService Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new NavigationService();

                    return instance;
                }
            }
        }

        public virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreateAndBindPage(viewModelType, parameter);

            if (page is DashboardView)
                CurrentApplication.MainPage = page;
            else
            {
                var nav = CurrentApplication.MainPage as NavigationPage;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await nav.PushAsync(page);
                });
            }
            await (page.BindingContext as BaseViewModel).InitializeAsync(parameter);
        }

        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!_mappings.ContainsKey(viewModelType))
                throw new KeyNotFoundException($"não existe mapeamento para ${viewModelType} por isso a navegação não está funcionando, mapeie a view model no método CreatePageViewModelMappings");

            return _mappings[viewModelType];
        }

        public Page CreateAndBindPage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
                throw new Exception($"A view model {viewModelType} não esta mapeado para uma page");

            Page page = Activator.CreateInstance(pageType) as Page;
            BaseViewModel viewModel = LocatorViewModel.Instance.Resolve(viewModelType) as BaseViewModel;
            page.BindingContext = viewModel;
            return page;
        }

        public void CreatePageViewModelMappings()
        {
           _mappings.Add(typeof(DashboardViewModel), typeof(DashboardView));
        }
    }
}