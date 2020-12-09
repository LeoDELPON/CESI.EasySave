using CESI.BS.EasySave.DAL;
using CESI.BS.EasySave.BS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Diagnostics;

namespace CESI.BS.EasySave.BS
{
    internal class Full : Save
    {

        long folderSize;
        public List<string> _extension;
        public string workName;
        public string _key;

        public Full(string props, List<string> extensions, string key) : base()
        {
            idTypeSave ="ful";
            handler = DataHandler.Instance;
            TypeSave = SaveType.FULL;
            workName = props;
            _extension = extensions;
            _key = key;
        }

        public override int SaveProcess(string sourceD, string destD)
        {
            int returnInfo = SUCCESS_OPERATION;
            if (!Directory.Exists(sourceD))
                throw new DirectoryNotFoundException(
                    "[-] Source directory has not been found: " + sourceD);

            DirectoryInfo dirSource = new DirectoryInfo(sourceD);
            DirectoryInfo dirDestination = new DirectoryInfo(destD);
            bool status = CopyAll(dirSource, dirDestination, false);
            if (!status)
            {
                handler.OnStop(false);
                returnInfo = ERROR_OPERATION;
                return returnInfo;
            }
            handler.OnStop(true);
            return returnInfo;
        }

        public bool CopyAll(DirectoryInfo source, DirectoryInfo target, bool recursive)
        {
            DirectoryInfo fullSaveDirectory;
            if (!FolderBuilder.CheckFolder(target.ToString()))
            {
                FolderBuilder.CreateFolder(target.FullName);
            }
            if (!recursive)
            {
                fullSaveDirectory = new DirectoryInfo(target.ToString());
                fullSaveDirectory.CreateSubdirectory(source.Name).CreateSubdirectory("FullSaves");
                fullSaveDirectory = new DirectoryInfo(target.ToString() + "\\" + source.Name + "\\FullSaves");
            }
            else
            {
                fullSaveDirectory = new DirectoryInfo(target.ToString());
            }
            int fileNumber = GetFilesFromFolder(source.ToString()).Length;
            propertiesWork[WorkProperties.EligibleFiles] = fileNumber;
            if (!recursive)
            {
                folderSize = GetFolderSize(source.ToString());
                propertiesWork[WorkProperties.Size] = folderSize;
                handler = DataHandler.Instance;
                handler.Init(fileNumber, folderSize, workName, source.Name, target.Name);
            }
            try
            {
                double temp = -1;
      
                foreach (FileInfo file in source.GetFiles())
                {
                    Console.WriteLine(@"[+] Copying {0}", file.Name);
                    foreach (string ext in _extension)
                    {
                        byte[] tmpByte = File.ReadAllBytes(file.FullName);
                        if (ext == file.Extension)
                        {
                            string arguments = _key + " " + file.FullName + " " + Path.Combine(fullSaveDirectory.FullName, file.Name);
                            Stopwatch stopW2 = new Stopwatch();
                            stopW2.Start();
                            RunProcess(Environment.CurrentDirectory + @"\Cryptosoft\CESI.Cryptosoft.EasySave.Project.exe", arguments);
                            stopW2.Stop();
                            temp = stopW2.ElapsedMilliseconds;
                        } else
                        {
                            file.CopyTo(Path.Combine(fullSaveDirectory.FullName, file.Name), true);
                        }

                    }
                    propertiesWork[WorkProperties.RemainingFiles] = fileNumber - 1;
                    folderSize = folderSize - file.Length;
                    propertiesWork[WorkProperties.RemainingSize] = folderSize;
                    propertiesWork[WorkProperties.Duration] = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
                    propertiesWork[WorkProperties.EncryptDuration] = temp;
                    handler.OnNext(propertiesWork);
                }

                foreach (DirectoryInfo directorySourceSubDir in source.GetDirectories())
                {
                    DirectoryInfo nextTargetSubDir =
                        fullSaveDirectory.CreateSubdirectory(directorySourceSubDir.Name);
                    Console.WriteLine("nextTarget = " + nextTargetSubDir +" \nnextDirectory = " + directorySourceSubDir);
                    CopyAll(directorySourceSubDir, nextTargetSubDir, true);
                }
                return true;
            } catch(SecurityException e)
            {
                Console.WriteLine("[-] While tryin to copy a file from source to destination," +
                    "an error occured because of the right access : {0}", e);
                return false;
            }
        }
        public override string GetNameTypeWork()
        {
            return "Ful";  // don't touch this it's useful
        }
    }
}
