using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CESI.BS.EasySave.DAL;

namespace CESI.BS.EasySave.BS
{
    internal static class Logger
    {

        private static readonly string logFilePath = Environment.CurrentDirectory + @"log\";
        private static readonly string logFileExtension = @".log";
        private static readonly string logFullName = LogFilePath + DateTime.Now.ToString("dd_mm_yyyy") + LogFileExtension;
        private static readonly string logInfoString = "All log are writen following this pattern:\n> date | workName | sourcePath | targetPath | fileSize | elapsedTime" + "\n\n";

        internal static void GenerateLog(Dictionary<WorkProperties, object> dictionary) 
        {
            if (!FolderBuilder.CheckFolder(LogFilePath))
            {
                FolderBuilder.CreateFolder(LogFilePath);
            }
            if (!File.Exists(LogFullName))
            {
                System.IO.File.WriteAllText(LogFullName, LogInfoString);
            }
            StreamWriter file = File.AppendText(LogFullName);
            file.WriteLine(@"> " + dictionary[WorkProperties.Date] + " | "
                                 + dictionary[WorkProperties.Name] + " | "
                                 + dictionary[WorkProperties.Source] + " | "
                                 + dictionary[WorkProperties.Target] + " | "
                                 + dictionary[WorkProperties.Size] + " bytes | "
                                 + dictionary[WorkProperties.Duration] + " ms" + "\n\n");
        }
        private static string LogFilePath => logFilePath;
        private static string LogFileExtension => logFileExtension;
        private static string LogFullName => logFullName;
        private static string LogInfoString => logInfoString;
    }
}
