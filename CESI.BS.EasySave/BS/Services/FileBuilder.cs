using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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


        
    }
}
