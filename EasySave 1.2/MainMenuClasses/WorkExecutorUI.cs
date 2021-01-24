using CESI.BS.EasySave.BS;
using CESI.BS.EasySave.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace EasySave_1._2.MainMenuClasses
{
    class WorkExecutorUI : Printer, IMainMenuMethodWithParam
    {
        public ThreadLifeManager threadLifeManager;
        public WorkExecutorUI(BSEasySave bs, PrintManager pm) : base(bs, pm)
        {

        }
        public void Perform(List<string> listString)
        {
            threadLifeManager = new ThreadLifeManager(bs, listString);
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
            IntReturn valueReturned = GetIntFromUser(question);
            if (valueReturned.returnVal)
            {
                return;
            }
            string workList = valueReturned.value.ToString();
            Console.Clear();
            Console.WriteLine(pm.GetPrintable("SavesRunning"));
            foreach (char work in workList)
            {
                int ij = int.Parse(work.ToString())-1;
                Console.WriteLine(bs.works[ij].Name);
                bs.works[ij].Perform();
            }
            Console.WriteLine(pm.GetPrintable("DataSaved"));
            while (Console.KeyAvailable)
            {
                Console.ReadKey(false);
            }
            Console.ReadKey();

        }
    }
}
