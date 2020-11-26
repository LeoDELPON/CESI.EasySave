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
        private static readonly string logFullName = LogFilePath + DateTime.Today.ToString("d") + LogFileExtension;
        private static readonly string logInfoString = "All log are writen following this pattern:\n> date | workName | sourcePath | targetPath | fileSize | elapsedTime" + "\n\n";

        internal static void GenerateLog(Dictionary<WorkProperties, object> dictionary) 
        {
            if (!File.Exists(LogFullName))
            {
                System.IO.File.WriteAllText(@"C:\Users\REMI\source\repos\LanguageClass\vendor\", LogInfoString);
            }
            using System.IO.StreamWriter file = new System.IO.StreamWriter(LogFullName);
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
