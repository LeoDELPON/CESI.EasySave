using System;
using System.Collections.Generic;
using System.Text;
using CESI.BS.EasySave.DAL;
using System.Diagnostics;

namespace CESI.BS.EasySave.BS
{
    public class DataHandler : Observable
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
            subscribers = new List<Observer>();
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
            dictionary[WorkProperties.Duration] = DateTime.Now.ToString("HH-mm-ss");
            dictionary[WorkProperties.EncryptDuration] = "0";
        }

        private void ComputeProgress(object remainingSize)
        {
            long sizeProperty = (long)Dictionary[WorkProperties.Size];
            if (sizeProperty != 0)
            {
                Dictionary[WorkProperties.Progress] = (sizeProperty-(long)remainingSize) * 100 / sizeProperty;
                NotifyAll();
            }
            else
            {
                Dictionary[WorkProperties.Progress] = "Too little size. Can't compute progress";
            }

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

        public void OnNext(Dictionary<WorkProperties, object> newDictionary)
        {
            dictionary[WorkProperties.RemainingSize] = newDictionary[WorkProperties.RemainingSize];
            dictionary[WorkProperties.RemainingFiles] = newDictionary[WorkProperties.RemainingFiles];
            dictionary[WorkProperties.Duration] = newDictionary[WorkProperties.Duration];
            ComputeProgress((Int64)newDictionary[WorkProperties.RemainingSize]);

            Logger.GenerateLog(Dictionary);
            StatusLogger.GenerateStatusLog(Dictionary);
        }

        public void Subscribe(Observer obs)
        {
           subscribers.Add(obs);
        }

        public void NotifyAll()
        {
            if (double.TryParse(dictionary[WorkProperties.Progress].ToString(), out double prog)){
                foreach (Observer obs in subscribers)
                {
                    obs.reactProgression(prog);
                }
            
            }
          
        }

        public void Unsubscribe(Observer obs)
        {
            subscribers.Remove(obs);
        }
    }
}
