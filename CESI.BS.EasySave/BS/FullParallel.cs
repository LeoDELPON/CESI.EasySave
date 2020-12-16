using CESI.BS.EasySave.DAL;
using CESI.BS.EasySave.BS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace CESI.BS.EasySave.BS
{
    internal class FullParallel : Save
    {
        /// <summary>
        /// Taille du dossier.
        /// </summary>
        long FolderSize { get; set; }
        /// <summary>
        /// Liste des extensions des fichiers.
        /// </summary>
        public List<string> _cryptoExtension = new List<string>();
        /// <summary>
        /// Liste des fichiers prioritaires.
        /// </summary>
        ///  public List<string> _priorityExtension;

        /// <summary>
        /// Clé.
        /// </summary>
        public string _key;

        public Mutex mutex;

        /// <summary>
        /// Sauvegarde complète.
        /// </summary>
        /// <param name="props">Props</param>
        /// <param name="cryptoExtensions">Liste des extensions des fichiers</param>
        /// <param name="priorityExtensions">Extensions de fichiers prioritaires</param>
        /// <param name="key"></param>
        public FullParallel(string props, List<string> cryptoExtensions, string key) : base()
        {
            IdTypeSave = "ful";
            TypeSave = SaveType.FULL;
            propertiesWork[WorkProperties.TypeSave] = "Full";
            propertiesWork[WorkProperties.Name] = props;
            _cryptoExtension = cryptoExtensions;
            //   _priorityExtension = priorityExtensions;
            _key = key;
            mutex = new Mutex();
        }

        /// <summary>
        /// Process de sauvegarde.
        /// </summary>
        /// <param name="sourceFolder">String du répertoire source</param>
        /// <param name="targetFolder">String du répertoire de destination</param>
        /// <returns></returns>
        public override bool SaveProcess(string sourceFolder, string targetFolder)
        {
            handler.GetStopwatch.Start();
            if (!Directory.Exists(sourceFolder))
            {
                throw new DirectoryNotFoundException(
                    "[-] Source directory has not been found: " + sourceFolder);
            }

            propertiesWork[WorkProperties.Source] = sourceFolder;
            propertiesWork[WorkProperties.Target] = targetFolder;
            propertiesWork[WorkProperties.EligibleFiles] = GetFilesFromFolder(sourceFolder).Length;
            propertiesWork[WorkProperties.Size] = FolderSize = GetFolderSize(sourceFolder);
            handler.Init(propertiesWork);
            DirectoryInfo dirSource = new DirectoryInfo(sourceFolder);
            DirectoryInfo dirDestination = new DirectoryInfo(targetFolder);
            bool status = CopyAll(dirSource, dirDestination, false);

            //Vérifie si ça c'est bien passé
            if (!status)
            {
                //Retourne une erreur
                handler.OnStop(false);
                return false;
            }

            //C'est réussi
            handler.OnStop(true);
            return true;
        }

        /// <summary>
        /// Copie de tous les fichiers.
        /// </summary>
        /// <param name="source">Répertoire source</param>
        /// <param name="target">Répertoire de destination</param>
        /// <returns></returns>
        public bool CopyAll(DirectoryInfo source, DirectoryInfo target, bool isRecursive)
        {

            DirectoryInfo fullSaveDirectory;
            //Vérifie le dossier cible
            string path = target.ToString() + @"\" + propertiesWork[WorkProperties.Name] + "_" + source.Name.ToString() + @"\FullSaves" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
            if (!Directory.Exists(path) && !isRecursive)
            {
                FolderBuilder.CreateFolder(path);
                fullSaveDirectory = new DirectoryInfo(path);
            }
            else
            {
                fullSaveDirectory = new DirectoryInfo(target.ToString());
            }
            try
            {
                double temp = -1;
                string[] files = GetFilesFromFolder(source.FullName);
                Parallel.For(0, files.Length, (i) =>
                {                  
                    WaitForUnpause();
                    FileInfo fileObject = new FileInfo(files[i]);
                    checkFileSize(fileObject.Length);
                    string dir = ReplaceLastOccurrence(files[i].Replace(source.FullName, fullSaveDirectory.FullName), fileObject.Name, "");
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }

                    Parallel.ForEach(_cryptoExtension, element =>
                    {
                        if (fileObject.Extension == element)
                        {
                            Stopwatch stopW2 = new Stopwatch();
                            CryptoSoft(_key, fileObject.FullName, files[i].Replace(source.FullName, fullSaveDirectory.FullName));
                            stopW2.Stop();
                            temp = stopW2.ElapsedMilliseconds;
                        }
                        else
                        {
                            try
                            {
                                mutex.WaitOne();
                                fileObject.CopyTo(files[i].Replace(source.FullName, fullSaveDirectory.FullName), true);
                                mutex.ReleaseMutex();
                            }
                            catch (ThreadInterruptedException)
                            {
                                Thread.CurrentThread.Interrupt();
                            }
                        }



                    });


                    propertiesWork[WorkProperties.RemainingFiles] = Convert.ToInt32(propertiesWork[WorkProperties.EligibleFiles]) - 1;
                    FolderSize -= fileObject.Length;
                    propertiesWork[WorkProperties.RemainingSize] = FolderSize;
                    propertiesWork[WorkProperties.EncryptDuration] = temp;
                    NotifyAll(handler.OnNext(propertiesWork));
                    EndReact();

                });




                return true;
            }
            catch (SecurityException e)
            {
                Console.WriteLine("[-] While tryin to copy a file from source to destination," +
                    "an error occured because of the right access : {0}", e);
                return false;
            }
        }
    }
}
