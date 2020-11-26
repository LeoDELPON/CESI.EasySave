using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CESI.BS.EasySave.DAL;

namespace CESI.BS.EasySave.BS
{
    internal static class StatusLogger
    {
        private static readonly string logFilePath = Environment.CurrentDirectory + @"\statusLog\";
        private static readonly string logFileExtension = @".status";
        private static readonly string logFullName = LogFilePath + DateTime.Now.ToString("dd_mm_yyyy") + LogFileExtension;
        private static readonly string logInfoString = "The log are writen following this pattern:\n> date | workName| status | totalFiles | totalSize | Progress | remainingFiles | remainingSize  | sourcePath | targetPath" + "\n\n";

        internal static void GenerateStatusLog(Dictionary<WorkProperties, object> dictionary)
        {
            if (!FolderBuilder.CheckFolder(LogFilePath))
            {
                FolderBuilder.CreateFolder(LogFilePath);
            }
            System.IO.File.WriteAllText(LogFullName, LogInfoString + @"> " + dictionary[WorkProperties.Date] + " | "
                                                                           + dictionary[WorkProperties.Name] + " | "
                                                                           + dictionary[WorkProperties.State] + " | "
                                                                           + dictionary[WorkProperties.EligibleFiles] + " | "
                                                                           + dictionary[WorkProperties.Size] + " | "
                                                                           + dictionary[WorkProperties.Progress] + " | "
                                                                           + dictionary[WorkProperties.RemainingFiles] + " | "
                                                                           + dictionary[WorkProperties.RemainingSize] + " | "
                                                                           + dictionary[WorkProperties.Source] + " | "
                                                                           + dictionary[WorkProperties.Target]);
        }

        private static string LogFilePath => logFilePath;
        private static string LogFileExtension => logFileExtension;
        private static string LogFullName => logFullName;
        private static string LogInfoString => logInfoString;
    }
}
