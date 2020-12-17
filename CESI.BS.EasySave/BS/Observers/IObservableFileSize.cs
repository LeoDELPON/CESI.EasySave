using System.Collections.Generic;

namespace CESI.BS.EasySave.BS
{
    public interface IObservableFileSize
    {
        public List<IObserverFileSize> SubscribersFileSize { get; set; }
        public void SubscribeFileSize(IObserverFileSize obs);
        public void UnsubscribeFileSize(IObserverFileSize obs);
        public void NotifyFileSize();
    }
}
