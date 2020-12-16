using System;
using System.Collections.Generic;
using System.Text;

namespace CESI.BS.EasySave.BS
{
    public interface ObservableFileSize
    {
        public List<ObserverFileSize> subscribersFileSize { get; set; }
        public void SubscribeFileSize(ObserverFileSize obs);
        public void UnsubscribeFileSize(ObserverFileSize obs);
        public void notifyFileSize();
    }
}
