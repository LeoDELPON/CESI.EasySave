﻿using CESI.BS.EasySave.DAL;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;


namespace CESI.BS.EasySave.BS
{
    public abstract class Save
    {
        public SaveType TypeSave { get; protected set; }
        protected Dictionary<WorkProperties, object> propertiesWork;
        public string IdTypeSave { get; set; }
        public DataHandler handler = new DataHandler();
        public static int SUCCESS_OPERATION = 0;
        public static int ERROR_OPERATION = 1;
        public static int IN_PROGRESS = 2;
        public static int STOP_SAVE = 3;
        public static int NO_FULL_SAVE = 4;
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
        
        public abstract bool SaveProcess(string sourceDirectory, 
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

        public void RunProcess(string processName, string arguments)
        {
            Process process = new Process();
            process.StartInfo.FileName = processName;
            process.StartInfo.Arguments = arguments;
            process.Start();
            process.WaitForExit();
        }
    }
}