using CESI.BS.EasySave.BS.Observers;
using CESI.BS.EasySave.DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CESI.BS.EasySave.BS
{

    public abstract class Save : Observable, ObservableFileSize
    {
        protected long FolderSize { get; set; }

        public List<string> _cryptoExtension;

        public List<string> _priorityExtension;

        public string _key;

        private string FullBackupPath { get; set; }

        private string BackupPath { get; set; }

        private string SrcPath { get; set; }

        private DirectoryInfo _srcDir;

        private DirectoryInfo _fullDir;

        private DirectoryInfo _backupDir;

        public long fileMaxSize = 100000000000;

        public SaveType TypeSave { get; protected set; }

        protected Dictionary<WorkProperties, object> propertiesWork;

        public object pause = new object();
        public string IdTypeSave { get; set; }
        public List<Observer> subscribers { get; set; } = new List<Observer>();
        public List<ObserverFileSize> subscribersFileSize { get; set; } = new List<ObserverFileSize>();

        public DataHandler handler = DataHandler.Instance;
        public static int SUCCESS_OPERATION = 0;
        public static int ERROR_OPERATION = 1;
        public static int IN_PROGRESS = 2;
        public static int STOP_SAVE = 3;
        public static int NO_FULL_SAVE = 4;
        public Save()
        {
  
            propertiesWork = new Dictionary<WorkProperties, object>
            {
                { WorkProperties.Duration, 0 },
                { WorkProperties.Date, null },
                { WorkProperties.CurrentFile, "notSet" },
                { WorkProperties.Progress, 0 },
                { WorkProperties.EligibleFiles, 0 },
                { WorkProperties.RemainingFiles, 0 },
                { WorkProperties.RemainingSize, 0 },
                { WorkProperties.Size, 0 }
            };
        }

      
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> design pattern "template method" </summary>
        ///
        /// <remarks>   Leo , 24/11/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool SaveProcess(string sourceDirectory, string targetFolder)
        {
            SrcPath = sourceDirectory;

            CheckSrcFile();

            _srcDir = new DirectoryInfo(SrcPath);

            BackupPath = SetSavePath(targetFolder);

            _fullDir = new DirectoryInfo(FullBackupPath);

            FolderBuilder.CreateFolder(BackupPath);


            ICollection<FileInfo> listFileLowPrio = SelectFilesToCopy(_srcDir, _fullDir);

            propertiesWork[WorkProperties.EligibleFiles] = listFileLowPrio.Count();
            propertiesWork[WorkProperties.Size] = FolderSize = GetFilesSize(listFileLowPrio);

            IEnumerable<FileInfo> listFileHighPrio = FilterHighPriorityFiles(ref listFileLowPrio);

            propertiesWork[WorkProperties.Source] = SrcPath;
            propertiesWork[WorkProperties.TypeSave] = TypeSave;
            propertiesWork[WorkProperties.Target] = BackupPath;
            handler.Init(propertiesWork);

            if (LoopThroughFiles(listFileHighPrio)){
                return LoopThroughFiles(listFileLowPrio);
            }
            else
            {
                throw new DirectoryNotFoundException("[-] High priority file save went wrong.");
            }

        }

        public void RunProcess(string processName, string arguments)
        {
            Process process = new Process();
            process.StartInfo.FileName = processName;
            process.StartInfo.Arguments = arguments;
            process.Start();
            process.WaitForExit();
        }
        public void WaitForUnpause()
        {
            Monitor.Enter(pause);
            Monitor.Exit(pause);
        }
        public void Subscribe(Observer obs)
        {
            subscribers.Add(obs);
        }

        public void NotifyAll(long progress)
        {
            foreach (Observer obs in subscribers)
            {
                obs.ReactProgression(progress);
            }
        }

        public void CheckFileSize(long size)
        {
            if (size > fileMaxSize && !Monitor.IsEntered(ThreadMutex.bigFile))
            {
                //notifyFileSize();
                Monitor.Enter(ThreadMutex.bigFile);
            }
        }
        public void Unsubscribe(Observer obs)
        {
            subscribers.Remove(obs);
        }

        public void SubscribeFileSize(ObserverFileSize obs)
        {
            subscribersFileSize.Add(obs);
        }

        public void UnsubscribeFileSize(ObserverFileSize obs)
        {
            subscribersFileSize.Remove(obs);
        }

        public void NotifyFileSize()
        {
            foreach(ObserverFileSize obs in subscribersFileSize)
            {
                obs.React(this);
            }
        }
        public void EndReact()
        {
/*            foreach (ObserverFileSize obs in subscribersFileSize)
            {
                obs.EndReaction(this);
            }*/
            if (Monitor.IsEntered(ThreadMutex.bigFile)){
                Monitor.Exit(ThreadMutex.bigFile);
            }
        }
       
        public void CryptoSoft(string _key, string sourcePath, string destPath)
        {
            string arguments = _key + " " + sourcePath + " " + destPath;
            RunProcess(Environment.CurrentDirectory + @"\Cryptosoft\CESI.Cryptosoft.EasySave.Project.exe", arguments);
        }

        public string ReplaceLastOccurrence(string Find, string Replace)
        {
            int place = SrcPath.LastIndexOf(Find);
            if (place == -1)
                return SrcPath;
            string result = SrcPath.Remove(place, Find.Length).Insert(place, Replace);
            return result;
        }

        public List<string> ScanSourceFolder()
        {
            List<string> availableExtensions = new List<string>();
            foreach(FileInfo file in GetFilesFromFolder(_srcDir)){
                foreach(string ext in availableExtensions)
                {
                    if (file.Extension != ext)
                    {
                        availableExtensions.Add(file.Extension);
                    }
                }
                
            }
            return availableExtensions;
        }

        protected IEnumerable<FileInfo> GetFilesFromFolder(DirectoryInfo dir)
        {
            IEnumerable<FileInfo> files = dir.GetFiles(
                "*.*",
                SearchOption.AllDirectories
                );
            return files;
        }

        private long GetFilesSize(IEnumerable<FileInfo> fileList)
        {
            long size = 0;
            foreach (FileInfo file in fileList)
            {
                size += file.Length;
            }
            return size;
        }

        public void CheckSrcFile()
        {
            if (!Directory.Exists(SrcPath))
            {
                throw new DirectoryNotFoundException("[-] Source directory has not been found: " + SrcPath);
            }
        }

        public string SetSavePath(string path)
        {
            FullBackupPath = path + @"\" + _srcDir.Name + @"\Full";
            return path + @"\" + _srcDir.Name + CheckSaveFile(path + @"\" + _srcDir.Name);
        }

        public virtual string CheckSaveFile(string dir)
        {
            return "";
        }

        public virtual List<FileInfo> SelectFilesToCopy(DirectoryInfo srcDir, DirectoryInfo fullDir)
        {
            return GetFilesFromFolder(srcDir).ToList();
        }

        public bool LoopThroughFiles(IEnumerable<FileInfo> fileList)
        {
            Stopwatch stopwatch = new Stopwatch();
            DirectoryInfo directorySave;
            long Duration = 0;
            long EncryptTime = 0;
            try
            {
                foreach (FileInfo file in fileList)
                {
                    directorySave = new DirectoryInfo(file.DirectoryName.Replace(SrcPath, Path.GetFullPath(BackupPath, propertiesWork[WorkProperties.Source].ToString())));
                    try
                    {
                        if (!Directory.Exists(directorySave.FullName))
                        {
                            FolderBuilder.CreateFolder(directorySave.FullName);
                        }

                        stopwatch.Start();

                        EncryptTime += EncryptAndCopyFiles(file);

                        stopwatch.Stop();

                        Duration += stopwatch.ElapsedMilliseconds;

                        Console.WriteLine("[+] Copying {0}", file.FullName);
                    }
                    //gestion des erreurs
                    catch (Exception e)
                    {
                        Console.WriteLine("[-] An Error has occured while trying to copy Files : {0}", e);
                    }
                    //assignation de valeur du dictionnaire
                    propertiesWork[WorkProperties.RemainingFiles] = Convert.ToInt32(propertiesWork[WorkProperties.EligibleFiles]) - 1;
                    FolderSize -= file.Length;
                    propertiesWork[WorkProperties.RemainingSize] = FolderSize;
                    propertiesWork[WorkProperties.EncryptDuration] = EncryptTime;
                    handler.OnNext(propertiesWork);
                }
                handler.OnStop(true);
                return true;
            }
            catch (Exception e)
            {
                //gestion des erreurs
                Console.WriteLine("[-] An error occured while trying to save : {0}", e);
                handler.OnStop(false);
                return false;
            }
        }

        public long EncryptAndCopyFiles(FileInfo file)
        {
            Stopwatch stopwatch2 = new Stopwatch();
            bool isEncrypted = false;
            Parallel.ForEach(_cryptoExtension, element =>
            {

                if (file.Extension == element)
                {
                    stopwatch2.Start();
                    CryptoSoft(_key, file.FullName, file.FullName.Replace(_srcDir.FullName, _fullDir.FullName));
                    stopwatch2.Stop();
                    isEncrypted = true;
                    return;
                }
                
                


            });
            if (!isEncrypted)
            {
                file.CopyTo(file.FullName.Replace(_srcDir.FullName, BackupPath), true);
                
            }
            return stopwatch2.ElapsedMilliseconds;
        }

        protected IEnumerable<FileInfo> FilterHighPriorityFiles(ref ICollection<FileInfo> fileList)
        {
            IEnumerable<FileInfo> priorityFileList = new List<FileInfo>();
            bool isPrioritary = false;
            foreach (FileInfo file in fileList) {
                
                Parallel.ForEach(_priorityExtension, element =>
                {

                    if (file.Extension == element)
                    {
                        priorityFileList.Append<FileInfo>(file);
                        isPrioritary = true;
                        return;
                    }

                });
                if (isPrioritary)
                {
                    fileList.Remove(file);
                }
            }
            return priorityFileList;
        }

        public void notifyFileSize()
        {
            throw new NotImplementedException();
        }
    }
}