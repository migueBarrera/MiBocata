namespace MiBocata.Services.NavigationService
{
    public interface IMiBocataNavigationService
    {
        Task InitializeAsync();

        Task NavigateToLogIn();

        Task NavigateToRegister();

        Task NavigateToEditProfile();

        Task NavigateToHome();

        Task NavigateToOrders();

        Task NavigateToStore();

        Task NavigateToCart();
    }
}
