namespace MiBocata.Converters;

//TODO replace by xamarin.googlemaps type
internal class Position
{
    private double latitude;
    private double longitude;

    public Position(double latitude, double longitude)
    {
        this.latitude = latitude;
        this.longitude = longitude;
    }
}