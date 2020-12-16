using CESI.BS.EasySave.DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace CESI.BS.EasySave.BS
{
    internal class Differential : Save
    {
        /// <summary>
        /// Sauvegarde différentielle.
        /// </summary>
        /// <param name="props"></param>
        /// <param name="extensions">Liste des extensions des fichiers</param>
        /// <param name="key"></param>
        public Differential(string props, List<string> cryptoExtensions, List<string> priorityExtensions, string key)
        {
            IdTypeSave  = "dif";
            TypeSave = SaveType.DIFFERENTIAL;
            propertiesWork[WorkProperties.Name] = props;
            _cryptoExtension = cryptoExtensions;
            _priorityExtension = priorityExtensions;
            _key = key;
        }

        public override string CheckSaveFile(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Console.WriteLine("[+] Warning, no existing save, a full save is being processed");
                return @"\Full";
            }
            else
            {
                return @"\Diff" + DateTime.Now.ToString("dd_MM_yyyy");
            }
        }

        public override ICollection<FileInfo> SelectFilesToCopy(DirectoryInfo srcDir,DirectoryInfo fullDir)
        {
            IEnumerable<FileInfo> listFileSource = GetFilesFromFolder(srcDir);
            IEnumerable<FileInfo> listFileFullSave = GetFilesFromFolder(fullDir);
            return (from file in listFileSource select file).Except(listFileFullSave, FileCompare.Instance);
        }
    }
}


