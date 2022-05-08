using Mibocata.Core.Framework;

namespace Mibocata.Core.Services.Interfaces;

public interface ITaskHelperFactory
{
    ITaskHelper CreateInternetAccessViewModelInstance(ILoggingService logger);

    ITaskHelper CreateInternetAccessViewModelInstance(ILoggingService logger, IBusyViewModel busyViewModel);
}
