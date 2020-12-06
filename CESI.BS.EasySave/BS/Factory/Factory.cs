using CESI.BS.EasySave.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace CESI.BS.EasySave.BS.Factory
{
    internal abstract class Factory
    {
        public abstract Work CreateWorkObject(Dictionary<WorkProperties, object> properties);
        public abstract Save CreateSaveObject(string _saveType, string prop, IList<string> extensions, string key);
    }
}
