using CESI.BS.EasySave.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace CESI.Server.EasySave.Factory
{
    public abstract class Factory
    {
        public abstract DTODataServer CreateDtoDataServer(Dictionary<WorkProperties, object> propertiesServer);
    }
}
