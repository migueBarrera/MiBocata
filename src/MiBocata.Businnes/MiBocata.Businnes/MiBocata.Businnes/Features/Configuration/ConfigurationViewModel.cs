using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using MiBocata.Businnes.Framework;
using MiBocata.Businnes.Services.Commons.Navigation;
using MiBocata.Businnes.Services.Commons.Preferences;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Features.Stores;
using Mibocata.Core.Services.Interfaces;
using Models.Core;
using Xamarin.Forms.Maps;
using static MiBocata.Businnes.Features.Stores.ChooseLocationViewModel;

namespace MiBocata.Businnes.Features.Configuration
{
    public class ConfigurationViewModel : BaseViewModel
    {
        private Store store;

        private readonly IStoreApi storeApi;

        private IEnumerable<Model> locations;

        public ConfigurationViewModel(
            INavigationService navigationService,
            IMiBocataNavigationService miBocataNavigationService,
            IPreferencesService preferencesService,
            ISessionService sessionService,
            ILoggingService loggingService,
            IDialogService dialogService,
            IConnectivityService connectivityService,
            ITaskHelper taskHelper,
            IRefitService refitService,
            ITaskHelperFactory taskHelperFactory)
            : base(
                  navigationService,
                  miBocataNavigationService,
                  preferencesService,
                  sessionService,
                  loggingService,
                  dialogService,
                  connectivityService,
                  taskHelper,
                  refitService,
                  taskHelperFactory)
        {
            storeApi = RefitService.InitRefitInstance<IStoreApi>(isAutenticated: true);
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

            var result = await TaskHelperFactory.
                                    CreateInternetAccessViewModelInstance(LoggingService, this).
                                    TryExecuteAsync(() => storeApi.Update(store.Id, new Models.Requests.StoreUpdateRequest()
                                    {
                                        //store TODO
                                    }));

            if (result)
            {
                PreferencesService.SetStore(store);
                await NavigationService.NavigateBackAsync();
            }
        }

        private bool Validate()
        {
            ////todo
            return true;
        }

        public override Task InitializeAsync(object navigationData)
        {
            Store = PreferencesService.GetStore();
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
