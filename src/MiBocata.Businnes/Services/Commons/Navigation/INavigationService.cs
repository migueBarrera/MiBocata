using Mibocata.Core.Framework;

namespace MiBocata.Businnes.Services.Commons.Navigation
{
    public interface INavigationService
    {
        Task NavigateToAsync<TViewModel>(bool clearStack = false)
            where TViewModel : CoreViewModel;

        Task NavigateToAsync<TViewModel>(object parameter, bool clearStack = false)
            where TViewModel : CoreViewModel;

        Task NavigateBackAsync();

        Task RemoveAll();

        Task NavigateToPopupAsync<TViewModel>(bool animate)
            where TViewModel : CoreViewModel;

        Task NavigateToPopupAsync<TViewModel>(object parameter, bool animate)
            where TViewModel : CoreViewModel;
    }
}
