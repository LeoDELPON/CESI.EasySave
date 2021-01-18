using CESI.BS.EasySave.BS;
using CESI.BS.EasySave.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace EasySave_1._2.MainMenuClasses
{
    class WorkExecutorUI : Printer, IMainMenuMethod
    {
        public ThreadLifeManager threadLifeManager;
        public WorkExecutorUI(BSEasySave bs, PrintManager pm) : base(bs, pm)
        {

        }
        public void Perform()
        {
            Console.Clear();
            Console.WriteLine("|" + pm.GetPrintable("ExitIndications") + "|");
            
            if (bs.works.Count == 0)
            {
                Console.WriteLine(pm.GetPrintable("NoExistingWork"));
                Console.ReadKey();
                return;
            }
            string question = pm.GetPrintable("SelectAWork") + Environment.NewLine;
            for (int i = 0; i< bs.works.Count; i++)
            {
                question += i + 1 + ") " + bs.works[i].Name + Environment.NewLine;
            }
            intReturn valueReturned = getIntFromUser(question);
            if (valueReturned.returnVal)
            {
                return;
            }
            string workList = valueReturned.value.ToString();
            int terminatedThreads = 0;
            foreach (char work in workList)
            {
                //   Save.fileMaxSize = highPriorityExtention.getLimit();
                int iw = int.Parse(work.ToString());
                Thread saveThread = new Thread(launchWork =>
                {
                    using (ThreadMutex.Canceller.Token.Register(Thread.CurrentThread.Abort)) { }

                    try
                    {
                       
                        //we.inWrkList.workProgressBar.Value = 0;
                        //setProgressBarToZero
                        threadLifeManager.SubscribeToSaves(bs.works[iw]);

                        //bs.works[iw].SaveType.Subscribe(we.inWrkList);
                        //LoadingBar subscribe to progression

                        //bs.works[iw].SaveType.Subscribe(myServer); 
                        //connection with the server
                        bs.works[iw].Perform();
                       // bs.works[iw].SaveType.Unsubscribe(we.inWrkList);//unsubscribe from progression
                        terminatedThreads++;
                        threadLifeManager.UnsubscribeToSaves(bs.works[weList.IndexOf(we)]);
                    }
                    catch (ThreadInterruptedException)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {

                            we.inWrkList.workProgressBar.Value = 0;
                            terminatedThreads++;
                        });
                    }
                    if (terminatedThreads == threadLifeManager.Count())
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            resetButtons();
                            EnableButtonsAccess(true);
                            terminatedThreads = 0;
                        });
                    }



                });

                threadLifeManager.AddThread(saveThread);
                saveThread.Priority = ThreadPriority.BelowNormal;
                saveThread.Start();
                i++;
            }
    
        }
    }
}
