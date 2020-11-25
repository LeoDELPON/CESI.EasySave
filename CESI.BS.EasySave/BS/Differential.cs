﻿using CESI.BS.EasySave.DAL;
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


        public override int SaveProcess(string sourceDirectory, string destinationDirectory)
        {
            throw new NotImplementedException();
        }

        public override void SaveProcess(string folderToSave,
            string diffBackupFolder, string fullBackupPath)
        {

            /////////////////////////////////////////////////////////////////////////////
            //réaranger les path en fonction de l'implémentation sinn normalement c'est ok
            /////////////////////////////////////////////////////////////////////////////
            this.commonPath = "C:\\Users\\Kylian\\Desktop";
            this.folderToSave = folderToSave;
            this.diffBackupFolder = diffBackupFolder;
            this.fullBackupPath = fullBackupPath;
            string[] allActualFiles = Directory.GetFiles(this.commonPath + this.folderToSave, "*.*", SearchOption.AllDirectories);
            //string[] allBackupFiles = Directory.GetFiles(Path.Combine(this.fullBackupPath , this.folderToSave), "*.*", SearchOption.AllDirectories);
            foreach (var file in allActualFiles)
            {
                FileInfo actualFile = new FileInfo(file);
                //Console.WriteLine(actualFile);
                string actualFileDirectory = actualFile.DirectoryName;

                //Console.WriteLine(Path.GetFullPath(this.commonPath + this.fullBackupPath + GetRelativePath(actualFile.ToString(), this.commonPath + this.folderToSave)));
                FileInfo backupFile = new FileInfo(Path.GetFullPath(this.commonPath + this.fullBackupPath + GetRelativePath(actualFile.ToString(), this.commonPath + this.folderToSave)));
                Console.WriteLine(backupFile);
                if (!backupFile.Exists || backupFile.Length != actualFile.Length)
                {
                    Console.WriteLine("jevaisla");
                    //Console.WriteLine(actualFileDirectory);
                    //Console.WriteLine(GetRelativePath(actualFileDirectory, this.commonPath + this.folderToSave));
                    //Console.WriteLine(Path.GetFullPath(this.commonPath + this.diffBackupFolder + GetRelativePath(actualFileDirectory, this.commonPath + this.folderToSave)));
                    if (!Directory.Exists(Path.GetFullPath(this.commonPath + this.diffBackupFolder + GetRelativePath(actualFileDirectory, this.commonPath + this.folderToSave))))
                    {

                        Directory.CreateDirectory(Path.GetFullPath(this.commonPath + this.diffBackupFolder + GetRelativePath(actualFileDirectory, this.commonPath + this.folderToSave)));
                    }
                    File.Copy(file, Path.GetFullPath(this.commonPath + this.diffBackupFolder + GetRelativePath(actualFile.ToString(), this.commonPath + this.folderToSave)), true);
                }

        override
        public string getName()
        {
            return Language.getDifferentialName();
        }

    }
}
