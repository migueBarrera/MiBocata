using Mibocata.Core.Features.Refit;
using Mibocata.Core.Features.Stores;
using Mibocata.Core.Services.Interfaces;
using Models.Core;
using Models.Requests;
using Models.Responses;

namespace MiBocata.Businnes.Features.Stores;

public class CreateStoreService : ICreateStoreService
{
    private readonly IStoreApi storeApi;
    private readonly IPreferencesService preferencesService;
    private readonly ISessionService sessionService;
    private readonly ILoggingService loggingService;
    private readonly ITaskHelperFactory taskHelperFactory;

    public CreateStoreService(
        IRefitService refitService,
        IPreferencesService preferencesService,
        ISessionService sessionService,
        ILoggingService loggingService,
        ITaskHelperFactory taskHelperFactory)
    {
        this.storeApi = refitService.InitRefitInstance<IStoreApi>(isAutenticated: true);
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

        if (result.IsSuccess)
        {
            var storeResponse = result.Value;
            await OnCreateStoreSuccessful(new Store()
            {
                Id = storeResponse.Id,
                Name = storeResponse.Name,
                Products = storeResponse.Products != null
                    ? storeResponse.Products?.Select(p => ProductsResponse.Parse(p)).ToList()
                    : new List<Product>(),
                AutoAccept = storeResponse.AutoAccept,
                Image = storeResponse.Image,
                StoreLocation = storeResponse.StoreLocation != null 
                    ? StoreLocationResponse.Parse(storeResponse.StoreLocation) 
                    : null,
            });
        }
    }

    private Task OnCreateStoreSuccessful(Store storeResult)
    {
        sessionService.Clear();
        preferencesService.SetStore(storeResult);

        //TODO await miBocataNavigationService.NavigateToHome();
        return Task.CompletedTask;
    }
}
