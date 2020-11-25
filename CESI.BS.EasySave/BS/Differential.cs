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
        //must be relavtive
        private string commonPath { get; set; }
       



        public override int SaveProcess(string sourceFolder,
            string targetFolder)
        {
            fullBackupPath = targetFolder + @"\fullBackup\";
            if (!FolderBuilder.CheckFolder(fullBackupPath))
            {
                FolderBuilder.CreateFolder(fullBackupPath);
            }


            /////////////////////////////////////////////////////////////////////////////
            //réaranger les path en fonction de l'implémentation sinn normalement c'est ok
            /////////////////////////////////////////////////////////////////////////////
            
            this.folderToSave = sourceFolder;
            this.diffBackupFolder = targetFolder;
            this.fullBackupPath = fullBackupPath;
            string[] allActualFiles = Directory.GetFiles( this.folderToSave, "*.*", SearchOption.AllDirectories);
            //string[] allBackupFiles = Directory.GetFiles(Path.Combine(this.fullBackupPath , this.folderToSave), "*.*", SearchOption.AllDirectories);
            foreach (var file in allActualFiles)
            {
                FileInfo actualFile = new FileInfo(file);
                //Console.WriteLine(actualFile);
                string actualFileDirectory = actualFile.DirectoryName;

                //Console.WriteLine(Path.GetFullPath(this.commonPath + this.fullBackupPath + GetRelativePath(actualFile.ToString(), this.commonPath + this.folderToSave)));
                FileInfo backupFile = new FileInfo(Path.GetFullPath( this.fullBackupPath + GetRelativePath(actualFile.ToString(), this.folderToSave)));
                Console.WriteLine(backupFile);
                if (!backupFile.Exists || backupFile.Length != actualFile.Length)
                {
                    Console.WriteLine("jevaisla");
                    //Console.WriteLine(actualFileDirectory);
                    //Console.WriteLine(GetRelativePath(actualFileDirectory, this.commonPath + this.folderToSave));
                    //Console.WriteLine(Path.GetFullPath(this.commonPath + this.diffBackupFolder + GetRelativePath(actualFileDirectory, this.commonPath + this.folderToSave)));
                    if (!Directory.Exists(Path.GetFullPath(this.diffBackupFolder + GetRelativePath(actualFileDirectory,  this.folderToSave))))
                    {

                        Directory.CreateDirectory(Path.GetFullPath( this.diffBackupFolder + GetRelativePath(actualFileDirectory,  this.folderToSave)));
                    }
                    File.Copy(file, Path.GetFullPath( this.diffBackupFolder + GetRelativePath(actualFile.ToString(), this.folderToSave)), true);
                }
            }
            return 0;
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
    }
}

