using System.Windows.Input;
using Mibocata.Core.Framework;
using Mibocata.Core.Services.Interfaces;
using MiBocata.Businnes.Features.Configuration;
using MiBocata.Businnes.Features.Products;
using Models.Core;

namespace MiBocata.Businnes.Features.Stores;

public class StoreViewModel : CoreViewModel
{
    private readonly IPreferencesService preferencesService;
    private Store store;

    public ICommand ConfigCommand { get; set; }

    public ICommand ProductsCommand { get; set; }

    public ICommand CloseSessionCommand { get; set; }

    public StoreViewModel(
         IPreferencesService preferencesService)
    {
        this.preferencesService = preferencesService;

        ConfigCommand = new AsyncCommand(() => ConfigCommandAsync());
        ProductsCommand = new AsyncCommand(() => ProductsCommandAsync());
        CloseSessionCommand = new AsyncCommand(() => CloseSessionCommandAsync());
    }

    public Store Store
    {
        get => store;
        set
        {
            SetAndRaisePropertyChanged(ref store, value);
        }
    }

    public override Task OnAppearingAsync()
    {
        Store = preferencesService.GetStore();
        return base.OnAppearingAsync();
    }

    private Task CloseSessionCommandAsync()
    {
        preferencesService.SetShopkeeper(null);
        preferencesService.SetStore(null);
        App.Current.MainPage = new AppShell();
        return Task.CompletedTask;
    }

    private async Task ConfigCommandAsync() => await Shell.Current.GoToAsync(nameof(ConfigurationPage));

    private async Task ProductsCommandAsync() => await Shell.Current.GoToAsync($"{nameof(ProductsPage)}");
}
