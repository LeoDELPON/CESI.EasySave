using CESI.BS.EasySave.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;

namespace CESI.BS.EasySave.BS
{
    internal class Full : Save
    {
        public Full() : base()
        {
            TypeSave = SaveType.FULL;
        }
        
        public override void SaveProcess(string sourceD, string destD)
        {
            int returnInfo = SUCCESS_OPERATION;
            propertiesWork[WorkProperties.Date] = DateTime.Now;

            var dirSource = new DirectoryInfo(sourceD);
            var dirDestination = new DirectoryInfo(destD);
            CopyAll(dirSource, dirDestination);
        }

        private static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            FolderBuilder.CreateFolder(target.FullName);
            try
            {
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
            } catch(SecurityException e)
            {
                Console.WriteLine("[-] While tryin to copy a file from source to destination," +
                    "an error occured because of the right access : {0}", e);
            }
        }
    }
}
