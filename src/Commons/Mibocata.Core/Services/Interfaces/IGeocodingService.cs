using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Mibocata.Core.Services.Interfaces
{
    public interface IGeocodingService
    {
        Task<Placemark> GetPlaceMark(double latidude, double longitude);
    }
}
