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
      

        public ConsoleController() :base()
        {
            languageChangerUI = new LanguageChangerUI(bs, pm);
            workCreatorUI = new WorkCreatorUI(bs, pm);
            workEraserUI = new WorkEraserUI(bs, pm);
            workExecutorUI = new WorkExecutorUI(bs, pm);
            workModifierUI = new WorkModifierUI(bs, pm);
            listWV = bs.confSaver.GetSavedWorks();
            pm.LoadLanguage(0);
            int menu = 1;
            do
            {
                menu = MainMenu();
                Console.WriteLine("MainMenu = " + menu);
            } while (menu != 0);
        
        }

       

        public int MainMenu()
        {
            intReturn answer;
            answer.correct = false;
            do
            {
                Console.Clear();
                Console.WriteLine(pm.GetPrintable("Title"));
                Console.WriteLine("1) " + pm.GetPrintable("CreateWork"));
                Console.WriteLine("2) " + pm.GetPrintable("ExecuteWork"));
                Console.WriteLine("3) " + pm.GetPrintable("ModifyWork"));
                Console.WriteLine("4) " + pm.GetPrintable("DeleteAWork"));
                Console.WriteLine("5) " + pm.GetPrintable("Priority"));
                Console.WriteLine("6) " + pm.GetPrintable("Language"));
                answer = getIntFromUser(1, 5);
                if (!answer.correct)
                {
                    Console.WriteLine(pm.GetPrintable("IntAnswerOutOfRange"));
                    Console.ReadKey();
                }
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
       
        public void WorkCreatorUI()
        {


        }



        public void WorkExecutorUI()
        {

        }
        public void WorkModifierUI()
        {

        }
        public void WorkEraserUI()
        {

        }
        public void LanguageChangerUI()
        {

        }
    }
}
