﻿using System.Threading.Tasks;
using Mibocata.Core.Services.Interfaces;
using MiBocata.Businnes.Features.Home;
using MiBocata.Businnes.Features.LogIn;
using MiBocata.Businnes.Features.Registro;
using MiBocata.Businnes.Features.Stores;
using MiBocata.Businnes.Services.Commons.AuthenticationService;

namespace MiBocata.Businnes.Services.Commons.Navigation
{
    public class MiBocataNavigationService : IMiBocataNavigationService
    {
        protected readonly IAuthenticationService AuthenticationService;
        protected readonly IPreferencesService PreferencesService;
        protected readonly INavigationService NavigationService;

        public MiBocataNavigationService(
            IAuthenticationService authenticationService,
            INavigationService navigationService,
            IPreferencesService preferencesService)
        {
            AuthenticationService = authenticationService;
            NavigationService = navigationService;
            PreferencesService = preferencesService;
        }

        public async Task InitializeAsync()
        {
            var store = PreferencesService.GetStore();
            if (store != null)
            {
                await NavigationService.NavigateToAsync<HomeViewModel>();
            }
            else
            {
                await NavigationService.NavigateToAsync<LogInViewModel>();
            }
        }

        public async Task NavigateToChooseLocationStore()
        {
            await NavigationService.NavigateToAsync<ChooseLocationViewModel>(clearStack: true);
        }

        public async Task NavigateToCreateStore()
        {
            await NavigationService.NavigateToAsync<CreateStoreViewModel>();
        }

        public async Task NavigateToHome()
        {
            await NavigationService.NavigateToAsync<HomeViewModel>(clearStack: true);
        }

        public Task NavigateToLogIn()
        {
            return NavigationService.NavigateToAsync<LogInViewModel>(clearStack: true);
        }

        public async Task NavigateToRegister()
        {
            await NavigationService.NavigateToAsync<RegisterViewModel>();
        }
    }
}
