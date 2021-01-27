using CESI.BS.EasySave.BS;
using CESI.BS.EasySave.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using static CESI.BS.EasySave.BS.ConfSaver.ConfSaver;

namespace EasySave_1._2.MainMenuClasses
{
    class WorkEraserUI :Printer, IMainMenuMethod
    {
        public WorkEraserUI(BSEasySave bs, PrintManager pm) : base(bs, pm)
        {

        }
        public string savePath = Environment.CurrentDirectory + @"\saveConf\";
        public string extension = ".xml";



        WorkVar getSpecifiedWorkVar(string name, WorkVar existingWV)
        {
            List<WorkVar> existingWVList = new List<WorkVar>();

            existingWVList = bs.confSaver.GetSavedWorks();
            foreach(WorkVar v in existingWVList)
            {
                if(v.name == name)
                {
                    existingWV = v;
                    return existingWV;
                }
            }
            return existingWV;
        }

        public void DeleteFile(WorkVar workVar, IntReturn valueReturned)
        {
            Console.WriteLine(savePath + workVar.name + extension);
            if (FileBuilder.CheckFile(savePath + workVar.name + extension))
            {
                Console.WriteLine("[+] Supression du travail confirmé.");
                FileBuilder.DeleteFile(savePath + workVar.name + extension);
                bs.DeleteWork(valueReturned.value - 1);
            }
            else
            {
                Console.WriteLine("[-] There is no work with this name.");
            }
        }

        public void Perform()
        {
            WorkVar wv = new WorkVar();
            WorkVar existingWorkVar = new WorkVar();
            Console.Clear();

            Console.WriteLine("|" + pm.GetPrintable("ExitIndications") + "|");

            if (bs.works.Count == 0)
            {
                Console.WriteLine(pm.GetPrintable("NoExistingWork"));
                Console.ReadKey();
                return;
            }
            else
            {
                string question = pm.GetPrintable("SelectAWork") + Environment.NewLine;
                for (int i = 0; i < bs.works.Count; i++)
                {
                    question += i + 1 + ") " + bs.works[i].Name + Environment.NewLine;
                }
                IntReturn valueReturned = GetIntFromUser(question);
                if (valueReturned.returnVal)
                {
                    return;
                }
                existingWorkVar = getSpecifiedWorkVar(bs.works[valueReturned.value - 1].Name, existingWorkVar);


                Console.WriteLine("Entrez le nom du travail à supprimer.");

                DeleteFile(existingWorkVar, valueReturned);
            }
        }


    }
}
