namespace Mibocata.Core.Services.Interfaces;

public interface ILoggingService
{
    void Initialize();

    void Debug(string message);

    void Warning(string message);

    void Error(Exception exception);
}
