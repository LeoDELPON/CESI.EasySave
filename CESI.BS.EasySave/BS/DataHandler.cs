using System;
using System.Collections.Generic;
using System.Text;
using CESI.BS.EasySave.DAL;

namespace CESI.BS.EasySave.BS
{
    internal sealed class DataHandler : IObserver<Save>
    {
        private Dictionary<WorkProperties, string> dictionary = new Dictionary<WorkProperties, string>;
        private static long Size { get; set; }
        private static int Files { get; set; }
        private DataHandler(long size , int files)
        {
            Size = size;
            Files = files;
            dictionary[WorkProperties.Date] = DateTime.Today.ToString("hh:mm:ss");
            dictionary[WorkProperties.Name] = "";//besoin de marcus pour données stockées de work
            dictionary[WorkProperties.Source] = "";//besoin de marcus pour données stockées de work
            dictionary[WorkProperties.Target] = "";//besoin de marcus pour données stockées de work
            dictionary[WorkProperties.Size] = Convert.ToString(size);
            dictionary[WorkProperties.EligibleFiles] = Convert.ToString(files);
            dictionary[WorkProperties.State] = "Running";

        }

        private void ComputeProgress(int remainingSize)
        {
            Dictionary[WorkProperties.Progress] = Convert.ToString((remainingSize * 100) / Convert.ToInt32(Dictionary[WorkProperties.Size]));
        }

        private static readonly Lazy<DataHandler> lazy = new Lazy<DataHandler>(() =>new DataHandler(Size, Files));
        public static DataHandler Instance { get { return lazy.Value; } }


        public Dictionary<WorkProperties, string> Dictionary { get => dictionary; set => dictionary = value; }


        public void OnCompleted()
        {
            dictionary[WorkProperties.RemainingSize] = "";//besoin de leo pour bypass la definition
            dictionary[WorkProperties.State] = "Not Running";//possiblement changer l'enum
            Logger.GenerateLog(Dictionary);
            StatusLogger.GenerateStatusLog(Dictionary);
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
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
