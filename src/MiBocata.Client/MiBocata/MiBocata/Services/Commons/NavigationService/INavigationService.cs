using Mibocata.Core.Framework;

namespace MiBocata.Services.NavigationService
{
    public interface INavigationService
    {
        Task NavigateToAsync<TViewModel>(bool cleanStack = false)
            where TViewModel : CoreViewModel;

        Task NavigateToAsync<TViewModel>(object parameter, bool cleanStack = false)
            where TViewModel : CoreViewModel;

        Task NavigateBackAsync();

        //Task NavigateToPopupAsync<TViewModel>(bool animate)
        //    where TViewModel : CoreViewModel;

        //Task NavigateToPopupAsync<TViewModel>(object parameter, bool animate)
        //    where TViewModel : CoreViewModel;
    }
}