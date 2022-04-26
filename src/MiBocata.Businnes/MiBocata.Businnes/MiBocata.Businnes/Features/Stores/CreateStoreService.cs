﻿using System.Threading.Tasks;
using MiBocata.Businnes.Services.Commons.Navigation;
using Mibocata.Core.Features.Stores;
using Mibocata.Core.Services.Interfaces;
using Models.Core;
using Models.Requests;
using Models.Responses;
using System.Linq;
using Mibocata.Core.Features.Refit;

namespace MiBocata.Businnes.Features.Stores
{
    public class CreateStoreService : ICreateStoreService
    {
        private readonly IStoreApi storeApi;
        private readonly IMiBocataNavigationService miBocataNavigationService;
        private readonly IPreferencesService preferencesService;
        private readonly ISessionService sessionService;
        private readonly ILoggingService loggingService;
        private readonly ITaskHelperFactory taskHelperFactory;

        public CreateStoreService(
            IRefitService refitService,
            IMiBocataNavigationService miBocataNavigationService,
            IPreferencesService preferencesService,
            ISessionService sessionService,
            ILoggingService loggingService,
            ITaskHelperFactory taskHelperFactory)
        {
            this.storeApi = refitService.InitRefitInstance<IStoreApi>(isAutenticated: true);
            this.miBocataNavigationService = miBocataNavigationService;
            this.preferencesService = preferencesService;
            this.sessionService = sessionService;
            this.loggingService = loggingService;
            this.taskHelperFactory = taskHelperFactory;
        }

        public async Task CreateStore(Store store)
        {
            var result = await taskHelperFactory.
                                    CreateInternetAccessViewModelInstance(loggingService/*, this*/).
                                    TryExecuteAsync(() => storeApi.Create(new StoreCreateRequest()
                                    {
                                        AutoAccept = store.AutoAccept,
                                        Name = store.Name,
                                        StoreLocation = StoreLocationRequest.Parse(store.StoreLocation),
                                    }));

            if (result)
            {
                var storeResponse = result.Value;
                await OnCreateStoreSuccessful(new Store()
                {
                    Id = storeResponse.Id,
                    Name = storeResponse.Name,
                    Products = storeResponse.Products?.Select(p => ProductsResponse.Parse(p)).ToList(),
                    AutoAccept = storeResponse.AutoAccept,
                    Image = storeResponse.Image,
                    StoreLocation = storeResponse.StoreLocation != null 
                        ? StoreLocationResponse.Parse(storeResponse.StoreLocation) 
                        : null,
                });
            }
        }

        private async Task OnCreateStoreSuccessful(Store storeResult)
        {
            sessionService.Clear();
            preferencesService.SetStore(storeResult);

            await miBocataNavigationService.NavigateToHome();
        }
    }
}