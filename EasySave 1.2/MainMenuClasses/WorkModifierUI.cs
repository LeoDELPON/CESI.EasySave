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

        public void Perform()
        {
            WorkVar wv = new WorkVar();
            WorkVar existingWorkVar = new WorkVar();
            wv.extension = new List<string>();
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
            existingWorkVar = getSpecifiedWorkVar(bs.works[valueReturned.value-1].Name, existingWorkVar);
            List<Tuple<string, List<string>, List<string>>> propertyQuestionsChange = new List<Tuple<string, List<string>, List<string>>>()
            {
                Tuple.Create(pm.GetPrintable("WorkName"), new List<string>(){ wv.name }, new List<string>(){ existingWorkVar.name }),
                Tuple.Create(pm.GetPrintable("WorkSource"), new List<string>() { wv.source}, new List<string>(){ existingWorkVar.source }),
                Tuple.Create(pm.GetPrintable("WorkTarget"), new List<string>() { wv.target}, new List<string>(){ existingWorkVar.target }),
                Tuple.Create(pm.GetPrintable("WorkSaveType"), new List<string>() { wv.typeSave.ToString() }, new List<string>() { existingWorkVar.typeSave.ToString() }),
                Tuple.Create(pm.GetPrintable("EXTENSION"), wv.extension, existingWorkVar.extension),
                Tuple.Create(pm.GetPrintable("KEY"), new List<string>() { wv.key}, new List<string>() { existingWorkVar.key})
            };
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
                    if(propertyQuestionsChange[counter].Item1 == pm.GetPrintable("EXTENSION"))
                    {
                        foreach (string ext in propertyQuestionsChange[counter].Item3)
                        {
                            propertyQuestionsChange[counter].Item2.Add(ext);
                        }
                    } else
                    {
                        propertyQuestionsChange[counter].Item2[0] = propertyQuestionsChange[counter].Item3[0];
                    }
                }

                if(counter == 5)
                {
                    isFinished = true;
                }
                counter++;
            } while (!isFinished);

            try
            {
                bs.confSaver.modifyEntireFile(bs.works[valueReturned.value - 1].Name, wv);
                bs.ModifyWork(
                    bs.works[valueReturned.value -1], 
                    propertyQuestionsChange[0].Item2[0], 
                    propertyQuestionsChange[1].Item2[0], 
                    propertyQuestionsChange[2].Item2[0], 
                    SaveTypeMethods.GetSaveTypeFromInt(Int32.Parse(propertyQuestionsChange[3].Item2[0])), 
                    propertyQuestionsChange[4].Item2, propertyQuestionsChange[5].Item2[0]
                    );
                Console.WriteLine("Work Modified with success !");
            }
            catch(Exception err)
            {
                Console.WriteLine("[-] Error while modifying work : " + err);
            }
        }    
    }
}
