using Mibocata.Core.Features.Auth;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Framework;
using Mibocata.Core.Services.Interfaces;
using MiBocata.Businnes.Features.Stores;
using Models.Core;

namespace MiBocata.Businnes.Features.Registro;

internal class RegisterService : IRegisterService
{
    private readonly IAuthApi authApi;
    private readonly IPreferencesService preferencesService;
    private readonly ILoggingService loggingService;
    private readonly ITaskHelperFactory taskHelperFactory;

    public RegisterService(
        IPreferencesService preferencesService,
        ILoggingService loggingService,
        ITaskHelperFactory taskHelperFactory,
        IRefitService refitService)
    {
        this.authApi = refitService.InitRefitInstance<IAuthApi>();
        this.preferencesService = preferencesService;
        this.loggingService = loggingService;
        this.taskHelperFactory = taskHelperFactory;
    }

    public async Task RegisterCommandAsync(IBusyViewModel viewModel, Shopkeeper newShopkeeper)
    {
        var result = await taskHelperFactory.
           CreateInternetAccessViewModelInstance(loggingService, viewModel).
           TryExecuteAsync(
           () => authApi.SignUp(new Models.Requests.ShopkeeperSignUpRequest()
           {
               Email = newShopkeeper.Email,
               Password = newShopkeeper.Password,
               ////Name = newShopkeeper.Name,
           }));

        if (result)
        {
            await RegisterSuccessesful(new Shopkeeper()
            {
                Id = result.Value.Id,
                Email = result.Value.Email,
                Name = result.Value.Name,
                Token = result.Value.Token,
            });
        }
    }

    private async Task RegisterSuccessesful(Shopkeeper result)
    {
        preferencesService.SetShopkeeper(result);
        await Shell.Current.GoToAsync($"///{nameof(ChooseLocationPage)}");
    }
}
