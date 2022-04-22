using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Mibocata.Core.Services.Interfaces
{
    public interface IGeolocationService
    {
        Task<Location> GetLastKnownLocationAsync();
    }
}
