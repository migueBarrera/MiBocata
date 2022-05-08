using Mibocata.Core.Framework;

namespace MiBocata.Features.LogIn;

public interface ILogInService
{
    Task DoLoginAsync(IBusyViewModel busyViewModel, string email, string pass);
}
