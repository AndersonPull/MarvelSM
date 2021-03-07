using System;
using System.Threading.Tasks;
using marvelsm.Modules.Shared.Services.Navigation;
using marvelsm.Modules.Shared.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace marvelsm
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            InitNavigation();
        }

        public Task InitNavigation()
        {
            var navigationService = LocatorViewModel.Instance.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
