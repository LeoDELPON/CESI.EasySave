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
        /// <summary>
        /// Taille du fichier.
        /// </summary>
        long folderSize;
        /// <summary>
        /// Liste des extensions des fichiers.
        /// </summary>
        public List<string> _cryptoExtension;
        /// <summary>
        /// Liste des fichiers prioritaires.
        /// </summary>
        public List<string> _priorityExtension;
        /// <summary>
        /// Nom du travail.
        /// </summary>
        public string workName;
        /// <summary>
        /// Clé.
        /// </summary>
        public string _key;

        /// <summary>
        /// Sauvegarde complète.
        /// </summary>
        /// <param name="props">Props</param>
        /// <param name="cryptoExtensions">Liste des extensions des fichiers</param>
        /// <param name="priorityExtensions">Extensions de fichiers prioritaires</param>
        /// <param name="key"></param>
        public Full(string props, List<string> cryptoExtensions, List<string> priorityExtensions, string key) : base()
        {
            idTypeSave ="ful";
            handler = DataHandler.Instance;
            TypeSave = SaveType.FULL;
            workName = props;
            _cryptoExtension = cryptoExtensions;
            _priorityExtension = priorityExtensions;
            _key = key;
        }

        /// <summary>
        /// Process de sauvegarde.
        /// </summary>
        /// <param name="sourceD">String du répertoire source</param>
        /// <param name="destD">String du répertoire de destination</param>
        /// <returns></returns>
        public override int SaveProcess(string sourceD, string destD)
        {
            int returnInfo = SUCCESS_OPERATION;
            if (!Directory.Exists(sourceD))
                throw new DirectoryNotFoundException(
                    "[-] Source directory has not been found: " + sourceD);

            DirectoryInfo dirSource = new DirectoryInfo(sourceD);
            DirectoryInfo dirDestination = new DirectoryInfo(destD);
            bool status = CopyAll(dirSource, dirDestination, false);

            //Vérifie si ça c'est bien passé
            if (!status)
            {
                //Retourne une erreur
                handler.OnStop(false);
                returnInfo = ERROR_OPERATION;
                return returnInfo;
            }

            //C'est réussi
            handler.OnStop(true);
            return returnInfo;
        }

        /// <summary>
        /// Copie de tous les fichiers.
        /// </summary>
        /// <param name="source">Répertoire source</param>
        /// <param name="target">Répertoire de destination</param>
        /// <param name="recursive">Booléan recursive</param>
        /// <returns></returns>
        public bool CopyAll(DirectoryInfo source, DirectoryInfo target, bool recursive)
        {
            DirectoryInfo fullSaveDirectory;

            //Vérifie le dossier cible
            if (!FolderBuilder.CheckFolder(target.ToString()))
            {
                FolderBuilder.CreateFolder(target.FullName);
            }
            if (!recursive)
            {
                fullSaveDirectory = new DirectoryInfo(target.ToString());
                fullSaveDirectory.CreateSubdirectory(source.Name).CreateSubdirectory("FullSaves");
                fullSaveDirectory = new DirectoryInfo(target.ToString() +  @"\" + source.Name + @"\FullSaves");
            }
            else
            {
                fullSaveDirectory = new DirectoryInfo(target.ToString());
            }
            
            //Numéro du fichier
            int fileNumber = GetFilesFromFolder(source.ToString()).Length;
            propertiesWork[WorkProperties.EligibleFiles] = fileNumber;

            //Récupère tous les fichierse
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
                
                //Pour tous les fichier dans la source
                foreach (FileInfo file in source.GetFiles())
                {
                    Console.WriteLine(@"[+] Copying {0}", file.Name);
                    
                    //Pour chaques extensions dans la source
                    foreach (string ext in _cryptoExtension)
                    {
                        byte[] tmpByte = File.ReadAllBytes(file.FullName);

                        //Vérifie les extensions
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
                    propertiesWork[WorkProperties.Duration] = DateTime.Now.ToString("ss-MM-hh");
                    propertiesWork[WorkProperties.EncryptDuration] = temp;
                    handler.OnNext(propertiesWork);
                }

                //Pour tous les répertoire source dans "source"
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
