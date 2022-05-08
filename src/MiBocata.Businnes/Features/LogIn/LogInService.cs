using Mibocata.Core.Features.Auth;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Features.Stores;
using Mibocata.Core.Services.Interfaces;
using Models.Core;
using Models.Responses;
using MiBocata.Features.LogIn;
using Mibocata.Core.Framework;

namespace MiBocata.Businnes.Features.LogIn;

public class LogInService : ILogInService
{
    private readonly IAuthApi authApi;
    private readonly IStoreApi storeApi;
    private readonly IPreferencesService preferencesService;
    private readonly ILoggingService loggingService;
    private readonly ITaskHelperFactory taskHelperFactory;

    public LogInService(
        IPreferencesService preferencesService,
        ILoggingService loggingService,
        ITaskHelperFactory taskHelperFactory,
        IRefitService refitService)
    {
        this.authApi = refitService.InitRefitInstance<IAuthApi>();
        this.storeApi = refitService.InitRefitInstance<IStoreApi>(isAutenticated: true);
        this.preferencesService = preferencesService;
        this.loggingService = loggingService;
        this.taskHelperFactory = taskHelperFactory;
    }

    public async Task DoLoginAsync(
        IBusyViewModel busyViewModel,
        string email, 
        string pass)
    {
        var result = await taskHelperFactory.
           CreateInternetAccessViewModelInstance(loggingService, busyViewModel).
           TryExecuteAsync(
           () => authApi.SignIn(new Models.Requests.ShopkeeperSignInRequest()
           {
               Email = email,
               Password = pass,
           }));

        if (result)
        {
            await LogInSuccessful(ShopkeeperSignInResponse.Parse(result.Value));
        }
    }

    private async Task LogInSuccessful(Shopkeeper shopkeeper)
    {
        preferencesService.SetShopkeeper(shopkeeper);

        if (shopkeeper.IdStore != 0)
        {
            var responseStore = await storeApi.Get(shopkeeper.IdStore);
            preferencesService.SetStore(StoreResponse.Parse(responseStore));
            App.Current.MainPage = new AppHomeShell();
        }
        else
        {
            //await miBocataNavigationService.NavigateToChooseLocationStore();
        }
    }
}
