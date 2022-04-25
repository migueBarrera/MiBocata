using Models.Requests;
using Models.Responses;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mibocata.Infrastructure.Data.Models;

[Table("StoreLocations")]
public class StoreLocation
{
    public int Id { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public string CountryCode { get; set; }

    public string CountryName { get; set; }

    public string FeatureName { get; set; }

    public string PostalCode { get; set; }

    public string SubLocality { get; set; }

    public string Thoroughfare { get; set; }

    public string SubThoroughfare { get; set; }

    public string Locality { get; set; }

    public string AdminArea { get; set; }

    public string SubAdminArea { get; set; }

    public int StoreId { get; set; }

    public static StoreLocationResponse Parse(StoreLocation storeLocation)
    {
        return new StoreLocationResponse()
        {
            Id = storeLocation.Id,
            Latitude = storeLocation.Latitude,
            Longitude = storeLocation.Longitude,
            CountryCode = storeLocation.CountryCode,
            CountryName = storeLocation.CountryName,
            FeatureName = storeLocation.FeatureName,
            PostalCode = storeLocation.PostalCode,
            SubLocality = storeLocation.SubLocality,
            Thoroughfare = storeLocation.Thoroughfare,
            SubThoroughfare = storeLocation.SubThoroughfare,
            Locality = storeLocation.Locality,
            AdminArea = storeLocation.AdminArea,
            StoreId = storeLocation.StoreId,
            SubAdminArea = storeLocation.SubAdminArea,
        };
    }
    
    public static StoreLocation Parse(StoreLocationRequest request)
    {
        return new StoreLocation()
        {
            Id = request.Id,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            CountryCode = request.CountryCode,
            CountryName = request.CountryName,
            FeatureName = request.FeatureName,
            PostalCode = request.PostalCode,
            SubLocality = request.SubLocality,
            Thoroughfare = request.Thoroughfare,
            SubThoroughfare = request.SubThoroughfare,
            Locality = request.Locality,
            AdminArea = request.AdminArea,
            StoreId = request.StoreId,
            SubAdminArea = request.SubAdminArea,
        };
    }
}
