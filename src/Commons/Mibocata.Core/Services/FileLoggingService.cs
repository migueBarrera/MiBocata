using Mibocata.Core.Services.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;

namespace Mibocata.Core.Services
{
    internal class FileLoggingService : IFileLoggingService
    {
        private readonly ILogFileSystemService logFileSystemService;

        public const string LogTag = "mibocata";
        private const string LogFolderName = "logs";
        private const string LogTemplateFilename = "log.txt";

        public FileLoggingService(ILogFileSystemService logFileSystemService)
        {
            this.logFileSystemService = logFileSystemService;
        }

        public void Init()
        {
            var logFilePath = Path.Combine(logFileSystemService.LogFolderPath, LogTemplateFilename);

            Log.Logger = new LoggerConfiguration()
                //.WriteTo.AndroidLog()
                .Enrich.WithProperty(Serilog.Core.Constants.SourceContextPropertyName, LogTag)
                .WriteTo.File(
                    logFilePath,
                    rollingInterval: RollingInterval.Day,
                    rollOnFileSizeLimit: true,
                    retainedFileCountLimit: 110)
                .CreateLogger();

            LogInfo($"{nameof(FileLoggingService)} init; path: {logFilePath}");
        }

        public void LogInfo(string message)
        {
            Log.Information(message);
        }

        public void LogWarning(string message)
        {
            Log.Warning(message);
        }

        public void LogError(string message, Exception ex = null, IDictionary<string, string> properties = null)
        {
            Log.Error(ex, message, properties);
        }
    }
}
