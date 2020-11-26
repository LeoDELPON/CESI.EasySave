using System;
using System.Collections.Generic;
using System.Text;
using CESI.BS.EasySave.DAL;
using System.Diagnostics;

namespace CESI.BS.EasySave.BS
{
    internal sealed class DataHandler
    {
        private Dictionary<WorkProperties, string> dictionary = new Dictionary<WorkProperties, string>();
        private static long Size { get; set; }
        private static int Files { get; set; }
        private static string Name { get; set; }
        private static string Source { get; set; }
        private static string Target { get; set; }

        private readonly Stopwatch stopwatch;
        private DataHandler(int files, long size, string name, string source, string target)
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            Size = size;
            Files = files;
            dictionary[WorkProperties.Date] = DateTime.Today.ToString("hh:mm:ss");
            dictionary[WorkProperties.Name] = name;
            dictionary[WorkProperties.Source] = source;
            dictionary[WorkProperties.Target] = target;
            dictionary[WorkProperties.Size] = Convert.ToString(size);
            dictionary[WorkProperties.EligibleFiles] = Convert.ToString(files);
            dictionary[WorkProperties.State] = "Running";

        }

        private void ComputeProgress(long remainingSize)
        {
            Dictionary[WorkProperties.Progress] = Convert.ToString((remainingSize * 100) / long.Parse(Dictionary[WorkProperties.Size]));
        }

        private static readonly Lazy<DataHandler> lazy = new Lazy<DataHandler>(() =>new DataHandler(Files, Size, Name, Source, Target));
        public static DataHandler Instance { get { return lazy.Value; } }

        public Dictionary<WorkProperties, string> Dictionary { get => dictionary; set => dictionary = value; }

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
            Logger.GenerateLog(Dictionary);
            StatusLogger.GenerateStatusLog(Dictionary);
            throw new NotImplementedException();
        }

        public void OnNext(int remainingFiles, long remainingSize)
        {
            dictionary[WorkProperties.RemainingSize] = Convert.ToString(remainingSize);
            dictionary[WorkProperties.RemainingFiles] = Convert.ToString(remainingFiles);
            ComputeProgress(remainingSize);
            StatusLogger.GenerateStatusLog(Dictionary);
            throw new NotImplementedException();
        }
    }
}
