using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace marvelsm.Modules.Shared.ViewModels
{
    public class BaseViewModel : ViewModelBase
    {
        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
    }
}
