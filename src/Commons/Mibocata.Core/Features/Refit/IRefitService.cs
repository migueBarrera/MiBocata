namespace Mibocata.Core.Features.Refit
{
    public interface IRefitService
    {
        T InitRefitInstance<T>(bool isAutenticated = false);
    }
}