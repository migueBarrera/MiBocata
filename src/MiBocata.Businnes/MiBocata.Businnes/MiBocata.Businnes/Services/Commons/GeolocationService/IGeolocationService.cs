using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MiBocata.Businnes.Services.GeolocationService
{
    public interface IGeolocationService
    {
        Task<Location> GetLastKnownLocationAsync();
    }
}
