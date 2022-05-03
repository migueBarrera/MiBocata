using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using MiBocata.Businnes.Framework;
using Mibocata.Core.Framework;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;
using Xamarin.CommunityToolkit.ObjectModel;

namespace MiBocata.Businnes.Features.Stores
{
    public class ChooseLocationViewModel : CoreViewModel
    {
        private readonly IChooseLocationService chooseLocationService;
        private Location userLocation;
        private IEnumerable<Model> locations;

        public ChooseLocationViewModel(
             IChooseLocationService chooseLocationService)
        {
            this.chooseLocationService = chooseLocationService;
        }

        public Location UserLocation { get => userLocation; set => SetAndRaisePropertyChanged(ref userLocation, value); }

        public IEnumerable<Model> Locations { get => locations; set => SetAndRaisePropertyChanged(ref locations, value); }

        public ICommand NextCommand => new AsyncCommand(() => NextCommandAsync());

        public override async Task InitializeAsync(object navigationData)
        {
            UserLocation = await chooseLocationService.GetLastKnownLocationAsync();

            await base.InitializeAsync(navigationData);
        }

        private async Task NextCommandAsync()
        {
            await chooseLocationService.GetPlaceMarkAndContinue(Locations);
        }

        public void UserSelectedPosition(Position position)
        {
            Locations = new List<Model>()
            {
                new Model()
                {
                    Position = new Position(position.Latitude, position.Longitude),
                    Description = "Tu tienda",
                    Address = string.Empty,
                },
            };
        }
    }
}
