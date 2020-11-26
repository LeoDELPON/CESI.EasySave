using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CESI.BS.EasySave.BS
{
    public static class FileBuilder
    {

        internal static void GenerateFile(string sourceDir, string backupDir, string fileName)

        {
            try
            {
                // Will not overwrite if the destination file already exists.
                File.Copy(Path.Combine(sourceDir, fileName), Path.Combine(backupDir, fileName));
            }

            // Catch exception if the file was already copied.
            catch (IOException copyError)
            {
                Console.WriteLine(copyError.Message);
            }
        }     


        
    }
}
