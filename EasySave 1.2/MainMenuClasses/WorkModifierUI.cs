using CESI.BS.EasySave.BS;
using CESI.BS.EasySave.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using static CESI.BS.EasySave.BS.ConfSaver.ConfSaver;

namespace EasySave_1._2.MainMenuClasses
{
    class WorkModifierUI : Printer, IMainMenuMethod
    {
        private bool isFinished = false;
        private int counter = 0;
        public WorkModifierUI(BSEasySave bs, PrintManager pm) : base(bs, pm)
        {

        }
        public void Perform()
        {
            WorkVar wv = new WorkVar();
            List<Tuple<string, List<string>>> propertyQuestionsChange = new List<Tuple<string, List<string>>>()
            {
                Tuple.Create(pm.GetPrintable("WorkName"), new List<string>(){ wv.name }),
                Tuple.Create(pm.GetPrintable("WorkSource"), new List<string>() { wv.source}),
                Tuple.Create(pm.GetPrintable("WorkTarget"), new List<string>() { wv.target}),
                Tuple.Create(pm.GetPrintable("WorkSaveType"), new List<string>() { wv.typeSave.ToString()}),
                Tuple.Create(pm.GetPrintable("EXTENSION"), wv.extension),
                Tuple.Create(pm.GetPrintable("KEY"), new List<string>() { wv.key})
            };
            Console.Clear();
            Console.WriteLine("|" + pm.GetPrintable("ExitIndications") + "|");
            
            if(bs.works.Count == 0)
            {
                Console.WriteLine(pm.GetPrintable("NoExistingWork"));
                Console.ReadKey();
                return;
            }

            string question = pm.GetPrintable("SelectAWork") + Environment.NewLine;
            for (int i = 0; i < bs.works.Count; i++)
            {
                question += i + 1 + ") " + bs.works[i].Name + Environment.NewLine;
            }
            intReturn valueReturned = getIntFromUser(question);
            bool extensionVerify = true;
            Console.Clear();
            do
            {
                Console.WriteLine(propertyQuestionsChange[counter].Item1 + "?");
                Console.WriteLine(pm.GetPrintable("YesUI") + "|" + pm.GetPrintable("NoUI"));
                string response = Console.ReadLine().ToUpper();
                if ((response == "O") || (response == "Y"))
                {
                    if(propertyQuestionsChange[counter].Item1 == pm.GetPrintable("EXTENSION"))
                    {
                        do
                        {
                            propertyQuestionsChange[counter].Item2.Add(Console.ReadLine());
                            Console.WriteLine(pm.GetPrintable("AnotherOne"));
                            Console.WriteLine(pm.GetPrintable("YesUI") + "|" + pm.GetPrintable("NoUI"));
                            string responseTwo = Console.ReadLine().ToUpper();
                            if ((responseTwo == "O") || (responseTwo == "Y"))
                            {
                                continue;
                            } else
                            {
                                extensionVerify = false;
                            }
                        } while (extensionVerify);
                    } else
                    {
                        propertyQuestionsChange[counter].Item2[0] = Console.ReadLine();
                    }
                } else if ((response == "N"))
                {
                    counter++;
                    continue;
                }

                counter++;
                if(propertyQuestionsChange.Count == counter)
                {
                    break;
                }
            } while (!isFinished);

            try
            {
                bs.ModifyWork(bs.works[valueReturned.value -1], wv.name, wv.source, wv.target, SaveTypeMethods.GetSaveTypeFromInt(Int32.Parse(propertyQuestionsChange[3].Item2[0])), wv.extension, wv.key);
                bs.confSaver.modifyEntireFile(bs.works[valueReturned.value-1].Name, wv);
            }
            catch(Exception err)
            {
                Console.WriteLine("[-] Error while modifying work : " + err);
            }
        }    
    }
}
