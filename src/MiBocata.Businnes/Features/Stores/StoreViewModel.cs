using System.Windows.Input;
using Mibocata.Core.Framework;
using Mibocata.Core.Services.Interfaces;
using Models.Core;

namespace MiBocata.Businnes.Features.Stores;

public class StoreViewModel : CoreViewModel
{
    private readonly IPreferencesService preferencesService;
    private Store store;

    public StoreViewModel(
         IPreferencesService preferencesService)
    {
        this.preferencesService = preferencesService;
    }

    public Store Store
    {
        get => store;
        set
        {
            SetAndRaisePropertyChanged(ref store, value);
        }
    }

    public ICommand ConfigCommand => new AsyncCommand(() => ConfigCommandAsync());

    public ICommand ProductsCommand => new AsyncCommand(() => ProductsCommandAsync());

    public ICommand CloseSessionCommand => new AsyncCommand(() => CloseSessionCommandAsync());

    public override Task OnAppearingAsync()
    {
        Store = preferencesService.GetStore();
        return base.OnAppearingAsync();
    }

    private async Task CloseSessionCommandAsync()
    {
        preferencesService.SetShopkeeper(null);
        preferencesService.SetStore(null);
        //TODO await navigationService.NavigateToAsync<LogInViewModel>(clearStack: true);
    }

    private async Task ConfigCommandAsync() => await Task.CompletedTask;// await navigationService.NavigateToAsync<ConfigurationViewModel>();

    private async Task ProductsCommandAsync() => await Task.CompletedTask;//await navigationService.NavigateToAsync<ProductsViewModel>();
}
