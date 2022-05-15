using MiBocata.Features.Cart;
using MiBocata.Features.LogIn;
using MiBocata.Features.Orders;
using MiBocata.Features.Profile;
using MiBocata.Features.Register;
using MiBocata.Features.Stores;

namespace MiBocata;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        //DependencyService.Resolve<ILoggingService>().Initialize();
        MainPage = new AppShell();

        Routing.RegisterRoute(nameof(LogInPage), typeof(LogInPage));
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        Routing.RegisterRoute(nameof(StoresPage), typeof(StoresPage));
        Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
        Routing.RegisterRoute(nameof(EditProfilePage), typeof(EditProfilePage));
        Routing.RegisterRoute(nameof(OrdersPage), typeof(OrdersPage));
        Routing.RegisterRoute(nameof(StoreDetailPage), typeof(StoreDetailPage));
        Routing.RegisterRoute(nameof(AddProductPage), typeof(AddProductPage));
        Routing.RegisterRoute(nameof(CartPage), typeof(CartPage));
    }
}
