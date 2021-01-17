using CESI.BS.EasySave.BS;
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
         /*   wv.typeSave = ir.value - 1;
            bs.AddWork(wv.name, wv.source, wv.target, SaveTypeMethods.GetSaveTypeFromInt(wv.typeSave), new List<string> { "test" }, "test");// ajout du travail
            WrkElements we = new WrkElements(wv, bs);
            we.chiffrage = (bool)addWorkWindow.isXor.IsChecked;

            PrepareWrkElement(we);
            bs.confSaver.SaveWork(wv);*/

        }
    }
}
