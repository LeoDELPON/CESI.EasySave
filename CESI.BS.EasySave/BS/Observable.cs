using System;
using System.Collections.Generic;
using System.Text;

namespace CESI.BS.EasySave.BS
{
    interface Observable
    {
        public void Subscribe(Observer obs);
        public void Unsubscribe(Observer obs);
        public void NotifyAll();
        public List<Observer> subscribers { get; set; }
    }
}
