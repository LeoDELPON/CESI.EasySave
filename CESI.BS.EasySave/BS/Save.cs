using CESI.BS.EasySave.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;


namespace CESI.BS.EasySave.BS
{
    public abstract class Save
    {
        public SaveType TypeSave { get; protected set; }
        protected Dictionary<WorkProperties, object> propertiesWork;

        public static int SUCCESS_OPERATION = 0;
        public static int ERROR_OPERATION = 1;
        public static int IN_PROGRESS = 2;
        public static int STOP_SAVE = 3;
        public Save()
        {
            propertiesWork = new Dictionary<WorkProperties, object>
            {
                { WorkProperties.Duration, 0 },
                { WorkProperties.Date, null },
                { WorkProperties.CurrentFile, "notSet" },
                { WorkProperties.Progress, 0 },
                { WorkProperties.EligibleFiles, 0 },
                { WorkProperties.RemainingFiles, 0 },
                { WorkProperties.RemainingSize, 0 },
                { WorkProperties.Size, 0 }
            };
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> Making a method that will be overrided by other classes </summary>
        ///
        /// <remarks>   Leo , 24/11/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        
        public abstract int SaveProcess(string sourceDirectory, 
            string destinationDirectory
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

        protected string GetExtension(string path)
        {
            string[] pathFile = path.Split('.');
            return pathFile[pathFile.Length - 1];
        }
        public abstract string GetName();

      
        protected long GetFolderSize(string path)
        {
            long size = 0;
            string[] files = GetFilesFromFolder(path);
            foreach(string f in files)
            {
                FileInfo info = new FileInfo(f);
                size += info.Length;
            }
            return size;
        }
    }
}