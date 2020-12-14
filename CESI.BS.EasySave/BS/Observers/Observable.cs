using System.Collections.Generic;


namespace CESI.BS.EasySave.BS.Observers
{
    interface Observable
    {
        public void Subscribe(Observer obs);
        public void Unsubscribe(Observer obs);
        public void NotifyAll(long progress);
        public List<Observer> subscribers { get; set; }
    }
}
