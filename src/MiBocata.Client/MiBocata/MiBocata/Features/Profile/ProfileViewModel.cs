using MiBocata.Services.NavigationService;
using System.Windows.Input;

namespace MiBocata.Features.Profile;

public class ProfileViewModel : CoreViewModel
{
    private Models.Core.Client client;
    private bool userIsLogged;
    private readonly IMiBocataNavigationService navigationService;
    private readonly IPreferencesService preferencesService;

    public ProfileViewModel(
        IMiBocataNavigationService navigationService,
        IPreferencesService preferencesService)
    {
        this.navigationService = navigationService;
        this.preferencesService = preferencesService;
    }

    public bool UserIsLogged 
    { 
        get => userIsLogged; 
        set => SetAndRaisePropertyChanged(ref userIsLogged, value); 
    }

    public Models.Core.Client Client { get => client; set => SetAndRaisePropertyChanged(ref client, value); }

    public ICommand CloseSessionCommand => new AsyncCommand(() => CloseSessionCommandAsync());

    public ICommand OrdersCommand => new AsyncCommand(() => OrdersCommandAsync());

    public ICommand EditProfileCommand => new AsyncCommand(() => EditProfileCommandAsync());

    public override Task InitializeAsync(object navigationData)
    {
        Client = preferencesService.GetClient();

        UserIsLogged = Client != null;

        return base.InitializeAsync(navigationData);
    }

    private async Task CloseSessionCommandAsync()
    {
        preferencesService.SetClient(null);
        await navigationService.NavigateToHome();
    }

    private async Task OrdersCommandAsync()
    {
        await navigationService.NavigateToOrders();
    }

    private async Task EditProfileCommandAsync()
    {
        await navigationService.NavigateToEditProfile();
    }
}
