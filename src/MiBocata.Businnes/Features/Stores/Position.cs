namespace MiBocata.Businnes.Features.Stores
{
    //TODO replace by xamarin.googlemaps type
    public class Position
    {
        private double latitude;
        private double longitude;

        public Position(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public double Latitude { get => latitude; set => latitude = value; }
        public double Longitude { get => longitude; set => longitude = value; }
    }
}