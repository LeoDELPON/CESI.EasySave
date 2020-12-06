using CESI.BS.EasySave.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace CESI.BS.EasySave.BS
{
    internal class Differential : Save
    {
       
        private string FullBackupPath { get; set; }
        private string FolderToSave { get; set; }
        private string DiffBackupPath { get; set; }
        private string BackupPath { get; set; }
        private string WorkName { get; set; }
        private IList<string> _extensions;
        public string _key;

        public Differential(string props, IList<string> extensions, string key)
        {
            handler = DataHandler.Instance;
            idTypeSave  = "dif";
            TypeSave = SaveType.DIFFERENTIAL;
            WorkName = props;
            _extensions = extensions;
            _key = key;
        }

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
                    DiffBackupPath = DiffBackupPath + "\\" + DateTime.Now.ToString("dd_MM_yyyy");
                }
                
                propertiesWork[WorkProperties.Date] = DateTime.Now.ToString("dd_MM_yyyy");
                FolderBuilder.CreateFolder(DiffBackupPath);

                DirectoryInfo directorySource = new DirectoryInfo(sourceFolder);
                DirectoryInfo directoryFullSave = new DirectoryInfo(FullBackupPath);

                IEnumerable<FileInfo> listFileSource = GetFilesFromFolderBis(directorySource);
                IEnumerable<FileInfo> listFileFullSave = GetFilesFromFolderBis(directoryFullSave);

                FileCompare fileCompared = new FileCompare();

                var queryGetDifferenceFile = (from file in listFileSource select file).Except(listFileFullSave, fileCompared);

                propertiesWork[WorkProperties.EligibleFiles] = queryGetDifferenceFile.Count();
                long folderSize = getSizeOfDiff(queryGetDifferenceFile);
                propertiesWork[WorkProperties.Size] = folderSize;
                propertiesWork[WorkProperties.RemainingSize] = folderSize;
                handler = DataHandler.Instance;
                handler.Init((int)propertiesWork[WorkProperties.EligibleFiles], folderSize, WorkName, directoryToSaveName, DiffBackupPath);
                int i = 0;
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
                        foreach (string ext in _extensions)
                        {
                            byte[] tmpByte = File.ReadAllBytes(file.Name);
                            if (ext == GetExtension(file.Name))
                            {
                                string args = _key = " " + file.FullName;
                                string dataEncrypted = RunProcess(Environment.CurrentDirectory + @"\Cryptosoft\CESI.Cryptosoft.EasySave.Project.exe", args);
                                tmpByte = Encoding.ASCII.GetBytes(dataEncrypted);
                            }
                            File.WriteAllBytes(Path.Combine(pathTest, file.Name), tmpByte);
                        }
                        //file.CopyTo(Path.Combine(pathTest, file.Name), true);
                        Console.WriteLine("[+] Copying {0}", file.FullName);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("[-] An Error has occured while trying to copy Files : {0}", e);
                    }
                    i++;
                    propertiesWork[WorkProperties.RemainingFiles] = Convert.ToInt32(propertiesWork[WorkProperties.EligibleFiles]) - i;
                    folderSize = folderSize - file.Length;
                    propertiesWork[WorkProperties.RemainingSize] = folderSize;
                    handler.OnNext(propertiesWork[WorkProperties.RemainingFiles], propertiesWork[WorkProperties.RemainingSize]);
                }
                handler.OnStop(true);
                return returnInfo;
            }
            catch (Exception e)
            {
                returnInfo = ERROR_OPERATION;
                Console.WriteLine("[-] An error occured while trying to save : {0}", e);
                handler.OnStop(false);
                return returnInfo;
            }
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

        private IEnumerable<FileInfo> GetFilesFromFolderBis(DirectoryInfo dir)
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
            return "Dif"; // don't touch this it's useful
        }
    }
}


