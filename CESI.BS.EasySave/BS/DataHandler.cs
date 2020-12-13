using System;
using System.Collections.Generic;
using System.Text;
using CESI.BS.EasySave.DAL;
using System.Diagnostics;
using CESI.BS.EasySave.DTO;
using CESI.BS.EasySave.BS.Factory;
using System.Threading;

namespace CESI.BS.EasySave.BS
{
    public class DataHandler : IObservable
    {
        private Dictionary<WorkProperties, object> dictionary = new Dictionary<WorkProperties, object>();
        private readonly Stopwatch stopwatch = new Stopwatch();
        public Stopwatch GetStopwatch { get { return stopwatch; } }
        public DataHandler() : base()
        {
            subscribers = new List<IObserver>();
            serverSubscriber = new List<IObserver>();
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
        public List<IObserver> subscribers { get; set; } = new List<IObserver>();
        public List<IObserver> serverSubscriber { get; set; }

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

            NotifyServer(Dictionary);
            Logger.GenerateLog(Dictionary);
            StatusLogger.GenerateStatusLog(Dictionary);
            return progress;
        }

     

        public void SubscribeServer(IObserver obs)
        {
            serverSubscriber.Add(obs);
        }

      

        public void NotifyServer(Dictionary<WorkProperties, object> dict)
        {
            DTODataServer dto = new WorkFactory().CreateDtoDataServer(dict);
            try
            {
                foreach (IObserver obs in serverSubscriber)
                {
                    obs.ReactDataLogServ(dto);
                }
            }catch(Exception e)
            {
                Console.WriteLine("[-] An error occured while trying to notify the server : {0}", e);
            }
        }

   

        public void UnSubscribeServer(IObserver obs)
        {
            serverSubscriber.Remove(obs);
        }
    }
}
