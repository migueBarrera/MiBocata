using Mibocata.Core.Services.Interfaces;

namespace Mibocata.Core.Services
{
    internal class LogFileSystemService : ILogFileSystemService
    {
        private const string LogFolderName = "logs";
        private const string LogTemplateFilename = "log.txt";

        public string LogFolderPath => Path.Combine(FileSystem.AppDataDirectory, LogFolderName);
    }
}
