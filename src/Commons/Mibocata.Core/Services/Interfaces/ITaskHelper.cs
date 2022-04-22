using System;
using System.Threading.Tasks;
using OperationResult;

namespace Mibocata.Core.Services.Interfaces
{
    public interface ITaskHelper
    {
        ITaskHelper CheckInternetBeforeStarting(bool check);

        ITaskHelper WithLogging(ILoggingService logger);

        ITaskHelper WhenStarting(Action action);

        ITaskHelper WhenFinished(Action action);

        ITaskHelper WithErrorHandling(Func<Exception, Task<bool>> handler);

        Task<Status> TryExecuteAsync(Func<Task> task);

        Task<Result<T>> TryExecuteAsync<T>(Func<Task<T>> task);
    }
}
