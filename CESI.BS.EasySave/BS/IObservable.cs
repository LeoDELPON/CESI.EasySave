using CESI.BS.EasySave.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace CESI.BS.EasySave.BS
{
    interface IObservable
    {
        public void Subscribe(IObserver obs);
        public void Unsubscribe(IObserver obs);
        public void UnSubscribeServer(IObserver obs);
        public void SubscribeServer(IObserver obs);
        public void NotifyAll();
        public List<IObserver> subscribers { get; set; }
        public List<IObserver> serverSubscriber { get; set; }
        public void NotifyServer(Dictionary<WorkProperties, object> dict);
    }
}
