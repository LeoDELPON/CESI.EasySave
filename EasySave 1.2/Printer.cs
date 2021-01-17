using CESI.BS.EasySave.BS;
using System;
using System.Collections.Generic;
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
        public int getIntFromUser()
        {

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

    }
}
