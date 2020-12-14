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
        private static string LogInfoString => logInfoString;
    }
}
