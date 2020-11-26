using CESI.BS.EasySave.DAL;
using CESI.BS.EasySave.BS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;

namespace CESI.BS.EasySave.BS
{
    internal class Full : Save, IObservable<Dictionary<WorkProperties, object>>
    {
        public List<long> dirSize = new List<long>();
        public List<IObserver<Dictionary<WorkProperties, object>>> observers;

        public Full() : base()
        {
            observers =  new List<IObserver<Dictionary<WorkProperties, object>>>();
            TypeSave = SaveType.FULL;
        }


        public IDisposable Subscribe(IObserver<Dictionary<WorkProperties, object>> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber(observers, observer);
        }


        private class Unsubscriber : IDisposable
        {
            private List<IObserver<Dictionary<WorkProperties, object>>> _observers;
            private IObserver<Dictionary<WorkProperties, object>> _observer;

            public Unsubscriber(List<IObserver<Dictionary<WorkProperties, object>>> observers, IObserver<Dictionary<WorkProperties, object>> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }

        public override int SaveProcess(string sourceD, string destD)
        {
            int returnInfo = SUCCESS_OPERATION;
            DateTime durationStart = DateTime.Now;
            //propertiesWork[WorkProperties.Date] = durationStart;
            if (!Directory.Exists(sourceD))
                throw new DirectoryNotFoundException(
                    "[-] Source directory has not been found: " + sourceD);

            DirectoryInfo dirSource = new DirectoryInfo(sourceD);
            DirectoryInfo dirDestination = new DirectoryInfo(destD);
            bool status = CopyAll(dirSource, dirDestination);
            if (!status)
            {
                propertiesWork[WorkProperties.Duration] = -1;

                returnInfo = ERROR_OPERATION;
                return returnInfo;
            }

            propertiesWork[WorkProperties.Duration] = DateTime.Now.Add(DateTime.Now.Subtract(durationStart)).Date;
            observers.ForEach(l => l.OnNext((Dictionary<WorkProperties, object>)propertiesWork[WorkProperties.Duration]));
            observers.ForEach(r => r.OnCompleted());
            return returnInfo;
        }

        public bool CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            if (!FolderBuilder.CheckFolder(target.ToString()))
                FolderBuilder.CreateFolder(target.FullName);

            int fileNumber = GetFilesFromFolder(source.ToString()).Length;
            propertiesWork[WorkProperties.EligibleFiles] = fileNumber;
            observers.ForEach(o => o.OnNext((Dictionary<WorkProperties, object>)propertiesWork[WorkProperties.EligibleFiles]));

            long folderSize = GetFolderSize(source.ToString());
            propertiesWork[WorkProperties.Size] = folderSize;
            observers.ForEach(p => p.OnNext((Dictionary<WorkProperties, object>)propertiesWork[WorkProperties.Size]));

            //Instantiate DataHandler;

            try
            {
                foreach (FileInfo file in source.GetFiles())
                {
                    Console.WriteLine(@"[+] Copying {0}\{1}", target.FullName, file.Name);
                    dirSize.Add(file.Length);
                    file.CopyTo(Path.Combine(target.FullName, file.Name), true);
                    observers.ForEach(o => o.OnNext((Dictionary<WorkProperties, object>)propertiesWork[WorkProperties.RemainingFiles]));
                    propertiesWork[WorkProperties.RemainingSize] = folderSize - file.Length;
                    observers.ForEach(q => q.OnNext((Dictionary<WorkProperties, object>)propertiesWork[WorkProperties.RemainingSize]));
                }

                foreach (DirectoryInfo directorySourceSubDir in source.GetDirectories())
                {
                    DirectoryInfo nextTargetSubDir =
                        target.CreateSubdirectory(directorySourceSubDir.Name);
                    propertiesWork[WorkProperties.RemainingFiles] = fileNumber - 1;
                    CopyAll(directorySourceSubDir, nextTargetSubDir);
                }
                
                return true;
            } catch(SecurityException e)
            {
                Console.WriteLine("[-] While tryin to copy a file from source to destination," +
                    "an error occured because of the right access : {0}", e);
                return false;
            }
        }
        public override string GetName()
        {
            return Language.GetRequestedString(16);
        }
    }
}
