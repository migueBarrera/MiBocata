using MiBocata.Framework;
using MiBocata.Services.LoggingService;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MiBocata.Services.NavigationService
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

        public Task NavigateToAsync<TViewModel>(bool cleanStack = false)
            where TViewModel : BaseViewModel
            => InternalNavigateToAsync<TViewModel>(typeof(TViewModel), null, cleanStack);

        public Task NavigateToAsync<TViewModel>(object parameter, bool cleanStack = false)
            where TViewModel : BaseViewModel
            => InternalNavigateToAsync<TViewModel>(typeof(TViewModel), parameter, cleanStack);

        public async Task NavigateBackAsync()
        {
            await CurrentApplication.MainPage.Navigation.PopAsync();
        }

        protected virtual async Task InternalNavigateToAsync<TViewModel>(Type viewModelType, object parameter, bool cleanStack = false)
        {
            try
            {
                var page = CreateAndBindPage<TViewModel>(viewModelType, parameter);

                var mainPage = CurrentApplication.MainPage as NavigationPage;
                if (mainPage == null || cleanStack)
                {
                    mainPage = new NavigationPage(page);
                }
                else
                {
                    await mainPage.PushAsync(page);
                }

                CurrentApplication.MainPage = mainPage;
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
            var viewModel = Locator.Resolve<TViewModel>()
                            as BaseViewModel;
            page.BindingContext = viewModel;

            return page;
        }
    }
}
