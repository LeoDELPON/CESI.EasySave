using CESI.BS.EasySave.BS;
using CESI.BS.EasySave.DAL;
using System;
using System.Collections.Generic;
using System.Management;
using System.Windows;
using System.Text;
using System.Threading;
using Xceed.Wpf.Toolkit;

namespace CESI.BS.EasySave.BS
{
    public class ThreadLifeManager : ObserverFileSize
    {
        ManagementEventWatcher processStartEvent = new ManagementEventWatcher("SELECT * FROM Win32_ProcessStartTrace");
        ManagementEventWatcher processStopEvent = new ManagementEventWatcher("SELECT * FROM Win32_ProcessStopTrace");
        List<Thread> listThreads = new List<Thread>();
        List<string> processes = new List<string>();
        BSEasySave bs;
        public static string notAdminErrorMsg = "NotAdminMsg";
        public static string processIsOnMessage = "ProcessIsOn";
        Thread threadPause;
        public delegate void OnErrorRaisedDel(string message);
        public OnErrorRaisedDel OnErrorRaised { get; set; } =(string message)=> {  };
     
        CancellationTokenSource cts = new CancellationTokenSource();
     
        
    public ThreadLifeManager(BSEasySave bs, List<string> p)
        {
            
      
            this.bs = bs;
            processes = p;
            processStartEvent.EventArrived += new EventArrivedEventHandler(processStartEvent_EventArrived);
            processStopEvent.EventArrived += new EventArrivedEventHandler(processStopEvent_EventArrived);
        
           
        }
     
        public void StartObservingProcesses()
        {
            try
            {
                processStartEvent.Start();
                processStopEvent.Start();
            }
            catch (ManagementException)
            {
                OnErrorRaised(notAdminErrorMsg);
            }
        }
        void processStartEvent_EventArrived(object sender, EventArrivedEventArgs e)
        {
           
            string str = (e.NewEvent).Properties["ProcessName"].Value.ToString();
            if (processes.Contains(str.Substring(0, str.Length - 4)))
            {
                threadPause = new Thread(ThreadPauseMethod =>
                {
                    Monitor.Enter(ThreadMutex.threadPauseWhenProcess);
                    Pause();
                    OnErrorRaised(processIsOnMessage);
                    Monitor.Wait(ThreadMutex.threadPauseWhenProcess);
                    Resume();
                    Monitor.Exit(ThreadMutex.threadPauseWhenProcess);

                });
                threadPause.Start();
      
          
            }   
                
              

         }

        void processStopEvent_EventArrived(object sender, EventArrivedEventArgs e)
        {
            string str = (e.NewEvent).Properties["ProcessName"].Value.ToString();
            if (processes.Contains(str.Substring(0, str.Length - 4)))
            {
                Monitor.Enter(ThreadMutex.threadPauseWhenProcess);
                Monitor.PulseAll(ThreadMutex.threadPauseWhenProcess);
                Monitor.Exit(ThreadMutex.threadPauseWhenProcess);
          

            }
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
                if (!Monitor.IsEntered(w.SaveType.pause))
                {
                    Monitor.Enter(w.SaveType.pause);
                    allclear = allclear && Monitor.IsEntered(w.SaveType.pause);
                }
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
                if (Monitor.IsEntered(w.SaveType.pause))
                {
                    Monitor.Exit(w.SaveType.pause);
                    allclear = allclear && !Monitor.IsEntered(w.SaveType.pause);
                }
               
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
