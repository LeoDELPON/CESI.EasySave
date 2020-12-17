using CESI.BS.EasySave.DAL;
using System.Collections.Generic;

namespace CESI.Server.EasySave.Factory
{
    internal abstract class Factory
    {
        public abstract DTODataServer CreateDtoDataServer(Dictionary<WorkProperties, object> propertiesServer);
    }
}
