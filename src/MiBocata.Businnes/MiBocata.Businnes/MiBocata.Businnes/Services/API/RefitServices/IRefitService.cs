namespace MiBocata.Businnes.Services.API.RefitServices
{
    public interface IRefitService
    {
        T InitRefitInstance<T>(bool isAutenticated = false);
    }
}