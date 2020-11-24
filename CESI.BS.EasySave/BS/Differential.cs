using System;
using System.Collections.Generic;
using System.Text;

namespace CESI.BS.EasySave.BS
{
    internal class Differential : Save
    {
        public override void SaveProcess(string sourceDirectory, string destinationDirectory, string directoryName, string fullBackupFolder)
        {
            getLastFullBackup(fullBackupFolder);

        }

        private void getLastFullBackup(string backupFolder)
        {

        }
        private void compareLastFullBackupToActualFolder(string backupFolder, string actualFolder)
        {
            //voir comment balayer l'arboresence .....
            if (isItSameFile())
            {
                //copy the file and the directory to backup directory
            }

        }


        private Boolean isItSameFile()
        {
            return true;
        }

    }
}
