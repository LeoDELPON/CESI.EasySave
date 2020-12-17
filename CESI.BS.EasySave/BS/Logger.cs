using CESI.BS.EasySave.BS.Factory;
using CESI.BS.EasySave.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


namespace CESI.BS.EasySave.BS
{
    internal static class Logger
    {

        static StreamWriter file;
        private static readonly string logFilePath = Environment.CurrentDirectory + @"\log\";
        private static readonly string logFileExtension = @".log";
        private static readonly string logFullName = LogFilePath + DateTime.Now.ToString("dd_MM_yyyy") + LogFileExtension;

        internal static void GenerateLog(Dictionary<WorkProperties, object> dictionary)
        {
            string json = JsonSerializer.Serialize(new WorkFactory().CreateDtoLogger(dictionary));
            if (!Directory.Exists(LogFilePath))
            {
                FolderBuilder.CreateFolder(LogFilePath);
            }
            if (!File.Exists(LogFullName))
            {
                File.WriteAllText(LogFullName, json);
            }

            lock (ThreadMutex.writeLogger)
            {
                file = File.AppendText(LogFullName);
                file.WriteLine(json);
                file.Close();
            }

        }

        private static string LogFilePath => logFilePath;
        private static string LogFileExtension => logFileExtension;
        private static string LogFullName => logFullName;
    }
}
