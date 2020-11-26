using CESI.BS.EasySave.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CESI.BS.EasySave.BS
{
    internal class Differential : Save
    {
       
        private string FullBackupPath { get; set; }
        private string FolderToSave { get; set; }
        private string DiffBackupPath { get; set; }
        private string CommonPath { get; set; }
        private string BackupPath { get; set; }
        private string WorkName { get; set; }
        private DataHandler handler;

        public Differential(string props)
        {
            idTypeSave  = "dif";
            TypeSave = SaveType.DIFFERENTIAL;
            WorkName = props;
        }


        public override int SaveProcess(string sourceFolder,
            string targetFolder)

        {
            int returnInfo = SUCCESS_OPERATION;
            if (!Directory.Exists(sourceFolder))
                throw new DirectoryNotFoundException(
                    "[-] Source directory has not been found: " + sourceFolder);

            string directoryToSaveName = Path.GetDirectoryName(sourceFolder);
            FullBackupPath = targetFolder + @"\" + directoryToSaveName + @"\" + "FullSaves";
            DiffBackupPath = targetFolder + @"\" + directoryToSaveName + @"\" + "DiffSaves";
            BackupPath = targetFolder + @"\" + directoryToSaveName;
            FolderToSave = sourceFolder;

        /*    try
            {*/
                //test if save folder exist
                if (!FolderBuilder.CheckFolder(BackupPath))
                {                    
                    FolderBuilder.CreateFolder(targetFolder);
                    FolderBuilder.CreateFolder(FullBackupPath);
                    FolderBuilder.CreateFolder(DiffBackupPath);

                    Console.WriteLine("Attention : Il n'y a pas de FullSave encore crée, nous allons donc en faire une");

                    DiffBackupPath = FullBackupPath;

                }
                DateTime durationStart = DateTime.Now;
                propertiesWork[WorkProperties.Date] = durationStart;
                DiffBackupPath = DiffBackupPath + "\\" + DateTime.Now; 
                FolderBuilder.CreateFolder(DiffBackupPath);


                DirectoryInfo directorySource = new DirectoryInfo(sourceFolder);
                DirectoryInfo directoryFullSave = new DirectoryInfo(FullBackupPath);

                IEnumerable<FileInfo> listFileSource = GetFilesFromFolder(directorySource);
                IEnumerable<FileInfo> listFileFullSave = GetFilesFromFolder(directoryFullSave);

                FileCompare fileCompared = new FileCompare();

                var queryGetDifferenceFile = (from file in listFileSource select file).Except(listFileFullSave, fileCompared);


                propertiesWork[WorkProperties.EligibleFiles] = queryGetDifferenceFile.Count();
                long folderSize = getSizeOfDiff(queryGetDifferenceFile);
                propertiesWork[WorkProperties.Size] = folderSize;
                propertiesWork[WorkProperties.RemainingSize] = folderSize;
                handler = DataHandler.Instance;
                handler.Init((int)propertiesWork[WorkProperties.EligibleFiles], folderSize, WorkName, directoryToSaveName, DiffBackupPath);
                int i = 0;

                foreach (var file in queryGetDifferenceFile)
                {                             
                  
                    string backupFolderWithRelativePath = Path.GetFullPath(DiffBackupPath + GetRelativePath(file.ToString(), FolderToSave));
                        
                    if (!Directory.Exists(backupFolderWithRelativePath))
                    {

                        Directory.CreateDirectory(backupFolderWithRelativePath);
                    }
                    File.Copy(file.ToString(), backupFolderWithRelativePath, true);

                    
                    i++;
                    propertiesWork[WorkProperties.RemainingFiles] = Convert.ToInt32(propertiesWork[WorkProperties.EligibleFiles]) - i;
                    folderSize = folderSize - file.Length;
                    propertiesWork[WorkProperties.RemainingSize] = folderSize;
                    handler.OnNext(propertiesWork[WorkProperties.RemainingFiles], propertiesWork[WorkProperties.RemainingSize]);
                }

                handler.OnStop(true);
                propertiesWork[WorkProperties.Size] = GetDirectorySize(DiffBackupPath);
                return returnInfo;

            }/*
            catch(Exception e)
            {
                returnInfo = ERROR_OPERATION;
                Console.WriteLine("[-] An error occured while trying to save : {0}", e);
                handler.OnStop(false);
                return returnInfo;
            }*/


        }

        public static string GetRelativePath(string fullPath, string basePath)
        {
            // Require trailing backslash for path
            if (!basePath.EndsWith("\\"))
                basePath += "\\";

            Uri baseUri = new Uri(basePath);
            Uri fullUri = new Uri(fullPath);

            Uri relativeUri = baseUri.MakeRelativeUri(fullUri);

            // Uri's use forward slashes so convert back to backward slashes
            return relativeUri.ToString().Replace("/", "\\");

        }

       

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

        private IEnumerable<FileInfo> GetFilesFromFolder(DirectoryInfo dir)
        {
            IEnumerable<FileInfo> files = dir.GetFiles(
                "*.*",
                SearchOption.AllDirectories
                );
            return files;
        }


        private long getSizeOfDiff(IEnumerable<FileInfo> queryGetDifferenceFile)
        {
            long size = 0;
            foreach(FileInfo file in queryGetDifferenceFile)
            {
                size += file.Length;
            }
            return size;
        }

        public override string GetNameTypeWork()
        {
            return Language.GetRequestedString(15);
        }
    }
}


