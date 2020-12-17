using System;
using System.IO;

namespace CESI.BS.EasySave.BS
{
    public static class FileBuilder
    {

        internal static void GenerateFile(FileInfo file, DirectoryInfo dirDestination)
        {
            try
            {
                file.CopyTo(Path.Combine(dirDestination.FullName, file.Name), true);
            }
            catch (UnauthorizedAccessException error)
            {
                Console.WriteLine("[-] The file may have been tried to be moved in another disk : {0}", error); ;
            }
        }

        public static void DeleteFile(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (UnauthorizedAccessException error)
            {
                Console.WriteLine("[-] there is no file with this name : {0}", error); ;
            }

        }

        public static bool CheckFile(string requestedPath)
        {
            if (File.Exists(requestedPath))
            {
                return true;
            }
            return false;
        }

    }
}
