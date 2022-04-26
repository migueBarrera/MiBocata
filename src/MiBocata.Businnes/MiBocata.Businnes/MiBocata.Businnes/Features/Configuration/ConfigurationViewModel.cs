using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using MiBocata.Businnes.Framework;
using MiBocata.Businnes.Services.Commons.Navigation;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Features.Stores;
using Mibocata.Core.Framework;
using Mibocata.Core.Services.Interfaces;
using Models.Core;
using Xamarin.Forms.Maps;
using static MiBocata.Businnes.Features.Stores.ChooseLocationViewModel;

namespace MiBocata.Businnes.Features.Configuration
{
    public class ConfigurationViewModel : CoreViewModel
    {
        private Store store;

        private readonly IStoreApi storeApi;
        private readonly INavigationService navigationService;
        private readonly IPreferencesService preferencesService;
        private readonly ILoggingService loggingService;
        private readonly ITaskHelperFactory taskHelperFactory;
        private IEnumerable<Model> locations;

        public ConfigurationViewModel(
            INavigationService navigationService,
            IPreferencesService preferencesService,
            ILoggingService loggingService,
            IRefitService refitService,
            ITaskHelperFactory taskHelperFactory)
        {
            storeApi = refitService.InitRefitInstance<IStoreApi>(isAutenticated: true);
            this.navigationService = navigationService;
            this.preferencesService = preferencesService;
            this.loggingService = loggingService;
            this.taskHelperFactory = taskHelperFactory;
        }

        public Store Store { get => store; set => SetAndRaisePropertyChanged(ref store, value); }

        public IEnumerable<Model> Locations { get => locations; set => SetAndRaisePropertyChanged(ref locations, value); }

        public ICommand SaveCommand => new AsyncCommand(SaveCommandExecute);

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
                await navigationService.NavigateBackAsync();
            }
        }

        private bool Validate()
        {
            ////todo
            return true;
        }

        public override Task InitializeAsync(object navigationData)
        {
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
            return base.InitializeAsync(navigationData);
        }
    }
}
