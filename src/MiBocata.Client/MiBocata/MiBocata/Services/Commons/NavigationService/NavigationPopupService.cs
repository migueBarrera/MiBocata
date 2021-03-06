﻿using MiBocata.Framework;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiBocata.Services.NavigationService
{
    public partial class NavigationService : INavigationService
    {
        public Task NavigateToPopupAsync<TViewModel>(bool animate)
            where TViewModel : BaseViewModel
            => NavigateToPopupAsync<TViewModel>(typeof(TViewModel), animate);

        public async Task NavigateToPopupAsync<TViewModel>(object parameter, bool animate)
            where TViewModel : BaseViewModel
        {
            var page = CreateAndBindPopupPage<TViewModel>(typeof(TViewModel), parameter);

            await (page.BindingContext as BaseViewModel).InitializeAsync(parameter);

            if (page is PopupPage)
            {
                await PopupNavigation.Instance.PushAsync(page as PopupPage, animate);
            }
            else
            {
                throw new ArgumentException($"The type ${typeof(TViewModel)} its not a PopupPage type");
            }
        }

        protected PopupPage CreateAndBindPopupPage<TViewModel>(Type viewModelType, object parameter)
        {
            var pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            var page = Activator.CreateInstance(pageType) as PopupPage;
            var viewModel = Locator.Resolve<TViewModel>()
                            as BaseViewModel;
            page.BindingContext = viewModel;

            return page;
        }
    }
}
