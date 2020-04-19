namespace MiBocata.Services.API.RefitServices
{
    public interface IRefitService
    {
        T InitRefitInstance<T>(bool isAutenticated = false);
    }
}