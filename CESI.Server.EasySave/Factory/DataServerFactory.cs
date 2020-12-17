using CESI.BS.EasySave.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace CESI.Server.EasySave.Factory
{
    class DataServerFactory : Factory
    {
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
