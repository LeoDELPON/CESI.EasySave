using CESI.BS.EasySave.BS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasySave_1._2
{
    public class Printer
    {
        public PrintManager pm;
        public struct intReturn
        {
            public int value;
            public bool correct;
            public bool returnVal;
        }
        public struct stringReturn
        {
            public string value;
            public bool correct;
            public bool returnVal;
        }
        public struct boolReturn
        {
            public bool value;
            public bool correct;
            public bool returnVal;
        }
        public BSEasySave bs;

        public Printer (BSEasySave bs, PrintManager pm)
        {
            this.bs = bs;
            this.pm = pm;
        }
        public Printer()
        {
           bs = new BSEasySave();
           pm = new PrintManager();
        }
        public intReturn getIntFromUser(string question)
        {
            string userInput;
            intReturn intUI = new intReturn();
            intUI.value = 0;
            intUI.correct = false;
            intUI.returnVal = false;
            do
            {
                Console.WriteLine(question);
                userInput = Console.ReadLine();
                intUI.returnVal = userInput.Equals("\u0018");
                if (!int.TryParse(userInput, out intUI.value) && !intUI.returnVal)
                {
                    Console.WriteLine(pm.GetPrintable("WrongAnswer"));
                    Console.ReadKey();
                }
            } while (!int.TryParse(userInput, out intUI.value) && !intUI.returnVal);
          
            return intUI;
        }
        public stringReturn getPathFromUser(string question)
        {
            stringReturn answer = new stringReturn();
            do
            {
                Console.WriteLine(question);
                answer.value = Console.ReadLine().ToString();               
                answer.returnVal = answer.value.Equals("\u0018");                
                if (!Directory.Exists(answer.value) && !answer.returnVal)
                {
                    Console.WriteLine(pm.GetPrintable("ThisDirectoryDoesNotExist"));
                    Console.ReadKey();
                }
            } while (!Directory.Exists(answer.value) && !answer.returnVal);
            return answer;
        }
        public intReturn getIntFromUser(int min, int max, string question)
        {
            intReturn ir;
            ir.value = 0;
            ir.correct = false;
            ir.returnVal = false;
            do
            {
                Console.WriteLine(question);
                string userInput = Console.ReadLine();
                ir.returnVal = userInput.Equals("\u0018");
                if (int.TryParse(userInput, out int intUI) && !ir.returnVal)
                {
                    if (intUI >= min && intUI <= max)
                    {
                        ir.value = intUI;
                        ir.correct = true;
                    }
                    else
                    {
                        Console.WriteLine(pm.GetPrintable("WrongAnswer"));
                        Console.ReadKey();
                    }
                    
                }
                ir.returnVal = userInput.Equals("\u0018");
               
            } while (!ir.correct && !ir.returnVal);
            return ir;
        }
        public boolReturn AskYesOrNo(string question)
        {
            boolReturn br = new boolReturn();
            string ans;
            do
            {
                Console.WriteLine(question + " (" + pm.GetPrintable("YesUI") + "/" + pm.GetPrintable("NoUI") + ") : ");

                ans = Console.ReadLine();
                br.value = ans.ToUpper().Equals(pm.GetPrintable("YesUI"));
                br.returnVal = ans.Equals("\u0018");
                if ((!ans.ToUpper().Equals(pm.GetPrintable("YesUI")) && !ans.ToUpper().Equals(pm.GetPrintable("NoUI"))) && !br.returnVal)
                {
                    Console.WriteLine(pm.GetPrintable("WrongAnswer"));
                    Console.ReadKey();
                }
            } while ((!ans.ToUpper().Equals(pm.GetPrintable("YesUI")) && !ans.ToUpper().Equals(pm.GetPrintable("NoUI"))) && !br.returnVal);
            return br;
        }

    }
}
