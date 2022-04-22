using MiBocata.Businnes.Framework;
using MiBocata.Businnes.Services.Commons.Navigation;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;

namespace MiBocata.Businnes.Services.Navigation
{
    public partial class NavigationService : INavigationService
    {
        public Task NavigateToPopupAsync<TViewModel>(bool animate)
            where TViewModel : BaseViewModel
            => NavigateToPopupAsync<TViewModel>(null, animate);

        public Task NavigateToPopupAsync<TViewModel>(object parameter, bool animate)
            where TViewModel : BaseViewModel
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
