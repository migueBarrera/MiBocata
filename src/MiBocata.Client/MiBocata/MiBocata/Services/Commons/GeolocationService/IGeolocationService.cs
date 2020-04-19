using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MiBocata.Services.GeolocationService
{
    public interface IGeolocationService
    {
        Task<Location> GetLastKnownLocationAsync();
    }
}
