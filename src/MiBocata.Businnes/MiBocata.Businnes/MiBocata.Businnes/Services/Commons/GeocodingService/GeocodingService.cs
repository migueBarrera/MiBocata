using MiBocata.Businnes.Services.LoggingService;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MiBocata.Businnes.Services.GeocodingService
{
    public class GeocodingService : IGeocodingService
    {
        private readonly ILoggingService loggingService;

        public GeocodingService(ILoggingService loggingService)
        {
            this.loggingService = loggingService;
        }

        public async Task<Placemark> GetPlaceMark(double latidude, double longitude)
        {
            try
            {
                var placemarks = await Geocoding.GetPlacemarksAsync(latidude, longitude);

                var placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    var geocodeAddress =
                        $"AdminArea:       {placemark.AdminArea}\n" +
                        $"CountryCode:     {placemark.CountryCode}\n" +
                        $"CountryName:     {placemark.CountryName}\n" +
                        $"FeatureName:     {placemark.FeatureName}\n" +
                        $"Locality:        {placemark.Locality}\n" +
                        $"PostalCode:      {placemark.PostalCode}\n" +
                        $"SubAdminArea:    {placemark.SubAdminArea}\n" +
                        $"SubLocality:     {placemark.SubLocality}\n" +
                        $"SubThoroughfare: {placemark.SubThoroughfare}\n" +
                        $"Thoroughfare:    {placemark.Thoroughfare}\n";

                    loggingService.Debug(geocodeAddress);
                }

                return placemark;
            }
            catch (FeatureNotSupportedException ex)
            {
                loggingService.Error(ex);
                return null;
            }
            catch (Exception ex)
            {
                loggingService.Error(ex);
                return null;
            }
        }
    }
}
