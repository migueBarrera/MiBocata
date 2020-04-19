using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using MiBocata.Businnes.Framework;
using MiBocata.Businnes.Services.API.Interfaces;
using Xamarin.Forms.Maps;
using static MiBocata.Businnes.Features.Store.ChooseLocationViewModel;

namespace MiBocata.Businnes.Features.Store
{
    public class CreateStoreViewModel : BaseViewModel
    {
        private Models.Store store;
        private readonly IStoreApi storeApi;

        private IEnumerable<Model> locations;

        public CreateStoreViewModel()
        {
            this.storeApi = RefitService.InitRefitInstance<IStoreApi>(isAutenticated: true);
        }

        public Models.Store Store { get => store; set => SetAndRaisePropertyChanged(ref store, value); }

        public IEnumerable<Model> Locations { get => locations; set => SetAndRaisePropertyChanged(ref locations, value); }

        public ICommand NextCommand => new AsyncCommand(_ => NextCommandAsync());

        public override Task InitializeAsync(object navigationData)
        {
            IsBusy = false;
            Store = SessionService.Get<Models.Store>(nameof(Models.Store));
            Locations = new List<Model>()
            {
                new Model()
                {
                    Position = new Position(Store.StoreLocation.Latitude, Store.StoreLocation.Longitude),
                    Description = "Tu tienda",
                    Address = string.Empty,
                },
            };
            return base.InitializeAsync(navigationData);
        }

        private async Task NextCommandAsync()
        {
            if (!Validate())
            {
                return;
            }

            ////var pushToken = PreferencesService.PushToken();
            ////todo store.PushToken = pushToken;

            var result = await TaskHelperFactory.
                                    CreateInternetAccessViewModelInstance(LoggingService, this).
                                    TryExecuteAsync(() => storeApi.Create(store));

            if (result)
            {
                await OnCreateStoreSuccessful(result.Value);
            }
        }

        private async Task OnCreateStoreSuccessful(Models.Store storeResult)
        {
            SessionService.Clear();
            PreferencesService.SetStore(storeResult);

            await MiBocataNavigationService.NavigateToHome();
        }

        private bool Validate()
        {
            return true;
        }
    }
}
