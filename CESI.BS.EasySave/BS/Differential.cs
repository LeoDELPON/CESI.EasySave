using CESI.BS.EasySave.DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace CESI.BS.EasySave.BS
{
    internal class Differential : Save
    {
       /// <summary>
       /// Chemins pour sauvegarde complète.
       /// </summary>
        private string FullBackupPath { get; set; }
        /// <summary>
        /// Chemins pour sauvegarde différentiel.
        /// </summary>
        private string DiffBackupPath { get; set; }
        /// <summary>
        /// taille des fichiers.
        /// </summary>
        private long FolderSize { get; set; }
        /// <summary>
        /// Chemin d'une sauvegarde.
        /// </summary>
        private string BackupPath { get; set; }
        /// <summary>
        /// Nom du travail.
        /// </summary>
        private readonly IList<string> _extensions;
        public string _key;


        /// <summary>
        /// Sauvegarde différentielle.
        /// </summary>
        /// <param name="props"></param>
        /// <param name="extensions">Liste des extensions des fichiers</param>
        /// <param name="key"></param>
        public Differential(string props, List<string> extensions, string key)
        {
            IdTypeSave  = "dif";
            TypeSave = SaveType.DIFFERENTIAL;
            propertiesWork[WorkProperties.Name] = props;
            _extensions = extensions;
            _key = key;
        }

        /// <summary>
        /// Création d'une sauvegarde différentielle
        /// </summary>
        /// <param name="sourceFolder">Fichier source</param>
        /// <param name="targetFolder">Fichier de destination</param>
        /// <returns></returns>
        public override bool SaveProcess(string sourceFolder, string targetFolder)
        {
            handler.GetStopwatch.Start();
            if (!Directory.Exists(sourceFolder))
            {
                throw new DirectoryNotFoundException("[-] Source directory has not been found: " + sourceFolder);
            }
            DirectoryInfo directorySource = new DirectoryInfo(sourceFolder);
            BackupPath = targetFolder + @"\" + directorySource.Name;
            FullBackupPath = BackupPath + @"\FullSaves";
            DiffBackupPath = BackupPath + @"\DiffSaves";
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

            DirectoryInfo directoryFullSave = new DirectoryInfo(FullBackupPath);

            IEnumerable<FileInfo> listFileSource = GetFilesFromFolderBis(directorySource);
            IEnumerable<FileInfo> listFileFullSave = GetFilesFromFolderBis(directoryFullSave);

            try
            {
                var queryGetDifferenceFile = (from file in listFileSource select file).Except(listFileFullSave, FileCompare.Instance);

                //premiere assignation de valeur et instanciation de data handler
                propertiesWork[WorkProperties.Target] = DiffBackupPath;
                propertiesWork[WorkProperties.EligibleFiles] = queryGetDifferenceFile.Count();
                propertiesWork[WorkProperties.Size] = FolderSize = GetSizeOfDiff(queryGetDifferenceFile);
                propertiesWork[WorkProperties.Source] = sourceFolder;
                handler.Init(propertiesWork);
                int i = 0;
                double temp = -1;
                foreach (FileInfo file in queryGetDifferenceFile)
                {
                    WaitForUnpause();
                    checkFileSize(file.Length);
                    string backupFolderWithRelativePath = Path.GetFullPath(DiffBackupPath, propertiesWork[WorkProperties.Source].ToString());
                    string pathTest = file.DirectoryName;
                    pathTest = pathTest.Replace(sourceFolder, backupFolderWithRelativePath);
                    try
                    {
                        if (!Directory.Exists(pathTest))
                        {
                            FolderBuilder.CreateFolder(pathTest);
                        }
                        // parti chiffrage
                        foreach (string ext in _extensions)
                        {

                            byte[] tmpByte = File.ReadAllBytes(file.FullName);
                            if (ext == file.Extension)
                            {
                                //chiffré
                                string args = _key + " " + file.FullName + " " + Path.Combine(pathTest, file.Name);
                                Stopwatch stopW2 = new Stopwatch();
                                stopW2.Start();
                                RunProcess(Environment.CurrentDirectory + @"\Cryptosoft\CESI.Cryptosoft.EasySave.Project.exe", args);
                                stopW2.Stop();
                                temp = stopW2.ElapsedMilliseconds;
                            } else
                            {
                                //non chiffré
                                file.CopyTo(Path.Combine(pathTest, file.Name), true);
                            }
                        }
                        Console.WriteLine("[+] Copying {0}", file.FullName);
                    }
                    //gestion des erreurs
                    catch (Exception e)
                    {
                        Console.WriteLine("[-] An Error has occured while trying to copy Files : {0}", e);
                    }
                    i++;
                    //assignation de valeur du dictionnaire
                    propertiesWork[WorkProperties.RemainingFiles] = Convert.ToInt32(propertiesWork[WorkProperties.EligibleFiles]) - i;
                    FolderSize -= file.Length;
                    propertiesWork[WorkProperties.RemainingSize] = FolderSize;
                    propertiesWork[WorkProperties.EncryptDuration] = temp;
                    NotifyAll(handler.OnNext(propertiesWork));
                    EndReact();
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
        /// <summary>
        /// trouve les fichiers du dossier.
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private IEnumerable<FileInfo> GetFilesFromFolderBis(DirectoryInfo dir)
        {
            IEnumerable<FileInfo> files = dir.GetFiles(
                "*.*",
                SearchOption.AllDirectories
                );
            return files;
        }

        /// <summary>
        /// calculer la taille des dossiers a manipuler
        /// </summary>
        /// <param name="queryGetDifferenceFile"></param>
        /// <returns></returns>
        private long GetSizeOfDiff(IEnumerable<FileInfo> queryGetDifferenceFile)
        {
            long size = 0;
            foreach(FileInfo file in queryGetDifferenceFile)
            {
                size += file.Length;
            }
            return size;
        }
    }
}


