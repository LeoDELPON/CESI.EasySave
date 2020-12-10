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
        /// Fichier à sauvegarder
        /// </summary>
        private string FolderToSave { get; set; }
        /// <summary>
        /// Chemins pour sauvegarde différentiel.
        /// </summary>
        private string DiffBackupPath { get; set; }
        /// <summary>
        /// Chemin d'une sauvegarde.
        /// </summary>
        private string BackupPath { get; set; }
        /// <summary>
        /// Nom du travail.
        /// </summary>
        private string WorkName { get; set; }
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
            handler = DataHandler.Instance;
            idTypeSave  = "dif";
            TypeSave = SaveType.DIFFERENTIAL;
            WorkName = props;
            _extensions = extensions;
            _key = key;
        }

        /// <summary>
        /// Création d'une sauvegarde différentielle
        /// </summary>
        /// <param name="sourceFolder">Fichier source</param>
        /// <param name="targetFolder">Fichier de destination</param>
        /// <returns></returns>
        public override int SaveProcess(string sourceFolder, string targetFolder)
        {
            int returnInfo = SUCCESS_OPERATION;
            if (!Directory.Exists(sourceFolder))
                throw new DirectoryNotFoundException(
                    "[-] Source directory has not been found: " + sourceFolder);
            string directoryToSaveName = new DirectoryInfo(sourceFolder).Name;
            FullBackupPath = targetFolder + @"\" + directoryToSaveName + @"\" + "FullSaves";
            DiffBackupPath = targetFolder + @"\" + directoryToSaveName + @"\" + "DiffSaves";
            BackupPath = targetFolder + @"\" + directoryToSaveName;
            FolderToSave = sourceFolder;
            try
            {
                //regarde si le fichier destination est vide (signifie que c'est la premiere sauvegarde.)
                if (!FolderBuilder.CheckFolder(BackupPath))
                {
                    FolderBuilder.CreateFolder(targetFolder);
                    FolderBuilder.CreateFolder(FullBackupPath);
                    FolderBuilder.CreateFolder(DiffBackupPath);
                    Console.WriteLine("[+] Warning, Full Save not created, Full Save in creating");
                    DiffBackupPath = FullBackupPath;
                }
                else
                {
                    DiffBackupPath += @"\" + DateTime.Now.ToString("dd_MM_yyyy");
                }
                
                propertiesWork[WorkProperties.Date] = DateTime.Now.ToString("dd_MM_yyyy");
                FolderBuilder.CreateFolder(DiffBackupPath);

                DirectoryInfo directorySource = new DirectoryInfo(sourceFolder);
                DirectoryInfo directoryFullSave = new DirectoryInfo(FullBackupPath);

                IEnumerable<FileInfo> listFileSource = GetFilesFromFolderBis(directorySource);
                IEnumerable<FileInfo> listFileFullSave = GetFilesFromFolderBis(directoryFullSave);

                var queryGetDifferenceFile = (from file in listFileSource select file).Except(listFileFullSave, FileCompare.Instance);

                //premiere assignation de valeur et instanciation de data handler
                propertiesWork[WorkProperties.EligibleFiles] = queryGetDifferenceFile.Count();
                long folderSize = getSizeOfDiff(queryGetDifferenceFile);
                propertiesWork[WorkProperties.Size] = folderSize;
                propertiesWork[WorkProperties.RemainingSize] = folderSize;
                handler = DataHandler.Instance;
                handler.Init((int)propertiesWork[WorkProperties.EligibleFiles], folderSize, WorkName, directoryToSaveName, DiffBackupPath);
                int i = 0;
                double temp = -1;
                foreach (FileInfo file in queryGetDifferenceFile)
                {
                    string backupFolderWithRelativePath = Path.GetFullPath(DiffBackupPath, FolderToSave);
                    string pathTest = file.DirectoryName;
                    pathTest = pathTest.Replace(sourceFolder, backupFolderWithRelativePath);
                    try
                    {
                        if (!FolderBuilder.CheckFolder(pathTest))
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
                    folderSize -= file.Length;
                    propertiesWork[WorkProperties.RemainingSize] = folderSize;
                    propertiesWork[WorkProperties.Duration] = DateTime.Now.ToString("ss-MM-hh");
                    propertiesWork[WorkProperties.EncryptDuration] = temp;
                    handler.OnNext(propertiesWork);
                }
                handler.OnStop(true);
                return returnInfo;
            }
            catch (Exception e)
            {
                //gestion des erreurs
                returnInfo = ERROR_OPERATION;
                Console.WriteLine("[-] An error occured while trying to save : {0}", e);
                handler.OnStop(false);
                return returnInfo;
            }
        }

        /// <summary>
        /// calcul la taille du dossier
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public long GetDirectorySize(string p)
        {
            string[] a = Directory.GetFiles(p, "*.*");
            long b = 0;
            foreach (string name in a)
            {
                FileInfo info = new FileInfo(name);
                b += info.Length;
            }
            return b;
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
        private long getSizeOfDiff(IEnumerable<FileInfo> queryGetDifferenceFile)
        {
            long size = 0;
            foreach(FileInfo file in queryGetDifferenceFile)
            {
                size += file.Length;
            }
            return size;
        }

        /// <summary>
        /// une fonction de marcus le fdp
        /// </summary>
        /// <returns></returns>
        public override string GetNameTypeWork()
        {
            return "Dif"; // don't touch this it's useful
        }
    }
}


