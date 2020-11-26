using System;
using System.Collections.Generic;
using System.Text;
using CESI.BS.EasySave.DAL;
using System.Diagnostics;

namespace CESI.BS.EasySave.BS
{
    internal sealed class DataHandler
    {
        private Dictionary<WorkProperties, string> dictionary = new Dictionary<WorkProperties, string>;
        private static long Size { get; set; }
        private static int Files { get; set; }
        private static string Name { get; set; }
        private static string Source { get; set; }
        private static string Target { get; set; }

        private Stopwatch stopwatch;
        private DataHandler(long size , int files, string name, string source, string target)
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

        private void ComputeProgress(int remainingSize)
        {
            Dictionary[WorkProperties.Progress] = Convert.ToString((remainingSize * 100) / Convert.ToInt32(Dictionary[WorkProperties.Size]));
        }

        private static readonly Lazy<DataHandler> lazy = new Lazy<DataHandler>(() =>new DataHandler(Size, Files, Name, Source, Target));
        public static DataHandler Instance { get { return lazy.Value; } }

        public Dictionary<WorkProperties, string> Dictionary { get => dictionary; set => dictionary = value; }

        public void OnCompleted()
        {
            stopwatch.Stop();
            dictionary[WorkProperties.Duration] = Convert.ToString(stopwatch.ElapsedMilliseconds);//besoin de leo pour bypass la definition
            dictionary[WorkProperties.State] = "Not Running";//possiblement changer l'enum
            Logger.GenerateLog(Dictionary);
            StatusLogger.GenerateStatusLog(Dictionary);
            throw new NotImplementedException();
        }

        public void OnNext(Save value)
        {
            dictionary[WorkProperties.RemainingSize] = Convert.ToString(value.remainingSize);
            dictionary[WorkProperties.RemainingFiles] = Convert.ToString(value.remainingFiles);
            ComputeProgress(value.remainingSize);
            StatusLogger.GenerateStatusLog(Dictionary);
            throw new NotImplementedException();
        }
    }
}
