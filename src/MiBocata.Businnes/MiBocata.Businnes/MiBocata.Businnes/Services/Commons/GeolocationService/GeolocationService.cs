using MiBocata.Businnes.Services.LoggingService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MiBocata.Businnes.Services.GeolocationService
{
    public class GeolocationService : IGeolocationService
    {
        private readonly ILoggingService loggingService;

        public GeolocationService(ILoggingService loggingService)
        {
            this.loggingService = loggingService;
        }

        public async Task<Location> GetLastKnownLocationAsync()
        {
            Location location = null;
            try
            {
                location = await Geolocation.GetLastKnownLocationAsync();

                loggingService.Debug($"{nameof(GeolocationService)} : Location is successful");
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                loggingService.Error(fnsEx);
            }
            catch (PermissionException pEx)
            {
                loggingService.Error(pEx);
            }
            catch (Exception ex)
            {
                loggingService.Error(ex);
            }

            return location;
        }
    }
}
