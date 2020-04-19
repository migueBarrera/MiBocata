using System;

namespace MiBocata.Services.LoggingService
{
    public interface ILoggingService
    {
        void Initialize();

        void Debug(string message);

        void Warning(string message);

        void Error(Exception exception);
    }
}
