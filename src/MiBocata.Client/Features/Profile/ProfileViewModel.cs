using MiBocata.Features.LogIn;
using MiBocata.Features.Orders;
using System.Windows.Input;

namespace MiBocata.Features.Profile;

public class ProfileViewModel : CoreViewModel
{
    private Models.Core.Client client;
    private bool userIsLogged;
    private readonly IPreferencesService preferencesService;
    public readonly LogInControlViewModel LoginControlViewModel;

    public ProfileViewModel(
        IPreferencesService preferencesService,
        LogInControlViewModel loginControlViewModel)
    {
        this.preferencesService = preferencesService;
        LoginControlViewModel = loginControlViewModel;
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

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();

        Client = preferencesService.GetClient();

        UserIsLogged = Client != null;
        await LoginControlViewModel.OnAppearingAsync();
    }

    private async Task CloseSessionCommandAsync()
    {
        preferencesService.SetClient(null);
        App.Current.MainPage = new AppShell();
    }

    private async Task OrdersCommandAsync()
    {
        //await navigationService.NavigateToOrders();
        await Shell.Current.GoToAsync($"{nameof(OrdersPage)}");
    }

    private async Task EditProfileCommandAsync()
    {
        //await navigationService.NavigateToEditProfile();
        await Shell.Current.GoToAsync($"{nameof(EditProfilePage)}");
    }
}
