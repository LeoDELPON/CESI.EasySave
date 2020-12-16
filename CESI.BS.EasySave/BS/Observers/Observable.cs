using CESI.BS.EasySave.DAL;
using System.Collections.Generic;


namespace CESI.BS.EasySave.BS.Observers
{
    interface Observable
    {
        public void Subscribe(Observer obs);
        public void Unsubscribe(Observer obs);
        public void NotifyAll(Dictionary<WorkProperties, object> dict);
        public List<Observer> subscribers { get; set; }
    }
}
