using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MiBocata.Businnes.Services.GeocodingService
{
    public interface IGeocodingService
    {
        Task<Placemark> GetPlaceMark(double latidude, double longitude);
    }
}
