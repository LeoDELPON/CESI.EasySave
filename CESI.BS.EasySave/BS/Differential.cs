using CESI.BS.EasySave.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CESI.BS.EasySave.BS
{
    internal class Differential : Save
    {
        private string fullBackupPath { get; set; }
        private string folderToSave { get; set; }
        private string diffBackupFolder { get; set; }
        private string commonPath { get; set; }




        public override int SaveProcess(string sourceFolder,
            string targetFolder)

        {
            int returnInfo = SUCCESS_OPERATION;
            try
            {
                
                fullBackupPath = targetFolder + @"\fullBackup\";

                DateTime durationStart = DateTime.Now;
                propertiesWork[WorkProperties.Date] = durationStart;


                //If Folder doesnt exist, create it.
                if (!FolderBuilder.CheckFolder(fullBackupPath))
                {
                    FolderBuilder.CreateFolder(fullBackupPath);
                }


               

                this.folderToSave = sourceFolder;
                this.diffBackupFolder = targetFolder;
                this.fullBackupPath = fullBackupPath;


                string[] allActualFiles = Directory.GetFiles(this.folderToSave, "*.*", SearchOption.AllDirectories);
                
                foreach (var file in allActualFiles)
                {
                    FileInfo actualFile = new FileInfo(file);                    
                    string actualFileDirectory = actualFile.DirectoryName;                   
                    FileInfo backupFile = new FileInfo(Path.GetFullPath(this.fullBackupPath + GetRelativePath(actualFile.ToString(), this.folderToSave)));
                    
                    if (!backupFile.Exists || backupFile.Length != actualFile.Length)
                    {
                        
                        string backupFolderWithRelativePath = Path.GetFullPath(this.diffBackupFolder + GetRelativePath(actualFileDirectory, this.folderToSave));
                        
                        if (!Directory.Exists(backupFolderWithRelativePath))
                        {

                            Directory.CreateDirectory(backupFolderWithRelativePath);
                        }
                        File.Copy(file, backupFolderWithRelativePath, true);
                    }
                }

                propertiesWork[WorkProperties.Duration] = DateTime.Now.Add(DateTime.Now.Subtract(durationStart)).Date;
                propertiesWork[WorkProperties.Size] = GetDirectorySize(this.diffBackupFolder);
                
                return returnInfo;

            }
            catch(Exception e)
            {
                returnInfo = ERROR_OPERATION;
                propertiesWork[WorkProperties.Duration] = -1;
                return returnInfo;
            }


        }
        


        public static string GetRelativePath(string fullPath, string basePath)
        {
            // Require trailing backslash for path
            if (!basePath.EndsWith(@"\"))
                basePath += @"\";

            Uri baseUri = new Uri(basePath);
            Uri fullUri = new Uri(fullPath);

            Uri relativeUri = baseUri.MakeRelativeUri(fullUri);

            // Uri's use forward slashes so convert back to backward slashes
            return relativeUri.ToString().Replace("/", @"\");

        }


        

        public override string GetName()
        {
            return Language.GetRequestedString(15);            
        }





        static long GetDirectorySize(string p){ 
            
            string[] a = Directory.GetFiles(p, "*.*");
            long b = 0;
            foreach (string name in a)
            {               
                FileInfo info = new FileInfo(name);
                b += info.Length;
            }            
            return b;
        }
    }
}


