namespace MiBocata.Businnes.Features.Stores
{
    public interface IChooseLocationService
    {
        Task<Location> GetLastKnownLocationAsync();

        Task GetPlaceMarkAndContinue(IEnumerable<Model> locations);
    }

    public class Model
    {
        public Position Position { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }
    }
}
