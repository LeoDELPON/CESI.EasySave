using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CESI.BS.EasySave.BS
{
    internal class Full : Save
    {
        public Full()
        {

        }

        public override void SaveProcess(string sourceD, string destD, string dirName)
        {
            var dirSource = new DirectoryInfo(sourceD);
            var dirDestination = new DirectoryInfo(destD);

        }
    }
}
