using System;
using System.Collections.Generic;
using System.Text;
using CESI.BS.EasySave.DAL;
using System.Diagnostics;
using System.Threading;

namespace CESI.BS.EasySave.BS
{
    public class DataHandler 
    {
        private Dictionary<WorkProperties, object> dictionary = new Dictionary<WorkProperties, object>();

        private readonly Stopwatch stopwatch;
        public DataHandler()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            subscribers = new List<Observer>();
        }

        public void Init(Dictionary<WorkProperties, object> newDictionary)
        {
            Monitor.Enter(dictionary);
            dictionary[WorkProperties.Date] = DateTime.Now.ToString("HH:mm:ss");
            dictionary[WorkProperties.Name] = newDictionary[WorkProperties.Name];
            dictionary[WorkProperties.Source] = newDictionary[WorkProperties.Source];
            dictionary[WorkProperties.Target] = newDictionary[WorkProperties.Target];
            dictionary[WorkProperties.Size] = newDictionary[WorkProperties.Size];
            dictionary[WorkProperties.EligibleFiles] = newDictionary[WorkProperties.EligibleFiles];
            dictionary[WorkProperties.State] = "Running";
            dictionary[WorkProperties.EncryptDuration] = "0";
            Monitor.Exit(dictionary);
        }

        private long ComputeProgress(object remainingSize)
        {
            long progress = 0;
            long sizeProperty = long.Parse(Dictionary[WorkProperties.Size].ToString());
            if (sizeProperty != 0)
            {
                progress = (sizeProperty - (long)remainingSize) * 100 / sizeProperty;
                Dictionary[WorkProperties.Progress] = progress;
              
            }
            else
            {
                Dictionary[WorkProperties.Progress] = "Too little size. Can't compute progress";
            }
            return progress;
        }

        private static readonly Lazy<DataHandler> lazy = new Lazy<DataHandler>(() =>new DataHandler());
        public static DataHandler Instance { get { return lazy.Value; } }

        public Dictionary<WorkProperties, object> Dictionary { get => dictionary; set => dictionary = value; }
        public List<Observer> subscribers { get; set; } = new List<Observer>();

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
                dictionary[WorkProperties.RemainingSize] = "Error";
                dictionary[WorkProperties.RemainingFiles] = "Error";
                dictionary[WorkProperties.Progress] = "Error";
            }
            dictionary[WorkProperties.State] = "Not Running";
            Logger.GenerateLog(Dictionary);
            StatusLogger.GenerateStatusLog(Dictionary);
        }

        public long OnNext(Dictionary<WorkProperties, object> newDictionary)
        {
            dictionary[WorkProperties.RemainingSize] = newDictionary[WorkProperties.RemainingSize];
            dictionary[WorkProperties.RemainingFiles] = newDictionary[WorkProperties.RemainingFiles];
            dictionary[WorkProperties.Duration] = stopwatch.ElapsedMilliseconds;
            dictionary[WorkProperties.EncryptDuration] = newDictionary[WorkProperties.EncryptDuration];
            long progress = ComputeProgress((Int64)newDictionary[WorkProperties.RemainingSize]);

            Logger.GenerateLog(Dictionary);
            StatusLogger.GenerateStatusLog(Dictionary);
            return progress;
        }

      
    }
}
