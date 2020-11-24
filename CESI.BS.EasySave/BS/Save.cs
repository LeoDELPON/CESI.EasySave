using CESI.BS.EasySave.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CESI.BS.EasySave.BS
{
    internal abstract class Save
    {

        protected Dictionary<WorkProperties, object> propertiesWork;

        public Save()
        {
            propertiesWork = new Dictionary<WorkProperties, object>();
            propertiesWork.Add(WorkProperties.Duration, 0);
            propertiesWork.Add(WorkProperties.Date, null);
            propertiesWork.Add(WorkProperties.CurrentFile, "notSet");
            propertiesWork.Add(WorkProperties.EligibleFiles, 0);
            propertiesWork.Add(WorkProperties.RemainingFiles, 0);
            propertiesWork.Add(WorkProperties.RemainingSize, 0);
            propertiesWork.Add(WorkProperties.Size, 0);
        }

        public abstract void SaveProcess(string sourceDirectory, 
            string destinationDirectory, 
            string directoryName
            );

        protected string[] GetFilesFromFolder(string path)
        {
            string[] files = Directory.GetFiles(
                path,
                "*.*",
                SearchOption.AllDirectories
                );
            return files;
        }

        protected void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            FolderBuilder.CreateFolder(target.FullName);
            foreach (FileInfo file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(target.FullName, file.Name), true);
            }

            foreach (DirectoryInfo directorySourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(directorySourceSubDir.Name);
                CopyAll(directorySourceSubDir, nextTargetSubDir);
            }
        }

        protected string GetExtension(string path)
        {
            string[] pathFile = path.Split('.');
            return pathFile[pathFile.Length - 1];
        }
        
    }
}