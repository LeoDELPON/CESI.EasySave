using CESI.BS.EasySave.DAL;
using CESI.BS.EasySave.DTO;
using System.Collections.Generic;

namespace CESI.BS.EasySave.BS.Factory
{
    class WorkFactory : Factory
    {

        public override Work CreateWorkObject(Dictionary<WorkProperties, object> properties)
        {
            return new Work(
                properties[WorkProperties.Name].ToString(),
                properties[WorkProperties.Source].ToString(),
                properties[WorkProperties.Target].ToString(),
                CreateSaveObject(properties[WorkProperties.TypeSave].ToString(),
                    properties[WorkProperties.Name].ToString(),
                    (List<string>)properties[WorkProperties.CryptoExtensions],
                    (List<string>)properties[WorkProperties.PriorityExtensions],
                    properties[WorkProperties.Key].ToString())
            );
        }

        public override Save CreateSaveObject(string _saveType, string prop, List<string> cryptoExtensions, List<string> priorityExtensions, string key)
        {
            Save _save = (_saveType.GetSaveTypeFromString()) switch
            {
                SaveType.DIFFERENTIAL => new Differential(prop, cryptoExtensions, priorityExtensions, key),
                SaveType.FULL => new Full(prop, cryptoExtensions, priorityExtensions, key),
                _ => new Full(prop, cryptoExtensions, priorityExtensions, key),
            };
            return _save;
        }

        public override DTOLogger CreateDtoLogger(Dictionary<WorkProperties, object> propertiesLogs)
        {
            DTOLogger logger = new DTOLogger
            {
                Date = propertiesLogs[WorkProperties.Date].ToString(),
                Name = propertiesLogs[WorkProperties.Name].ToString(),
                Source = propertiesLogs[WorkProperties.Source].ToString(),
                Target = propertiesLogs[WorkProperties.Target].ToString(),
                Size = propertiesLogs[WorkProperties.Size].ToString(),
                Duration = propertiesLogs[WorkProperties.Duration].ToString(),
                EncryptDuration = propertiesLogs[WorkProperties.EncryptDuration].ToString()
            };
            return logger;
        }

        public override DTOStatusLogger CreateDtoStatusLogger(Dictionary<WorkProperties, object> propertiesStatus)
        {
            DTOStatusLogger statusLogger = new DTOStatusLogger
            {
                Name = propertiesStatus[WorkProperties.Name].ToString(),
                State = propertiesStatus[WorkProperties.State].ToString(),
                EligibleFiles = propertiesStatus[WorkProperties.EligibleFiles].ToString(),
                Size = propertiesStatus[WorkProperties.Size].ToString(),
                Progress = propertiesStatus[WorkProperties.Progress].ToString(),
                RemainingSize = propertiesStatus[WorkProperties.RemainingSize].ToString(),
                Source = propertiesStatus[WorkProperties.Source].ToString(),
                Target = propertiesStatus[WorkProperties.Target].ToString(),
                Date = propertiesStatus[WorkProperties.Date].ToString()
            };
            return statusLogger;
        }
    }
}
