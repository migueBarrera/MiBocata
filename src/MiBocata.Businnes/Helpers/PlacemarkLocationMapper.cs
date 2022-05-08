using Models.Core;

namespace MiBocata.Businnes.Helpers
{
    public class PlacemarkLocationMapper
    {
        public static StoreLocation Mapper(Placemark placemark, double latidude, double longitude)
        {
            var location = new StoreLocation()
            {
                Longitude = longitude,
                Latitude = latidude,
            };

            if (placemark != null)
            {
                location.FeatureName = !string.IsNullOrWhiteSpace(placemark.FeatureName) ? placemark.FeatureName : string.Empty;
                location.Locality = !string.IsNullOrWhiteSpace(placemark.Locality) ? placemark.Locality : string.Empty;
                location.PostalCode = !string.IsNullOrWhiteSpace(placemark.PostalCode) ? placemark.PostalCode : string.Empty;
                location.CountryCode = !string.IsNullOrWhiteSpace(placemark.CountryCode) ? placemark.CountryCode : string.Empty;
                location.AdminArea = !string.IsNullOrWhiteSpace(placemark.AdminArea) ? placemark.AdminArea : string.Empty;
                location.CountryName = !string.IsNullOrWhiteSpace(placemark.CountryName) ? placemark.CountryName : string.Empty;
                location.SubAdminArea = !string.IsNullOrWhiteSpace(placemark.SubAdminArea) ? placemark.SubAdminArea : string.Empty;
                location.SubLocality = !string.IsNullOrWhiteSpace(placemark.SubLocality) ? placemark.SubLocality : string.Empty;
                location.SubThoroughfare = !string.IsNullOrWhiteSpace(placemark.SubThoroughfare) ? placemark.SubThoroughfare : string.Empty; 
                location.Thoroughfare = !string.IsNullOrWhiteSpace(placemark.Thoroughfare) ? placemark.Thoroughfare : string.Empty;
            }

            return location;
        }
    }
}
