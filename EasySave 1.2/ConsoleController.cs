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


        IMainMenuMethod languageChangerUI;
        IMainMenuMethod workCreatorUI;
        IMainMenuMethod workEraserUI;
        IMainMenuMethod workExecutorUI;
        IMainMenuMethod workModifierUI;
        public DirectoryInfo currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());

        List<WorkVar> listWV;


        public ConsoleController() : base()
        {
            pm.LoadLanguage(0);
            languageChangerUI = new LanguageChangerUI(bs, pm);
            workCreatorUI = new WorkCreatorUI(bs, pm);
            workEraserUI = new WorkEraserUI(bs, pm);
            workExecutorUI = new WorkExecutorUI(bs, pm);
            workModifierUI = new WorkModifierUI(bs, pm);
            getSavedWorks();          
            int menu = 1;
            do
            {
                menu = MainMenu();
                Console.WriteLine("MainMenu = " + menu);
            } while (menu != 7);

        }



        public int MainMenu()
        {
            intReturn answer;
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
                    "5) " + pm.GetPrintable("Priority") + Environment.NewLine +
                    "6) " + pm.GetPrintable("Language") + Environment.NewLine +
                    "7) " + pm.GetPrintable("Quit") + Environment.NewLine;
                answer = getIntFromUser(1, 5, question);

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
                    languageChangerUI.Perform();
                    break;
                default:
                    break;
            }
            return answer.value;

        }
        private void getSavedWorks()
        {           
            listWV = bs.confSaver.GetSavedWorks();
            foreach (WorkVar wv in listWV)
            {
                bs.AddWork(wv.name, wv.source, wv.target, pm.GetPrintable(SaveTypeMethods.GetSaveTypeFromInt(wv.typeSave)), wv.extension, wv.key);
            }

        }
    }
}
