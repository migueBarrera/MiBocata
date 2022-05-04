using System.Threading.Tasks;

namespace Mibocata.Core.Services.Interfaces
{
    public interface IGeolocationService
    {
        Task<Location> GetLastKnownLocationAsync();
    }
}
