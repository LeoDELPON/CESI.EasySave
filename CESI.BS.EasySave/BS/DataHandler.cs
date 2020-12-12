using System;
using System.Collections.Generic;
using System.Text;
using CESI.BS.EasySave.DAL;
using System.Diagnostics;
using CESI.BS.EasySave.DTO;
using CESI.BS.EasySave.BS.Factory;

namespace CESI.BS.EasySave.BS
{
    public class DataHandler : Observable
    {
        private Dictionary<WorkProperties, object> dictionary = new Dictionary<WorkProperties, object>();
        private readonly Stopwatch stopwatch = new Stopwatch();
        public Stopwatch GetStopwatch { get { return stopwatch; } }
        public DataHandler() : base()
        {
            subscribers = new List<Observer>();
            serverSubscriber = new List<Observer>();
        }

        public void Init(Dictionary<WorkProperties, object> newDictionary)
        {
            dictionary[WorkProperties.Date] = DateTime.Now.ToString("HH:mm:ss");
            dictionary[WorkProperties.Name] = newDictionary[WorkProperties.Name];
            dictionary[WorkProperties.Source] = newDictionary[WorkProperties.Source];
            dictionary[WorkProperties.Target] = newDictionary[WorkProperties.Target];
            dictionary[WorkProperties.Size] = newDictionary[WorkProperties.Size];
            dictionary[WorkProperties.EligibleFiles] = newDictionary[WorkProperties.EligibleFiles];
            dictionary[WorkProperties.State] = "Running";
            dictionary[WorkProperties.EncryptDuration] = "0";
        }

        private void ComputeProgress(object remainingSize)
        {
            long sizeProperty = long.Parse(Dictionary[WorkProperties.Size].ToString());
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
        public List<Observer> serverSubscriber { get; set; }

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
            dictionary[WorkProperties.Duration] = stopwatch.ElapsedMilliseconds;
            dictionary[WorkProperties.EncryptDuration] = newDictionary[WorkProperties.EncryptDuration];
            ComputeProgress((Int64)newDictionary[WorkProperties.RemainingSize]);

            Logger.GenerateLog(Dictionary);
            StatusLogger.GenerateStatusLog(Dictionary);
            NotifyServer(Dictionary);
        }

        public void Subscribe(Observer obs)
        {
           subscribers.Add(obs);
        }

        public void SubscribeServer(Observer obs)
        {
            serverSubscriber.Add(obs);
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

        public void NotifyServer(Dictionary<WorkProperties, object> dict)
        {
            try
            {
                DTOLogger dto = new WorkFactory().CreateDtoLogger(dict);
                foreach (Observer obs in serverSubscriber)
                {
                    obs.reactDataLogServ(dto);
                }
            }catch(Exception e)
            {
                Console.WriteLine("[-] An error occured while trying to notify the server");
            }
        }

        public void Unsubscribe(Observer obs)
        {
            subscribers.Remove(obs);
        }

        public void UnSubscribeServer(Observer obs)
        {
            serverSubscriber.Remove(obs);
        }
    }
}
