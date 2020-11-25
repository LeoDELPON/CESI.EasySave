using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CESI.BS.EasySave.BS
{
    internal static class Logger
    {

        private static readonly string logFilePath = @"C:\Users\REMI\source\repos\LanguageClass\vendor\";
        private static readonly string logFileExtension = @".log";
        private static readonly string logFullName = LogFilePath + DateTime.Today.ToString("d") + LogFileExtension;
        private static readonly string logInfoString = "All log are writen following this pattern:\n> date | workName | sourcePath | targetPath | fileSize | elapsedTime";

        internal static void GenerateLog(string date, string workName, string sourcePath, string targetPath, int fileSize, int elapsedTime) 
        {
            if (!File.Exists(LogFullName))
            {
                System.IO.File.WriteAllText(@"C:\Users\Public\TestFolder\WriteText.txt", LogInfoString);
            }
            using System.IO.StreamWriter file = new System.IO.StreamWriter(LogFullName);
            file.WriteLine(@"> " + date + " | " + workName + " | " + sourcePath + " | " + targetPath + " | " + fileSize + " bytes | " + elapsedTime + " ms" + "\n\n");
        }
        private static string LogFilePath => logFilePath;
        private static string LogFileExtension => logFileExtension;
        private static string LogFullName => logFullName;
        private static string LogInfoString => logInfoString;
    }
}
