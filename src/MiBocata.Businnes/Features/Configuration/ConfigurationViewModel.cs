using System.Windows.Input;
using MiBocata.Businnes.Features.Stores;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Features.Stores;
using Mibocata.Core.Framework;
using Mibocata.Core.Services.Interfaces;
using Models.Core;

namespace MiBocata.Businnes.Features.Configuration;

public class ConfigurationViewModel : CoreViewModel
{
    private Store store;

    private readonly IStoreApi storeApi;
    private readonly IPreferencesService preferencesService;
    private readonly ILoggingService loggingService;
    private readonly ITaskHelperFactory taskHelperFactory;
    private IEnumerable<Model> locations;

    public ConfigurationViewModel(
        IPreferencesService preferencesService,
        ILoggingService loggingService,
        IRefitService refitService,
        ITaskHelperFactory taskHelperFactory)
    {
        storeApi = refitService.InitRefitInstance<IStoreApi>(isAutenticated: true);
        this.preferencesService = preferencesService;
        this.loggingService = loggingService;
        this.taskHelperFactory = taskHelperFactory;

        SaveCommand = new AsyncCommand(SaveCommandExecute);
    }

    public Store Store { get => store; set => SetAndRaisePropertyChanged(ref store, value); }

    public IEnumerable<Model> Locations { get => locations; set => SetAndRaisePropertyChanged(ref locations, value); }

    public ICommand SaveCommand { get;set; }

    private async Task SaveCommandExecute()
    {
        if (IsBusy)
        {
            return;
        }

        if (!Validate())
        {
            return;
        }

        var result = await taskHelperFactory.
                                CreateInternetAccessViewModelInstance(loggingService, this).
                                TryExecuteAsync(() => storeApi.Update(store.Id, new Models.Requests.StoreUpdateRequest()
                                {
                                    //store TODO
                                }));

        if (result)
        {
            preferencesService.SetStore(store);
            //TODO await navigationService.NavigateBackAsync();
        }
    }

    private bool Validate()
    {
        ////todo
        return true;
    }

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();

        Store = preferencesService.GetStore();
        Locations = new List<Model>()
        {
            new Model()
            {
                Position = new Position(Store.StoreLocation.Latitude, Store.StoreLocation.Longitude),
                Description = Store.Name,
                Address = string.Empty,
            },
        };
    }
}
