using System;
using System.Collections.Generic;
using System.Text;
using CESI.BS.EasySave.DAL;
using System.Diagnostics;

namespace CESI.BS.EasySave.BS
{
    internal sealed class DataHandler
    {
        private Dictionary<WorkProperties, object> dictionary = new Dictionary<WorkProperties, object>();
        private static long Size { get; set; }
        private static int Files { get; set; }
        private static string Name { get; set; }
        private static string Source { get; set; }
        private static string Target { get; set; }

        private readonly Stopwatch stopwatch;
        private DataHandler()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        public void Init(int files, long size, string name, string source, string target)
        {
            Size = size;
            Files = files;
            dictionary[WorkProperties.Date] = DateTime.Now.ToString("HH:mm:ss");
            dictionary[WorkProperties.Name] = name;
            dictionary[WorkProperties.Source] = source;
            dictionary[WorkProperties.Target] = target;
            dictionary[WorkProperties.Size] = size;
            dictionary[WorkProperties.EligibleFiles] = files;
            dictionary[WorkProperties.State] = "Running";
        }

        private void ComputeProgress(object remainingSize)
        {
            long sizeProperty = (long)Dictionary[WorkProperties.Size];
            if (sizeProperty != 0)
            {
                Dictionary[WorkProperties.Progress] = ((long)remainingSize * 100) / sizeProperty;
            }
            else
            {
                Dictionary[WorkProperties.Progress] = "Too little size. Can't compute progress";
            }

        }

        private static readonly Lazy<DataHandler> lazy = new Lazy<DataHandler>(() =>new DataHandler());
        public static DataHandler Instance { get { return lazy.Value; } }

        public Dictionary<WorkProperties, object> Dictionary { get => dictionary; set => dictionary = value; }

        public void OnStop(bool noError)
        {
            stopwatch.Stop();           
            if (noError)
            {
                dictionary[WorkProperties.Duration] = Convert.ToString(stopwatch.ElapsedMilliseconds);
            }
            else
            {
                dictionary[WorkProperties.Duration] = "-1";
            }
            dictionary[WorkProperties.State] = "Not Running";
            dictionary[WorkProperties.RemainingSize] = "Error";
            dictionary[WorkProperties.RemainingFiles] = "Error";
            dictionary[WorkProperties.Progress] = "Error";
            Logger.GenerateLog(Dictionary);
            StatusLogger.GenerateStatusLog(Dictionary);
        }

        public void OnNext(object remainingFiles, object remainingSize)
        {
            dictionary[WorkProperties.RemainingSize] = remainingSize;
            dictionary[WorkProperties.RemainingFiles] = remainingFiles;
            ComputeProgress(remainingSize);
            StatusLogger.GenerateStatusLog(Dictionary);
        }
    }
}
