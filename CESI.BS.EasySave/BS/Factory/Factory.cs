using CESI.BS.EasySave.BS.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CESI.BS.EasySave.BS.Factory
{
    internal abstract class Factory
    {
        public abstract IWork CreateWorkObject(string name, string source, string destination, Save saveType);

    }
}
