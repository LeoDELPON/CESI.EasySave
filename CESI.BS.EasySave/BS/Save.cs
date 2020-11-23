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


        public abstract int SaveFile(string sourceFile, string target);


        protected void CreateFolder()
        {

        }

        protected void CreateFile()
        {

        }

        protected string[] GetFilesFromFolder(string path)
        {
            string[] files = Directory.GetFiles(
                path,
                "*.*",
                SearchOption.AllDirectories
                );
            return files;
        }

        protected string GetExtension(string path)
        {
            string[] pathFile = path.Split('.');
            return pathFile[pathFile.Length - 1];
        }
        
    }
}