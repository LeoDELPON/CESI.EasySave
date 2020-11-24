using CESI.BS.EasySave.BS.Interface;
using CESI.BS.EasySave.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace CESI.BS.EasySave.BS.Factory
{
    class WorkFactory : Factory
    {
        public override IWork CreateWorkObject(string name, string source, string destination, Save saveType)
        {
            return new Work(name, source, destination, saveType);
        }
    }
}
