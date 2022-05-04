using MiBocata.Features.Cart;
using MiBocata.Features.Home;
using MiBocata.Features.LogIn;
using MiBocata.Features.Orders;
using MiBocata.Features.Profile;
using MiBocata.Features.Register;
using MiBocata.Features.Stores;

namespace MiBocata.Services.NavigationService
{
    public class MiBocataNavigationService : IMiBocataNavigationService
    {
        // protected readonly IPreferencesService preferencesService;
        protected readonly INavigationService NavigationService;

        public MiBocataNavigationService(
            INavigationService navigationService)
        {
            this.NavigationService = navigationService;
        }

        public async Task InitializeAsync()
        {
            await NavigateToHome();
        }

        public async Task NavigateToHome()
        {
            await NavigationService.NavigateToAsync<HomeViewModel>(cleanStack: true);
        }

        public async Task NavigateToLogIn()
        {
            await NavigationService.NavigateToAsync<LogInViewModel>();
        }

        public Task NavigateToRegister()
        {
            return NavigationService.NavigateToAsync<RegisterViewModel>();
        }

        public async Task NavigateToStore()
        {
            await NavigationService.NavigateToAsync<StoreDetailViewModel>();
        }

        public Task NavigateToCart()
        {
            return NavigationService.NavigateToAsync<CartViewModel>();
        }

        public Task NavigateToEditProfile()
        {
            return NavigationService.NavigateToAsync<EditProfileViewModel>();
        }

        public Task NavigateToOrders()
        {
            return NavigationService.NavigateToAsync<OrdersViewModel>();
        }
    }
}