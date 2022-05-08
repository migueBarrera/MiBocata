using System.Windows.Input;
using Mibocata.Core.Framework;
using Mibocata.Core.Services.Interfaces;
using Models.Core;

namespace MiBocata.Businnes.Features.Stores
{
    public class CreateStoreViewModel : CoreViewModel
    {
        private Store store;
        private readonly ICreateStoreService createStoreService;
        private readonly ISessionService sessionService;
        private IEnumerable<Model> locations;

        public CreateStoreViewModel(
           ICreateStoreService createStoreService,
           ISessionService sessionService)
        {
            this.createStoreService = createStoreService;
            this.sessionService = sessionService;
        }

        public Store Store { get => store; set => SetAndRaisePropertyChanged(ref store, value); }

        public IEnumerable<Model> Locations { get => locations; set => SetAndRaisePropertyChanged(ref locations, value); }

        public ICommand NextCommand => new AsyncCommand(() => NextCommandAsync());

        public override Task OnAppearingAsync()
        {
            IsBusy = false;
            Store = sessionService.Get<Store>(nameof(Store));
            Locations = new List<Model>()
            {
                new Model()
                {
                    Position = new Position(Store.StoreLocation.Latitude, Store.StoreLocation.Longitude),
                    Description = "Tu tienda",
                    Address = string.Empty,
                },
            };
            return base.OnAppearingAsync();
        }

        private async Task NextCommandAsync()
        {
            if (!Validate())
            {
                return;
            }

            await createStoreService.CreateStore(Store);
        }

        private bool Validate()
        {
            return true;
        }
    }
}
