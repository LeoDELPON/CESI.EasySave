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

        public void DeleteFile(string name)
        {
            Console.WriteLine(savePath + name + extension);
            if (FileBuilder.CheckFile(savePath + name + extension))
            {
                FileBuilder.DeleteFile(savePath + name + extension);
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
                Console.WriteLine("Entrez le nom du travail à supprimer.");
                string name = Console.ReadLine();
                DeleteFile(name);
            }
        }


    }
}
