using CESI.BS.EasySave.BS;
using CESI.BS.EasySave.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static CESI.BS.EasySave.BS.ConfSaver.ConfSaver;

namespace EasySave_1._2.MainMenuClasses
{
    class WorkCreatorUI : Printer, IMainMenuMethod
    {
        public WorkCreatorUI(BSEasySave bs, PrintManager pm) : base(bs, pm)
        {

        }
        public void Perform()
        {

            WorkVar wv = new WorkVar();
            Console.Clear();
            Console.WriteLine("|" + pm.GetPrintable("ExitIndications") + "|");
            Console.WriteLine(pm.GetPrintable("WorkName") + " : ");
            wv.name = Console.ReadLine().ToString();
            if (wv.name.Equals("\u0018"))
            {
                return;
            }

            StringReturn sr = GetPathFromUser(pm.GetPrintable("WorkSource") + " : ");
            if (sr.returnVal)
            {
                return;
            }
            wv.source = sr.value;
            sr = GetPathFromUser(pm.GetPrintable("WorkTarget") + " : ");
            if (sr.returnVal)
            {
                return;
            }
            wv.target = sr.value;
            
            IntReturn ir;
            ir.correct = false;
            do
            {
                string question = pm.GetPrintable("WorkSaveType") + " :" + Environment.NewLine;
                for (int i = 0; i <= Enum.GetNames(typeof(SaveType)).Length-1; i++)
                {
                    question += (i + 1) + ") " + pm.GetPrintable(SaveTypeMethods.GetSaveTypeStrFromInt(i)) + Environment.NewLine;
                }
               
                ir = GetIntFromUser(1, Enum.GetNames(typeof(SaveType)).Length+1, question);
                if (ir.returnVal)
                {
                    return;
                }                

            } while (!ir.correct);
            wv.typeSave = ir.value - 1;
            wv.key = "null";
            wv.extension = new List<string>();
            wv.extension.Add("null");
            boolReturn br = AskYesOrNo(pm.GetPrintable("XOR") + "?");
            if (br.returnVal) {
                return;
            }
            if (br.value)
            {
                wv.extension.Clear();
                boolReturn addMore = new boolReturn();
                addMore.value = true;
                do
                {
                    Console.WriteLine(pm.GetPrintable("EXTENSION") + " : ");
                    wv.extension.Add(Console.ReadLine().ToString());
                    addMore = AskYesOrNo(pm.GetPrintable("AnotherOne"));
                    if (addMore.returnVal)
                    {
                        return;
                    }
                } while (addMore.value);
                Console.WriteLine(pm.GetPrintable("KEY"));
                wv.key = Console.ReadLine();
                if (wv.key.Equals("\u0018"))
                {
                    return;
                }

            }
            wv.typeSave = ir.value - 1;
            bs.AddWork(wv.name, wv.source, wv.target, SaveTypeMethods.GetSaveTypeStrFromInt(wv.typeSave), new List<string> { "test" }, "test");// ajout du travail            
            bs.confSaver.SaveWork(wv);
            Console.WriteLine(pm.GetPrintable("WorkCreated"));
            Console.ReadKey();

        }
    }
}
