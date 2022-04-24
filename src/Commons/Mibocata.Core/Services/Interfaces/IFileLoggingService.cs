using System;
using System.Collections.Generic;

namespace Mibocata.Core.Services.Interfaces
{
    public interface IFileLoggingService
    {
        void Init();

        void LogInfo(string message);

        void LogWarning(string message);

        void LogError(string message, Exception exception = null, IDictionary<string, string> properties = null);
    }
}
