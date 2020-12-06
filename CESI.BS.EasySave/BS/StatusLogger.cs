using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;
using CESI.BS.EasySave.DAL;
using CESI.BS.EasySave.BS.Factory;

namespace CESI.BS.EasySave.BS
{
    internal static class StatusLogger
    {
        private static readonly string logFilePath = Environment.CurrentDirectory + @"\statusLog\";
        private static readonly string logFileExtension = @".status";
        private static readonly string logFullName = LogFilePath + "status" + LogFileExtension;
        private static readonly string logInfoString = "The log are writen following this pattern:\n> date | workName| status | totalFiles | totalSize | Progress | remainingFiles | remainingSize  | sourcePath | targetPath" + "\n\n";

        internal static void GenerateStatusLog(Dictionary<WorkProperties, object> dictionary)
        {
            if (!FolderBuilder.CheckFolder(LogFilePath))
            {
                FolderBuilder.CreateFolder(LogFilePath);
            }
            string json = JsonSerializer.Serialize(new WorkFactory().CreateDtoStatusLogger(dictionary));
            File.WriteAllText(LogFullName, json);
        }

        private static string LogFilePath => logFilePath;
        private static string LogFileExtension => logFileExtension;
        private static string LogFullName => logFullName;
        private static string LogInfoString => logInfoString;
    }
}
