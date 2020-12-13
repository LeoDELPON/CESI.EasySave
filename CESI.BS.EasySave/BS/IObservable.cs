using CESI.BS.EasySave.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace CESI.BS.EasySave.BS
{
    interface IObservable
    {
        
        public void UnSubscribeServer(IObserver obs);
        public void SubscribeServer(IObserver obs);
        public List<IObserver> serverSubscriber { get; set; }
        public void NotifyServer(Dictionary<WorkProperties, object> dict);
    }
}
