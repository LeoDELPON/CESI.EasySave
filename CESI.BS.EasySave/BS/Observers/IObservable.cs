using CESI.BS.EasySave.DAL;
using System.Collections.Generic;

namespace CESI.BS.EasySave.BS.Observers
{
    interface IObservable
    {
        
        public void UnSubscribeServer(IObserver obs);
        public void SubscribeServer(IObserver obs);
        public List<IObserver> serverSubscriber { get; set; }
        public void NotifyServer(Dictionary<WorkProperties, object> dict);
    }
}
