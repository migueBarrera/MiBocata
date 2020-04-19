using MiBocata.Businnes.Framework;
using MiBocata.Businnes.Services.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms.Maps;
using static MiBocata.Businnes.Features.Store.ChooseLocationViewModel;

namespace MiBocata.Businnes.Features.Configuration
{
    public class ConfigurationViewModel : BaseViewModel
    {
        private Models.Store store;

        private readonly IStoreApi storeApi;

        private IEnumerable<Model> locations;

        public ConfigurationViewModel()
        {
            storeApi = RefitService.InitRefitInstance<IStoreApi>(isAutenticated: true);
        }

        public Models.Store Store { get => store; set => SetAndRaisePropertyChanged(ref store, value); }

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
                                    TryExecuteAsync(() => storeApi.Update(store.Id, store));

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
