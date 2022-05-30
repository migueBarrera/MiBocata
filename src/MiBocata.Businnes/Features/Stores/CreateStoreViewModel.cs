using System.Windows.Input;
using Mibocata.Core.Framework;
using Mibocata.Core.Services.Interfaces;
using Models.Core;

namespace MiBocata.Businnes.Features.Stores
{
    public class CreateStoreViewModel : CoreViewModel
    {
        private Store store;
        private string positionName;
        private readonly ICreateStoreService createStoreService;
        private readonly ISessionService sessionService;
        private readonly IGeocodingService geocodingService;
        private IEnumerable<Model> locations;

        public CreateStoreViewModel(
           ICreateStoreService createStoreService,
           ISessionService sessionService,
           IGeocodingService geocodingService)
        {
            this.createStoreService = createStoreService;
            this.sessionService = sessionService;
            this.geocodingService = geocodingService;

            NextCommand = new AsyncCommand(() => NextCommandAsync());
    }

        public Store Store { get => store; set => SetAndRaisePropertyChanged(ref store, value); }

        public string PositionName { get => positionName; set => SetAndRaisePropertyChanged(ref positionName, value); }

        public IEnumerable<Model> Locations { get => locations; set => SetAndRaisePropertyChanged(ref locations, value); }

        public ICommand NextCommand { get; set; }

        public override async Task OnAppearingAsync()
        {
            IsBusy = false;
            Store = sessionService.Get<Store>(nameof(Store));
            var placeMark = await geocodingService.GetPlaceMark(Store.StoreLocation.Latitude, Store.StoreLocation.Longitude);
            PositionName = $"{placeMark.CountryName}, {placeMark.AdminArea}, {placeMark.FeatureName}";
            //Temporarl unsuported map
            //Locations = new List<Model>()
            //{
            //    new Model()
            //    {
            //        Position = new Position(Store.StoreLocation.Latitude, Store.StoreLocation.Longitude),
            //        Description = "Tu tienda",
            //        Address = string.Empty,
            //    },
            //};
            //return base.OnAppearingAsync();
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
