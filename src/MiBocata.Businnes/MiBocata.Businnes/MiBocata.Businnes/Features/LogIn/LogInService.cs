using System.Linq;
using System.Threading.Tasks;
using MiBocata.Businnes.Services.Commons.Navigation;
using Mibocata.Core.Features.Auth;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Features.Stores;
using Mibocata.Core.Services.Interfaces;
using Models.Core;
using Models.Responses;

namespace MiBocata.Businnes.Features.LogIn
{
    public class LogInService : ILogInService
    {
        private readonly IAuthApi authApi;
        private readonly IStoreApi storeApi;
        private readonly IMiBocataNavigationService miBocataNavigationService;
        private readonly IPreferencesService preferencesService;
        private readonly ILoggingService loggingService;
        private readonly ITaskHelperFactory taskHelperFactory;

        public LogInService(
            IMiBocataNavigationService miBocataNavigationService,
            IPreferencesService preferencesService,
            ILoggingService loggingService,
            ITaskHelperFactory taskHelperFactory,
            IRefitService refitService)
        {
            this.authApi = refitService.InitRefitInstance<IAuthApi>();
            this.storeApi = refitService.InitRefitInstance<IStoreApi>(isAutenticated: true);
            this.miBocataNavigationService = miBocataNavigationService;
            this.preferencesService = preferencesService;
            this.loggingService = loggingService;
            this.taskHelperFactory = taskHelperFactory;
        }

        public async Task DoLoginAsync(string email, string pass)
        {
            var result = await taskHelperFactory.
               CreateInternetAccessViewModelInstance(loggingService/*, this*/).
               TryExecuteAsync(
               () => authApi.SignIn(new Models.Requests.ShopkeeperSignInRequest()
               {
                   Email = email,
                   Password = pass,
               }));

            if (result)
            {
                await LogInSuccessful(new Shopkeeper()
                {
                    Email = result.Value.Email,
                    Name = result.Value.Name,
                    Id = result.Value.Id,
                    IdStore = result.Value.IdStore,
                    Token = result.Value.Token,
                });
            }
        }

        private async Task LogInSuccessful(Shopkeeper shopkeeper)
        {
            preferencesService.SetShopkeeper(shopkeeper);

            ////var pushToken = preferencesService.PushToken();

            if (shopkeeper.IdStore != 0)
            {
                var store = await storeApi.Get(shopkeeper.IdStore);
                preferencesService.SetStore(new Store()
                {
                    Id = store.Id,
                    Name = store.Name,
                    Image = store.Image,
                    AutoAccept = store.AutoAccept,
                    Products = store.Products?.Select(pr => ProductsResponse.Parse(pr)).ToList(),
                });
                ////store.PushToken = pushToken;
                await miBocataNavigationService.NavigateToHome();
            }
            else
            {
                await miBocataNavigationService.NavigateToChooseLocationStore();
            }
        }
    }
}
