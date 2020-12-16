using CESI.BS.EasySave.DAL;
using CESI.BS.EasySave.DTO;
using System;
using System.Collections.Generic;
using System.Text;

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
                    (List<string>)properties[WorkProperties.Extensions],
                    properties[WorkProperties.Key].ToString())
            );
        }

        public override Save CreateSaveObject(string _saveType, string prop, List<string> extensions, string key)
        {
            Save _save;
            switch(_saveType.GetSaveTypeFromString())
            {
                case SaveType.DIFFERENTIAL:
                    _save = new Differential(prop, extensions, key);
                    break;
                case SaveType.FULL:
                    _save = new Full(prop, extensions, key);
                    break;
                default:
                    _save = new Differential(prop, extensions, key);
                    break;
            }
            return _save;
        }

        public override DTOLogger CreateDtoLogger(Dictionary<WorkProperties, object> propertiesLogs)
        {
            DTOLogger logger = new DTOLogger();
            logger.Date = propertiesLogs[WorkProperties.Date].ToString();
            logger.Name = propertiesLogs[WorkProperties.Name].ToString();
            logger.Source = propertiesLogs[WorkProperties.Source].ToString();
            logger.Target = propertiesLogs[WorkProperties.Target].ToString();
            logger.Size = propertiesLogs[WorkProperties.Size].ToString();
            logger.Duration = propertiesLogs[WorkProperties.Duration].ToString();
            logger.EncryptDuration = propertiesLogs[WorkProperties.EncryptDuration].ToString();
            return logger;
        }

        public override DTOStatusLogger CreateDtoStatusLogger(Dictionary<WorkProperties, object> propertiesStatus)
        {
            DTOStatusLogger statusLogger = new DTOStatusLogger();
            statusLogger.Name = propertiesStatus[WorkProperties.Name].ToString();
            statusLogger.State = propertiesStatus[WorkProperties.State].ToString();
            statusLogger.EligibleFiles = propertiesStatus[WorkProperties.EligibleFiles].ToString();
            statusLogger.Size = propertiesStatus[WorkProperties.Size].ToString();
            statusLogger.Progress = propertiesStatus[WorkProperties.Progress].ToString();
            statusLogger.RemainingSize = propertiesStatus[WorkProperties.RemainingSize].ToString();
            statusLogger.Source = propertiesStatus[WorkProperties.Source].ToString();
            statusLogger.Target = propertiesStatus[WorkProperties.Target].ToString();
            statusLogger.Date = propertiesStatus[WorkProperties.Date].ToString();
            return statusLogger;
        }

        public override DTODataServer CreateDtoDataServer(Dictionary<WorkProperties, object> propertiesServer)
        {
            DTODataServer dataServer = new DTODataServer();
            dataServer.Date = propertiesServer[WorkProperties.Date].ToString();
            dataServer.Name = propertiesServer[WorkProperties.Name].ToString();
            dataServer.Source = propertiesServer[WorkProperties.Source].ToString();
            dataServer.TypeSave = propertiesServer[WorkProperties.TypeSave].ToString();
            dataServer.Target = propertiesServer[WorkProperties.Target].ToString();
            dataServer.Size = propertiesServer[WorkProperties.Size].ToString();
            dataServer.Duration = propertiesServer[WorkProperties.Duration].ToString();
            dataServer.EncryptDuration = propertiesServer[WorkProperties.EncryptDuration].ToString();
            dataServer.State = propertiesServer[WorkProperties.State].ToString();
            dataServer.EligibleFiles = propertiesServer[WorkProperties.EligibleFiles].ToString();
            dataServer.Progress = propertiesServer[WorkProperties.Progress].ToString();
            dataServer.RemainingSize = propertiesServer[WorkProperties.RemainingSize].ToString();
            return dataServer;
        }
    }
}
