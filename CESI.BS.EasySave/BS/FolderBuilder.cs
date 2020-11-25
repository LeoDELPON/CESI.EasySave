using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CESI.BS.EasySave.BS
{
    internal static class FolderBuilder
    {
        //Allow user to create folder in specified path
        internal static void CreateFolder(string requestedPath)
        {
            if (Directory.Exists(requestedPath))
            {
                Console.WriteLine("That path exists already.");
                return;
            }
            Directory.CreateDirectory(requestedPath);
        }

        //Allow user to delete an existing folder at specified path
        internal static void DeleteFolder(string requestedPath)
        {
            if (Directory.Exists(requestedPath) != true)
            {
                Console.WriteLine("That path doesn't exists.");
                return;
            }
            Directory.Delete(requestedPath);
        }

        //Allow user to check if a specified folder already exist
        internal static bool CheckFolder(string requestedPath)
        {
            if (Directory.Exists(requestedPath))
            {
                return true;
            }
            return false;
        }
    }
}
