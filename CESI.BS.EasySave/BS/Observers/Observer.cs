

using CESI.BS.EasySave.DAL;
using System.Collections.Generic;

namespace CESI.BS.EasySave.BS.Observers
{
    public interface Observer
    {     
        public void ReactDataUpdate(Dictionary<WorkProperties, object> dict);
    }
}
