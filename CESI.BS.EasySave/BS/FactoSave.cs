using CESI.BS.EasySave.DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace CESI.BS.EasySave.BS
{
    class FactoSave
    {
        /// <summary>
        /// taille des fichiers.
        /// </summary>
        private long FolderSize { get; set; }
        /// <summary>
        /// Liste des extensions des fichiers.
        /// </summary>
        public List<string> _cryptoExtension;
        /// <summary>
        /// Liste des fichiers prioritaires.
        /// </summary>
        public List<string> _priorityExtension;
        /// <summary>
        /// Clé.
        /// </summary>
        public string _key;
        public SaveType TypeSave { get; private set; }
        /// <summary>
        /// Chemins pour sauvegarde complète.
        /// </summary>
        private string FullBackupPath { get; set; }
        /// <summary>
        /// Chemins pour sauvegarde différentiel.
        /// </summary>
        private string DiffBackupPath { get; set; }
        
        /// <summary>
        /// Chemin d'une sauvegarde.
        /// </summary>
        private string BackupPath { get; set; }
        

        private readonly Dictionary<WorkProperties, object> propertiesWork;
        public string IdTypeSave { get; set; }

        public DataHandler handler;

        Stopwatch stopwatch;
        public FactoSave(bool isSaveDiff,
                         string sourceFolder,
                         string targetFolder,
                         string props,
                         List<string> cryptoExtensions,
                         List<string> priorityExtensions,
                         string key)
        {
            if (isSaveDiff)
            {
                IdTypeSave = "dif";
                TypeSave = SaveType.DIFFERENTIAL;
            }
            else
            {
                IdTypeSave = "ful";
                TypeSave = SaveType.FULL;
            }
            propertiesWork[WorkProperties.Name] = props;
            _cryptoExtension = cryptoExtensions;
            _priorityExtension = priorityExtensions;
            _key = key;
            Save(isSaveDiff,sourceFolder,targetFolder);
        }

        public bool Save(bool isSaveDiff,string sourceFolder,string targetFolder)
        {
            handler = DataHandler.Instance;
            if (!Directory.Exists(sourceFolder))
            {
                throw new DirectoryNotFoundException("[-] Source directory has not been found: " + sourceFolder);
            }
            DirectoryInfo directorySource = new DirectoryInfo(sourceFolder);
            BackupPath = targetFolder + @"\" + directorySource.Name;
            FullBackupPath = BackupPath + @"\FullSaves";
            if (isSaveDiff)
            {
                DiffBackupPath = BackupPath + @"\DiffSaves";
            }
            if (!Directory.Exists(BackupPath))
            {
                Console.WriteLine("[+] Warning, Full Save not created, Full Save being created");
                DiffBackupPath = FullBackupPath;
            }
            else
            {
                DiffBackupPath += @"\" + DateTime.Now.ToString("dd_MM_yyyy");
            }

            FolderBuilder.CreateFolder(DiffBackupPath);

            DirectoryInfo directorySave = new DirectoryInfo(FullBackupPath);

            IEnumerable<FileInfo> listFileSource = GetFilesFromFolder(directorySource);
            if (isSaveDiff)
            {
                IEnumerable<FileInfo> listFileFullSave = GetFilesFromFolder(new DirectoryInfo(FullBackupPath));
                var queryGetDifferenceFile = (from file in listFileSource select file).Except(listFileFullSave, FileCompare.Instance);
                propertiesWork[WorkProperties.EligibleFiles] = queryGetDifferenceFile.Count();
                propertiesWork[WorkProperties.Size] = FolderSize = GetFilesSize(queryGetDifferenceFile);
                listFileSource = queryGetDifferenceFile;
            }
            propertiesWork[WorkProperties.Source] = sourceFolder;
            propertiesWork[WorkProperties.Target] = DiffBackupPath;
            handler.Init(propertiesWork);
            try
            { 
                foreach (FileInfo file in listFileSource)
                {
                    directorySave = new DirectoryInfo(file.DirectoryName.Replace(sourceFolder, Path.GetFullPath(DiffBackupPath, propertiesWork[WorkProperties.Source].ToString())));
                    try
                    {
                        if (!Directory.Exists(directorySave.FullName))
                        {
                            FolderBuilder.CreateFolder(directorySave.FullName);
                        }
                        // parti chiffrage
                        foreach (string ext in _cryptoExtension)
                        {

                            byte[] tmpByte = File.ReadAllBytes(file.FullName);
                            if (ext == file.Extension)
                            {
                                //chiffré
                                string args = _key + " " + file.FullName + " " + Path.Combine(directorySave.FullName, file.Name);
                                stopwatch = new Stopwatch();
                                stopwatch.Start();
                                RunProcess(Environment.CurrentDirectory + @"\Cryptosoft\CESI.Cryptosoft.EasySave.Project.exe", args);
                                stopwatch.Stop();
                            }
                            else
                            {
                                //non chiffré
                                file.CopyTo(Path.Combine(directorySave.FullName, file.Name), true);
                            }
                        }
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
                    propertiesWork[WorkProperties.EncryptDuration] = stopwatch.ElapsedMilliseconds;
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
        public void ScanSourceFolder()
        {
            //to be done
        }
        private IEnumerable<FileInfo> GetFilesFromFolder(DirectoryInfo dir)
        {
            IEnumerable<FileInfo> files = dir.GetFiles(
                "*.*",
                SearchOption.AllDirectories
                );
            return files;
        }

        private long GetFilesSize(IEnumerable<FileInfo> queryGetDifferenceFile)
        {
            long size = 0;
            foreach (FileInfo file in queryGetDifferenceFile)
            {
                size += file.Length;
            }
            return size;
        }


        public void RunProcess(string processName, string arguments)
        {
            Process process = new Process();
            process.StartInfo.FileName = processName;
            process.StartInfo.Arguments = arguments;
            process.Start();
            process.WaitForExit();
        }
    }
}
