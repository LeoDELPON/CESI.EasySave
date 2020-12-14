using CESI.BS.EasySave.BS;
using CESI.BS.EasySave.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CESI.BS.EasySave.BS
{
    public class ThreadLifeManager : ObserverFileSize
    {
        List<Thread> listThreads = new List<Thread>();
        BSEasySave bs;
        public ThreadLifeManager(BSEasySave bs)
        {
            this.bs = bs;
        }
        public void AddThread(Thread th)
        {
            listThreads.Add(th);
        }
        public void ClearThread()
        {
            listThreads.Clear();
        }
        public int Count()
        {
            return listThreads.Count;
        }

        public void React(Save savetype)
        {           
            foreach(Work w in bs.works)
            {
                if (!(w.SaveType == savetype))
                {
                    Monitor.TryEnter(w.SaveType.pause);
                }
            }
        }
        public bool Pause()
        {
            bool allclear = true;
            foreach (Work w in bs.works)
            {
                allclear = allclear && Monitor.TryEnter(w.SaveType.pause);

            }
            return allclear;
        }
        public void Abort()
        {
            foreach (Thread th in listThreads)
            {
                th.Interrupt();
            }
        }
        public void SubscribeToSaves(Work w)
        {          
            w.SaveType.SubscribeFileSize(this);            
        }
        public void UnsubscribeToSaves(Work w)
        {                    
             w.SaveType.UnsubscribeFileSize(this);
        }
        public bool Resume()
        {
            bool allclear = true;
            foreach (Work w in bs.works)
            {
                Monitor.Exit(w.SaveType.pause);
                allclear = allclear && !Monitor.IsEntered(w.SaveType.pause);
            }
            return allclear;
        }
        public void EndReaction(Save savetype)
        {
            foreach (Work w in bs.works)
            {
                if (Monitor.IsEntered(w.SaveType.pause))
                {
                    Monitor.Exit(w.SaveType.pause);
                }
            }
        }
    }
}
