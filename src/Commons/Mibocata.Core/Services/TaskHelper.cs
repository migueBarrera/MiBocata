using Mibocata.Core.Services.Interfaces;
using OperationResult;
using Refit;
using static OperationResult.Helpers;

namespace Mibocata.Core.Services;

public class TaskHelper : ITaskHelper
{
    private readonly IDialogService dialogsService;
    private readonly IConnectivityService connectivityService;

    private bool checkInternetAccess;
    private Action whenStarting;
    private Action whenFinished;
    private Func<Exception, Task<bool>> errorHandler;
    private ILoggingService logger;

    public TaskHelper(
        IDialogService dialogsService,
        IConnectivityService connectivityService)
    {
        this.dialogsService = dialogsService;
        this.connectivityService = connectivityService;
    }

    public ITaskHelper CheckInternetBeforeStarting(bool check)
    {
        checkInternetAccess = check;

        return this;
    }

    public ITaskHelper WhenStarting(Action action)
    {
        whenStarting = action;

        return this;
    }

    public ITaskHelper WhenFinished(Action action)
    {
        whenFinished = action;

        return this;
    }

    public ITaskHelper WithLogging(ILoggingService logger)
    {
        this.logger = logger;

        return this;
    }

    public ITaskHelper WithErrorHandling(Func<Exception, Task<bool>> handler)
    {
        errorHandler = handler;

        return this;
    }

    public async Task<Status> TryExecuteAsync(Func<Task> task)
    {
        var taskWrapper = new Func<Task<object>>(() => WrapTaskAsync(task));
        var result = await TryExecuteAsync(taskWrapper);

        if (result)
        {
            return Ok();
        }

        return Error();
    }

    public async Task<Result<T>> TryExecuteAsync<T>(Func<Task<T>> task)
    {
        if (checkInternetAccess)
        {
            bool abort = await ExecuteInternetAccessLoopAsync();

            if (abort)
            {
                return Error();
            }
        }

        whenStarting?.Invoke();

        Result<T> result = Error();

        try
        {
            var actualResult = await task();
            result = Ok(actualResult);
        }
        catch (Exception exception)
        {
            logger?.Error(exception);

            var isAlreadyHandled = false;

            if (errorHandler != null)
            {
                isAlreadyHandled = await errorHandler.Invoke(exception);
            }

            if (!isAlreadyHandled)
            {
                isAlreadyHandled = await HandleCommonExceptionsAsync(exception);
            }

            if (!isAlreadyHandled)
            {
                throw;
            }
        }
        finally
        {
            whenFinished?.Invoke();
        }

        return result;
    }

    private static async Task<object> WrapTaskAsync(Func<Task> innerTask)
    {
        await innerTask();

        return new object();
    }

    private async Task<bool> ExecuteInternetAccessLoopAsync()
    {
        while (!connectivityService.IsThereInternet)
        {
            await dialogsService.ShowAlertAsync(
                "This application requires an active Internet connection to work.",
                "There is no internet access");
        }

        return !connectivityService.IsThereInternet;
    }

    private async Task<bool> HandleCommonExceptionsAsync(Exception exception)
    {
        if (exception is HttpRequestException || exception is ApiException)
        {
            await dialogsService.ShowAlertAsync(
                "An error was detected while communicating with server. Please, try again later.",
                "Data error");

            return true;
        }

        return false;
    }
}