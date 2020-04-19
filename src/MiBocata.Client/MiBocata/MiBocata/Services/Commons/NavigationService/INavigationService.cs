using MiBocata.Framework;
using System.Threading.Tasks;

namespace MiBocata.Services.NavigationService
{
    public interface INavigationService
    {
        Task NavigateToAsync<TViewModel>(bool cleanStack = false)
            where TViewModel : BaseViewModel;

        Task NavigateToAsync<TViewModel>(object parameter, bool cleanStack = false)
            where TViewModel : BaseViewModel;

        Task NavigateBackAsync();

        Task NavigateToPopupAsync<TViewModel>(bool animate)
            where TViewModel : BaseViewModel;

        Task NavigateToPopupAsync<TViewModel>(object parameter, bool animate)
            where TViewModel : BaseViewModel;
    }
}