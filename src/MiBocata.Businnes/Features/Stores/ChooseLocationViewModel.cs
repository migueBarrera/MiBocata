using System.Windows.Input;
using Mibocata.Core.Framework;

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

            NextCommand = new AsyncCommand(() => NextCommandAsync());
        }

        public Location UserLocation { get => userLocation; set => SetAndRaisePropertyChanged(ref userLocation, value); }

        public IEnumerable<Model> Locations { get => locations; set => SetAndRaisePropertyChanged(ref locations, value); }

        public ICommand NextCommand {get; set;}

        public override async Task OnAppearingAsync()
        {
            await base.OnAppearingAsync();

            UserLocation = await chooseLocationService.GetLastKnownLocationAsync();
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
