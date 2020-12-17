using CESI.BS.EasySave.DAL;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CESI.BS.EasySave.BS
{
    internal class Full : Save
    {
        /// <summary>
        /// Sauvegarde complète.
        /// </summary>
        /// <param name="props">Props</param>
        /// <param name="cryptoExtensions">Liste des extensions des fichiers</param>
        /// <param name="priorityExtensions">Extensions de fichiers prioritaires</param>
        /// <param name="key"></param>
        public Full(string props, List<string> cryptoExtensions, List<string> priorityExtensions, string key) : base()
        {
            IdTypeSave = "ful";
            TypeSave = SaveType.FULL;
            propertiesWork[WorkProperties.Name] = props;
            _cryptoExtension = cryptoExtensions;
            _priorityExtension = priorityExtensions;
            _key = key;
        }

        public override string CheckSaveFile(string dir)
        {
            if (!Directory.Exists(dir + @"\Full"))
            {
                return @"\Full";
            }
            else
            {
                return @"\Full";
            }
        }

        public override List<FileInfo> SelectFilesToCopy(DirectoryInfo dir, DirectoryInfo fullDir)
        {
            return GetFilesFromFolder(dir).ToList();
        }
    }
}

