using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MiBocata.Businnes.Framework;
using MiBocata.Businnes.Services.Commons.Navigation;
using Mibocata.Core.Extensions;
using Mibocata.Core.Services.Interfaces;
using Xamarin.Forms;

namespace MiBocata.Businnes.Services.Navigation
{
    public partial class NavigationService : INavigationService
    {
        private readonly ILoggingService loggingService;
        
        public NavigationService(
            ILoggingService loggingService)
        {
            this.loggingService = loggingService;
        }

        protected Application CurrentApplication => Application.Current;

        public Task NavigateToAsync<TViewModel>(bool clearStack = false)
            where TViewModel : BaseViewModel
            => InternalNavigateToAsync<TViewModel>(typeof(TViewModel), null, clearStack);

        public Task NavigateToAsync<TViewModel>(object parameter, bool clearStack = false)
            where TViewModel : BaseViewModel
            => InternalNavigateToAsync<TViewModel>(typeof(TViewModel), parameter, clearStack);

        public async Task NavigateBackAsync()
        {
            await CurrentApplication.MainPage.Navigation.PopAsync();  
        }

        public virtual Task RemoveAll()
        {
            var existingPages = CurrentApplication.MainPage.Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                CurrentApplication.MainPage.Navigation.RemovePage(page);
            }

            return Task.FromResult(true);
        }

        protected virtual async Task InternalNavigateToAsync<TViewModel>(Type viewModelType, object parameter, bool clearStack = false)
        {
            try
            {
                var page = CreateAndBindPage<TViewModel>(viewModelType, parameter);

                var cp = CurrentApplication.MainPage as BaseNavigationPage;

                if (cp == null || clearStack)
                {
                    cp = new BaseNavigationPage(page);
                    CurrentApplication.MainPage = cp;
                }
                else
                {
                    await cp.PushAsync(page);
                }
            }
            catch (Exception e)
            {
                loggingService.Error(e);
                Debugger.Break();
            }
        }

        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            var pagename = viewModelType.Name.Replace("ViewModel", "Page");
            var @namespace = viewModelType.Namespace;
                                                     
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}.{1}, {2}", @namespace, pagename, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        protected Page CreateAndBindPage<TViewModel>(Type viewModelType, object parameter)
        {
            var pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            var page = Activator.CreateInstance(pageType) as Page;
            var viewModel = App.Current.DependencyService.Resolve<TViewModel>()
                            as BaseViewModel;
            page.BindingContext = viewModel;

            return page;
        }
    }
}
