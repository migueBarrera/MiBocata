using Mibocata.Core.Services.Interfaces;
using System;

namespace Mibocata.Core.Services
{
    public class ReleaseLoggingService : ILoggingService
    {
        private readonly IAppCenterService appCenterService;
        private readonly IFileLoggingService fileLoggingService;

        public ReleaseLoggingService(
            IAppCenterService appCenterService, 
            IFileLoggingService fileLoggingService)
        {
            this.appCenterService = appCenterService;
            this.fileLoggingService = fileLoggingService;
        }

        public void Initialize()
        {
            appCenterService.Initialize();
            fileLoggingService.Init();
        }

        public void Debug(string message)
        {
            appCenterService.TrackEvent(message); 
            fileLoggingService.LogInfo(message);
        }

        public void Error(Exception exception)
        {
            appCenterService.TrackError(exception);
            fileLoggingService.LogError($"{nameof(Error)}", exception);
        }

        public void Warning(string message)
        {
            appCenterService.TrackEvent(message, null);
            fileLoggingService.LogWarning(message);
        }
    }
}
