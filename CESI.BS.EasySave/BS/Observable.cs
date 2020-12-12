using CESI.BS.EasySave.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace CESI.BS.EasySave.BS
{
    interface Observable
    {
        public void Subscribe(Observer obs);
        public void Unsubscribe(Observer obs);
        public void UnSubscribeServer(Observer obs);
        public void SubscribeServer(Observer obs);
        public void NotifyAll();
        public List<Observer> subscribers { get; set; }
        public List<Observer> serverSubscriber { get; set; }
        public void NotifyServer(Dictionary<WorkProperties, object> dict);
    }
}
