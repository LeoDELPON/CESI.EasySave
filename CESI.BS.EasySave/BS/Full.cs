using CESI.BS.EasySave.DAL;
using CESI.BS.EasySave.BS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;

namespace CESI.BS.EasySave.BS
{
    internal class Full : Save
    {
        public List<long> dirSize = new List<long>();
        public DirectoryInfo fullSaveDirectory;
        public Full() : base()
        {
            TypeSave = SaveType.FULL;
        }

        public override int SaveProcess(string sourceD, string destD)
        {
            int returnInfo = SUCCESS_OPERATION;
            DateTime durationStart = DateTime.Now;
            //propertiesWork[WorkProperties.Date] = durationStart;
            if (!Directory.Exists(sourceD))
                throw new DirectoryNotFoundException(
                    "[-] Source directory has not been found: " + sourceD);

            DirectoryInfo dirSource = new DirectoryInfo(sourceD);
            DirectoryInfo dirDestination = new DirectoryInfo(destD);
            bool status = CopyAll(dirSource, dirDestination);
            if (!status)
            {
                propertiesWork[WorkProperties.Duration] = -1;

                returnInfo = ERROR_OPERATION;
                return returnInfo;
            }

            propertiesWork[WorkProperties.Duration] = DateTime.Now.Add(DateTime.Now.Subtract(durationStart)).Date;
            return returnInfo;
        }

        public bool CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            if (!FolderBuilder.CheckFolder(target.ToString()))
            {
                FolderBuilder.CreateFolder(target.FullName);
                fullSaveDirectory = new DirectoryInfo(target.ToString());
                fullSaveDirectory.CreateSubdirectory(source.FullName).CreateSubdirectory("FullSaves");
            }
                
            int fileNumber = GetFilesFromFolder(source.ToString()).Length;
            propertiesWork[WorkProperties.EligibleFiles] = fileNumber;

            long folderSize = GetFolderSize(source.ToString());
            propertiesWork[WorkProperties.Size] = folderSize;

            try
            {
                foreach (FileInfo file in source.GetFiles())
                {
                    Console.WriteLine(@"[+] Copying {0}\{1}", target.FullName, file.Name);
                    dirSize.Add(file.Length);
                    file.CopyTo(Path.Combine(fullSaveDirectory.FullName, file.Name), true);
                    propertiesWork[WorkProperties.RemainingSize] = folderSize - file.Length;
                }

                foreach (DirectoryInfo directorySourceSubDir in source.GetDirectories())
                {
                    DirectoryInfo nextTargetSubDir =
                        fullSaveDirectory.CreateSubdirectory(directorySourceSubDir.Name);
                    propertiesWork[WorkProperties.RemainingFiles] = fileNumber - 1;
                    CopyAll(directorySourceSubDir, nextTargetSubDir);
                }
                
                return true;
            } catch(SecurityException e)
            {
                Console.WriteLine("[-] While tryin to copy a file from source to destination," +
                    "an error occured because of the right access : {0}", e);
                return false;
            }
        }
        public override string GetName()
        {
            return Language.GetRequestedString(16);
        }
    }
}
