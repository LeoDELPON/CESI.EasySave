using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using CESI.BS.EasySave.BS.Factory;
using CESI.BS.EasySave.DAL;

namespace CESI.BS.EasySave.BS
{
    internal static class Logger
    {

        private static readonly string logFilePath = Environment.CurrentDirectory + @"\log\";
        private static readonly string logFileExtension = @".log";
        private static readonly string logFullName = LogFilePath + DateTime.Now.ToString("dd_MM_yyyy") + LogFileExtension;
        private static readonly string logInfoString = "All log are writen following this pattern:\n> date | workName | sourcePath | targetPath | fileSize | elapsedTime" + "\n\n";

        internal static void GenerateLog(Dictionary<WorkProperties, object> dictionary) 
        {
            string json = "";
            json = JsonSerializer.Serialize(new WorkFactory().CreateDtoLogger(dictionary));
            if (!FolderBuilder.CheckFolder(LogFilePath))
            {
                FolderBuilder.CreateFolder(LogFilePath);
            }
            if (!File.Exists(LogFullName))
            {
                File.WriteAllText(LogFullName, json);
            }
            StreamWriter file = File.AppendText(LogFullName);
            file.WriteLine(json);
            file.Close();
        }

        private static string LogFilePath => logFilePath;
        private static string LogFileExtension => logFileExtension;
        private static string LogFullName => logFullName;
        private static string LogInfoString => logInfoString;
    }
}
