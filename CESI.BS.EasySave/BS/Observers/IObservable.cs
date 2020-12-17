using CESI.BS.EasySave.DAL;
using System.Collections.Generic;


namespace CESI.BS.EasySave.BS.Observers
{
    interface IObservable
    {
        public void Subscribe(IObserver obs);
        public void Unsubscribe(IObserver obs);
        public void NotifyAll(Dictionary<WorkProperties, object> dict);
        public List<IObserver> Subscribers { get; set; }
    }
}
