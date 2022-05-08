namespace Models.Responses;

public class StoreLocationResponse
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

    public static StoreLocation Parse(StoreLocationResponse storeLocation)
    {
        return new StoreLocation()
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
            SubAdminArea = storeLocation.SubAdminArea,
            StoreId = storeLocation.StoreId,
        };
    }
}
