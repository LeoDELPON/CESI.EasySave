using System;
using System.IO;

namespace CESI.BS.EasySave.BS
{
    public static class FolderBuilder
    {
        //Allow user to create folder in specified path
        public static void CreateFolder(string requestedPath)
        {
            if (Directory.Exists(requestedPath))
            {
                Console.WriteLine("[-] " + requestedPath + " already exist");
                return;
            }
            Directory.CreateDirectory(requestedPath);
        }

        //Allow user to delete an existing folder at specified path
        public static void DeleteFolder(string requestedPath)
        {
            if (Directory.Exists(requestedPath) != true)
            {
                Console.WriteLine("[-] " + requestedPath + " doesn't exists.");
                return;
            }
            Directory.Delete(requestedPath);
        }
    }
}
