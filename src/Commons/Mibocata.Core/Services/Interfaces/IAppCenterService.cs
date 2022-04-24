using System;
namespace Mibocata.Core.Services.Interfaces
{
    public interface IAppCenterService
    {
        void Initialize();

        void Debug(string message);

        void Warning(string message);

        void Error(Exception exception);
    }
}
