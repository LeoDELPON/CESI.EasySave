using CESI.BS.EasySave.BS.Factory;
using CESI.BS.EasySave.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace CESI.BS.EasySave.BS
{
    internal static class StatusLogger
    {

        private static readonly string logFilePath = Environment.CurrentDirectory + @"\statusLog\";
        private static readonly string logFileExtension = @".status";
        private static readonly string logFullName = LogFilePath + "status" + LogFileExtension;

        internal static void GenerateStatusLog(Dictionary<WorkProperties, object> dictionary)
        {
            string json = JsonSerializer.Serialize(new WorkFactory().CreateDtoStatusLogger(dictionary));
            if (!Directory.Exists(LogFilePath))
            {
                FolderBuilder.CreateFolder(LogFilePath);
            }
            if (!File.Exists(LogFullName))
            {
                File.WriteAllText(LogFullName, json);
            }

            lock (ThreadMutex.writeStatusLogger)
            {
                StreamWriter file = File.AppendText(LogFullName);
                file.WriteLine(json);
                file.Close();
            }


        }

        private static string LogFilePath => logFilePath;
        private static string LogFileExtension => logFileExtension;
        private static string LogFullName => logFullName;
    }
}
