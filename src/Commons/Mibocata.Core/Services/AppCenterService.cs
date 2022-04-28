using Mibocata.Core.Services.Interfaces;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mibocata.Core.Services
{
    public class AppCenterService : Interfaces.IAppCenterService
    {
        private readonly IAppCenterSecretService appCenterSecretService;
        private readonly ILogFileSystemService logFileSystemService;

        public AppCenterService(
            IAppCenterSecretService appCenterSecretService,
            ILogFileSystemService logFileSystemService)
        {
            if (appCenterSecretService is null)
            {
                throw new ArgumentNullException($"You must be implemented and register {nameof(IAppCenterSecretService)}");
            }

            this.appCenterSecretService = appCenterSecretService;
            this.logFileSystemService = logFileSystemService;
        }

        public void Initialize()
        {
            AppCenter.LogLevel = LogLevel.Verbose;
            AppCenter.Start(appCenterSecretService.GetSecret(), typeof(Analytics), typeof(Crashes));

            Crashes.GetErrorAttachments =
                    report => GetAttatchment();
        }

        public void TrackError(Exception exception, IDictionary<string, string> properties = null, params ErrorAttachmentLog[] attachments)
        {
            Crashes.TrackError(exception, properties, attachments);
        }

        public void TrackEvent(string name, IDictionary<string, string> properties = null)
        {
            Analytics.TrackEvent(name, properties);
        }

        private IEnumerable<ErrorAttachmentLog> GetAttatchment()
        {
            var localFolderPath = logFileSystemService.LogFolderPath;
            var latestLogPath = Directory
                .EnumerateFiles(localFolderPath)
                .OrderByDescending(item => item)
                .FirstOrDefault();

            ErrorAttachmentLog attachment;

            if (latestLogPath == null)
            {
                attachment = ErrorAttachmentLog.AttachmentWithText("(No log found.)", "Log.txt");
            }
            else
            {
                try
                {
                    var data = File.ReadAllBytes(latestLogPath);
                    var fileName = Path.GetFileName(latestLogPath);
                    attachment = ErrorAttachmentLog.AttachmentWithBinary(data, fileName, "text/plain");
                }
                catch (Exception e)
                {
                    attachment = ErrorAttachmentLog.AttachmentWithText("(ERROR READING LOG FILE)", e.StackTrace);
                }
            }

            return new[] { attachment };
        }
    }
}
