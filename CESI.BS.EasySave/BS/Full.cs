using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CESI.BS.EasySave.BS
{
    internal class Full : Save
    {
        public Full()
        {

        }

        public override void SaveProcess(string sourceD, string destD, string dirName)
        {
            var dirSource = new DirectoryInfo(sourceD);
            var dirDestination = new DirectoryInfo(destD);

        }

        private static void CopyAll(DirectoryInfo source, DirectoryInfo target)
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
    }
}
