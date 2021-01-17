using System;
using System.Collections.Generic;
using System.IO;
using static CESI.BS.EasySave.BS.ConfSaver.ConfSaver;
using CESI.BS.EasySave.BS.ConfSaver;
namespace EasySave_1._2
{
    public class ConsoleController
    {
        ConfSaver cf = new ConfSaver();
        public DirectoryInfo currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
        PrintManager pm = new PrintManager();
        List<WorkVar> listWV;
        public struct intReturn
        {
            public int value;
            public bool correct;
            public bool returnVal;
        }

        public ConsoleController()
        {
            listWV = cf.GetSavedWorks();
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
                    WorkCreatorUI();
                    break;
                case 2:
                    WorkExecutorUI();
                    break;
                case 3:
                    WorkModifierUI();
                    break;
                case 4:
                    WorkEraserUI();
                    break;
                case 5:
                    LanguageChangerUI();
                    break;
                default:
                    break;
            }
            return answer.value;

        }
        public int getIntFromUser() {
      
            string userInput = Console.ReadLine();
            if (int.TryParse(userInput, out int intUI))
            {
                return intUI;
            }
            return 0;
        }
        public intReturn getIntFromUser(int min, int max)
        {
            intReturn ir;
            ir.value = 0;
            ir.correct = false;
            ir.returnVal = false;
            string userInput = Console.ReadLine();
            if (int.TryParse(userInput, out int intUI))
            {
                if (intUI >= min && intUI <= max)
                {
                    ir.value = intUI;
                    ir.correct = true;
                }             
                return ir;
            }
            if (userInput.Equals("\u0018"))
            {
                ir.returnVal = true;
            }
            return ir;
        }

        public void WorkCreatorUI()
        {
            WorkVar wv;
            Console.Clear();
            Console.WriteLine("|" + pm.GetPrintable("ExitIndications") + "|");
            Console.WriteLine(pm.GetPrintable("WorkName") + " : ");           
            wv.name = Console.ReadLine().ToString();
            if (wv.name.Equals("\u0018"))
            {
                return;
            }
          
            do
            {
                Console.WriteLine(pm.GetPrintable("WorkSource") + " : ");
                wv.source = Console.ReadLine().ToString();
                if (wv.source.Equals("\u0018"))
                {
                    return;
                }
                if (!Directory.Exists(wv.source))
                {
                    Console.WriteLine(pm.GetPrintable("ThisDirectoryDoesNotExist"));
                    Console.ReadKey();
                }
            } while (!Directory.Exists(wv.source));
            do
            {
                Console.WriteLine(pm.GetPrintable("WorkSource") + " : ");
                wv.target = Console.ReadLine().ToString();
                if (wv.target.Equals("\u0018"))
                {
                    return;
                }
                if (!Directory.Exists(wv.target))
                {
                    Console.WriteLine(pm.GetPrintable("ThisDirectoryDoesNotExist"));
                    Console.ReadKey();
                }
            } while (!Directory.Exists(wv.target));
            intReturn ir;
            ir.correct = false;
            do
            {
                Console.WriteLine(pm.GetPrintable("WorkSaveType"));
                Console.WriteLine("1) " + pm.GetPrintable(""));
                ir = getIntFromUser(1, 2);
                if (ir.returnVal)
                {
                    return;
                }
                if (!ir.correct)
                {
                    Console.WriteLine(pm.GetPrintable("IntAnswerOutOfRange"));
                    Console.ReadKey();
                }
             
            } while (!ir.correct);
            wv.typeSave = ir.value - 1;




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
