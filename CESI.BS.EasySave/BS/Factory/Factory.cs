using CESI.BS.EasySave.DAL;
using CESI.BS.EasySave.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CESI.BS.EasySave.BS.Factory
{
    internal abstract class Factory
    {
        public abstract Work CreateWorkObject(Dictionary<WorkProperties, object> properties);
        public abstract Save CreateSaveObject(string _saveType, string prop, List<string> cryptoExtensions, List<string> priorityExtensions, string key);
        public abstract DTOLogger CreateDtoLogger(Dictionary<WorkProperties, object> propertiesLogs);
        public abstract DTOStatusLogger CreateDtoStatusLogger(Dictionary<WorkProperties, object> propertiesStatus);
    }
}
