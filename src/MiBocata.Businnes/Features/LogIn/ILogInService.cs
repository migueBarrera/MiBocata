namespace MiBocata.Features.LogIn;

public interface ILogInService
{
    Task DoLoginAsync(string email, string pass);
}
