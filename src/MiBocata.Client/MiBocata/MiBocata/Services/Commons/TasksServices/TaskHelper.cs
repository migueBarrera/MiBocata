using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MiBocata.Services.ConnectivityServices;
using MiBocata.Services.DialogService;
using MiBocata.Services.LoggingService;
using OperationResult;
using Refit;
using static OperationResult.Helpers;

namespace MiBocata.Services.TasksServices
{
    public class TaskHelper : ITaskHelper
    {
        private readonly IDialogService dialogsService;
        private readonly IConnectivityService connectivityService;

        private bool checkInternetAccess;
        private Action whenStarting;
        private Action whenFinished;
        private Func<Exception, Task<bool>> errorHandler;
        private ILoggingService logger;

        public TaskHelper(IDialogService dialogsService, IConnectivityService connectivityService)
        {
            this.dialogsService = dialogsService;
            this.connectivityService = connectivityService;
        }

        public ITaskHelper CheckInternetBeforeStarting(bool check)
        {
            this.checkInternetAccess = check;

            return this;
        }

        public ITaskHelper WhenStarting(Action action)
        {
            this.whenStarting = action;

            return this;
        }

        public ITaskHelper WhenFinished(Action action)
        {
            this.whenFinished = action;

            return this;
        }

        public ITaskHelper WithLogging(ILoggingService logger)
        {
            this.logger = logger;

            return this;
        }

        public ITaskHelper WithErrorHandling(Func<Exception, Task<bool>> handler)
        {
            this.errorHandler = handler;

            return this;
        }

        public async Task<Status> TryExecuteAsync(Func<Task> task)
        {
            var taskWrapper = new Func<Task<object>>(() => WrapTaskAsync(task));
            var result = await this.TryExecuteAsync(taskWrapper);

            if (result)
            {
                return Ok();
            }

            return Error();
        }

        public async Task<Result<T>> TryExecuteAsync<T>(Func<Task<T>> task)
        {
            if (this.checkInternetAccess)
            {
                bool abort = await this.ExecuteInternetAccessLoopAsync();

                if (abort)
                {
                    return Error();
                }
            }

            this.whenStarting?.Invoke();

            Result<T> result = Error();

            try
            {
                var actualResult = await task();
                result = Ok(actualResult);
            }
            catch (Exception exception)
            {
                this.logger?.Error(exception);

                var isAlreadyHandled = false;

                if (this.errorHandler != null)
                {
                    isAlreadyHandled = await this.errorHandler.Invoke(exception);
                }

                if (!isAlreadyHandled)
                {
                    isAlreadyHandled = await this.HandleCommonExceptionsAsync(exception);
                }

                if (!isAlreadyHandled)
                {
                    throw;
                }
            }
            finally
            {
                this.whenFinished?.Invoke();
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
            while (!this.connectivityService.IsThereInternet)
            {
                await this.dialogsService.ShowAlertAsync(
                    "This application requires an active Internet connection to work.",
                    "There is no internet access");
            }

            return !this.connectivityService.IsThereInternet;
        }

        private async Task<bool> HandleCommonExceptionsAsync(Exception exception)
        {
            if (exception is HttpRequestException || exception is ApiException)
            {
                await this.dialogsService.ShowAlertAsync(
                    "An error was detected while communicating with server. Please, try again later.",
                    "Data error");

                return true;
            }

            return false;
        }
    }
}