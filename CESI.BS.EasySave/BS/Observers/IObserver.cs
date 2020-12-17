using CESI.BS.EasySave.DAL;
using System.Collections.Generic;

namespace CESI.BS.EasySave.BS.Observers
{
    public interface IObserver
    {
        public void ReactDataUpdate(Dictionary<WorkProperties, object> progress);
    }
}
