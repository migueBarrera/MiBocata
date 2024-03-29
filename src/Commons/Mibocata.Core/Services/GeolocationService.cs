﻿using Mibocata.Core.Services.Interfaces;

namespace Mibocata.Core.Services;

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
