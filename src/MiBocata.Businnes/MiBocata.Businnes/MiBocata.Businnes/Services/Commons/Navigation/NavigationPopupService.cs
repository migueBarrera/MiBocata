using Mibocata.Core.Framework;
using MiBocata.Businnes.Services.Commons.Navigation;
using System.Threading.Tasks;

namespace MiBocata.Businnes.Services.Navigation
{
    public partial class NavigationService : INavigationService
    {
        public Task NavigateToPopupAsync<TViewModel>(bool animate)
            where TViewModel : CoreViewModel
            => NavigateToPopupAsync<TViewModel>(null, animate);

        public Task NavigateToPopupAsync<TViewModel>(object parameter, bool animate)
            where TViewModel : CoreViewModel
        {
            // var page = CreateAndBindPage(typeof(TViewModel), parameter);
            // await (page.BindingContext as BaseViewModel).InitializeAsync(parameter);

            /* if (page is PopupPage)
             {
                 await PopupNavigation.Instance.PushAsync(page as PopupPage, animate);
             }
             else
             {
                 throw new ArgumentException($"The type ${typeof(TViewModel)} its not a PopupPage type");
             }*/

            return Task.CompletedTask;
        }
    }
}
