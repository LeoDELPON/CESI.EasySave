using System;
using System.Collections.Generic;
using System.IO;
using static CESI.BS.EasySave.BS.ConfSaver.ConfSaver;

using CESI.BS.EasySave.BS;
using CESI.BS.EasySave.DAL;
using EasySave_1._2.MainMenuClasses;

namespace EasySave_1._2
{
    public class ConsoleController : Printer
    {
        readonly IMainMenuMethod languageChangerUI;
        readonly IMainMenuMethod workCreatorUI;
        readonly IMainMenuMethod workEraserUI;
        readonly IMainMenuMethod workExecutorUI;
        readonly IMainMenuMethod workModifierUI;
        readonly IMainMenuMethodNonVoid blckngPrcssModifierUI;
        public DirectoryInfo currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
        ThreadLifeManager tlf;
        List<WorkVar> listWV;


        public ConsoleController() : base()
        {
            pm.LoadLanguage(0);
            languageChangerUI = new LanguageChangerUI(bs, pm);
            workCreatorUI = new WorkCreatorUI(bs, pm);
            workEraserUI = new WorkEraserUI(bs, pm);
            workExecutorUI = new WorkExecutorUI(bs, pm);
            workModifierUI = new WorkModifierUI(bs, pm);
            blckngPrcssModifierUI = new BlckngPrcssModifierUI(bs, pm);
            GetSavedWorks();          
            int menu;
            List<string> ls = new List<string>{ "null" };
            tlf = new ThreadLifeManager(bs,ls);
            tlf.StartObservingProcesses();
            do
            {
                menu = MainMenu();
            } while (menu != 7);

        }



        public int MainMenu()
        {
            IntReturn answer;
            answer.correct = false;
            do
            {
                Console.Clear();
                Console.WriteLine(pm.GetPrintable("Title"));
                string question =
                    "1) " + pm.GetPrintable("CreateWork") + Environment.NewLine +
                    "2) " + pm.GetPrintable("ExecuteWork") + Environment.NewLine +
                    "3) " + pm.GetPrintable("ModifyWork") + Environment.NewLine +
                    "4) " + pm.GetPrintable("DeleteAWork") + Environment.NewLine +
                    "5) " + pm.GetPrintable("SelectBlockingProcesses") + Environment.NewLine +
                    "6) " + pm.GetPrintable("Language") + Environment.NewLine +
                    "7) " + pm.GetPrintable("Quit") + Environment.NewLine;
                answer = GetIntFromUser(1, 6, question);

            } while (!answer.correct);
            switch (answer.value)
            {
                case 1:
                    workCreatorUI.Perform();
                    break;
                case 2:
                    workExecutorUI.Perform();
                    break;
                case 3:
                    workModifierUI.Perform();
                    break;
                case 4:
                    workEraserUI.Perform();
                    break;
                case 5:                    
                    tlf.processes = blckngPrcssModifierUI.Perform();                    
                    break;
                case 6:
                    languageChangerUI.Perform();
                    MainMenu();
                    break;
                default:
                    break;
            }
            return answer.value;

        }
        private void GetSavedWorks()
        {           
            listWV = bs.confSaver.GetSavedWorks();
            foreach (WorkVar wv in listWV)
            {
                bs.AddWork(wv.name, wv.source, wv.target, pm.GetPrintable(SaveTypeMethods.GetSaveTypeStrFromInt(wv.typeSave)), wv.extension, wv.key);
            }

        }
    }
}
