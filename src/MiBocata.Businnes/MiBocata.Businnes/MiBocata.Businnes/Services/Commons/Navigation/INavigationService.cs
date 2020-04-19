using MiBocata.Businnes.Framework;
using System;
using System.Threading.Tasks;

namespace MiBocata.Businnes.Services.Navigation
{
    public interface INavigationService
    {
        Task NavigateToAsync<TViewModel>(bool clearStack = false)
            where TViewModel : BaseViewModel;

        Task NavigateToAsync<TViewModel>(object parameter, bool clearStack = false)
            where TViewModel : BaseViewModel;

        Task NavigateBackAsync();

        Task RemoveAll();

        Task NavigateToPopupAsync<TViewModel>(bool animate)
            where TViewModel : BaseViewModel;

        Task NavigateToPopupAsync<TViewModel>(object parameter, bool animate)
            where TViewModel : BaseViewModel;
    }
}
