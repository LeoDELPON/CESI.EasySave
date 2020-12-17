using CESI.BS.EasySave.DAL;
using System.Collections.Generic;

namespace CESI.Server.EasySave.Factory
{
    class DataServerFactory : Factory
    {
        public override DTODataServer CreateDtoDataServer(Dictionary<WorkProperties, object> propertiesServer)
        {
            DTODataServer dataServer = new DTODataServer
            {
                Date = propertiesServer[WorkProperties.Date].ToString(),
                Name = propertiesServer[WorkProperties.Name].ToString(),
                Source = propertiesServer[WorkProperties.Source].ToString(),
                TypeSave = propertiesServer[WorkProperties.TypeSave].ToString(),
                Target = propertiesServer[WorkProperties.Target].ToString(),
                Size = propertiesServer[WorkProperties.Size].ToString(),
                Duration = propertiesServer[WorkProperties.Duration].ToString(),
                EncryptDuration = propertiesServer[WorkProperties.EncryptDuration].ToString(),
                State = propertiesServer[WorkProperties.State].ToString(),
                EligibleFiles = propertiesServer[WorkProperties.EligibleFiles].ToString(),
                Progress = propertiesServer[WorkProperties.Progress].ToString(),
                RemainingSize = propertiesServer[WorkProperties.RemainingSize].ToString()
            };
            return dataServer;
        }
    }
}
