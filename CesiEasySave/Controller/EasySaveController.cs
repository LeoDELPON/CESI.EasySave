﻿using CESI.BS.EasySave.BS;
using CesiEasySave.View;
using CesiEasySave.View.Interface;
using LanguageClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace CesiEasySave.Controller
{
    public class Controller
    {
        IEasySaveView view;
        CESI.BS.EasySave.BS.BSEasySave model;
        int LimitWork = 5;
        public struct WorkVar
        {
            public string name;
            public string source;
            public string target;
            public int typeSave;
        }
        public Controller()
        {
            view = new EasySaveViewConsole();
            model = new BSEasySave();
            ProgramLoop();
        }

        private void ProgramLoop()
        {


            string answerMainMenu;
            do
            {
                answerMainMenu = view.PrintMainMenu();
                string idWork;
                switch (answerMainMenu)
                {
                    case "1": //the user want to start one or several works
                        idWork = PrintWorks();

                        foreach (char id in idWork)
                        {
                            if (int.TryParse(id.ToString(), out int intID))

                            {
                                if (intID > -1 && intID < model.GetWorks().Count)
                                {
                                    model.GetWorks()[intID].save.Perform();//start the save
                                }

                            }

                        }
                        break;
                    case "2": //The user want to create a new work
                        CreateWork();
                        break;
                    case "3": // The user want to modify a work
                        ModifyWork();
                        break;
                    case "4": //The user want to delete a work
                        DeleteWork();
                        break;
                    case "5":
                        ChangeLanguage();
                        break;
                    default:
                        break;



                }
            } while (!answerMainMenu.Equals("6"));




        }
        private void ChangeLanguage()
        {
            if (Language.GetAllLanguages().Count > 0)
            {
                string languageSelected = view.AskLanguage();
                Language.ChangeLanguage(int.Parse(languageSelected));
            }
            else
            {
                Console.WriteLine("le changement de langage n'est pas encore implémenté");
            }


        }
        private void DeleteWork()
        {
            string selectedWork;
            int selectedWorkInt;
            do
            {
                selectedWork = PrintWorks();
            } while (!int.TryParse(selectedWork, out selectedWorkInt) && !selectedWork.Equals("workCreated"));
            if (selectedWork.Equals("workCreated"))
            {
                return;
            }
            foreach (char work in selectedWork)
            {
                if (int.Parse(work.ToString()) >= 0 && int.Parse(work.ToString()) < model.GetWorks().Count)
                    if (view.ConfirmDelete(model.GetWorks()[int.Parse(work.ToString())].name))
                    {
                        model.DeleteWork(selectedWorkInt);
                    }
            }

        }

        private string PrintWorks()
        {
            int strReturnInt;
            if (model.GetWorks().Count == 0)
            {
                if (view.PrintNoWork().ToUpper().Equals(Language.GetValidate()))
                {
                    CreateWork();
                }
                return "workCreated";

            }
            else
            {
                string strReturn;
                do
                {
                    do
                    {
                        strReturn = view.PrintWorks(model.GetWorks());
                    } while (!int.TryParse(strReturn, out strReturnInt));
                } while (strReturnInt < 0 || strReturnInt >= model.GetWorks().Count);
                return strReturn;
            }
        }
        public WorkVar AskDataWork()
        {
            WorkVar workvar = new WorkVar();
            int savetype;
            workvar.name = view.AskName();
            workvar.source = view.AskSource();
            workvar.target = view.AskTarget();
            string strSave = "";

            do
            {
                do
                {
                    strSave = view.AskSaveType(model.typeSave);
                } while (!int.TryParse(strSave, out savetype));
            } while ((savetype >= model.typeSave.Count) || savetype < 0);
            workvar.typeSave = int.Parse(strSave);// verifying that user input is an int

            return workvar;

        }
        private void CreateWork()
        {
            WorkVar workvar = AskDataWork();
            if (model.GetWorks().Count < 5)
            {
                try
                {
                    model.AddWork(workvar.name, workvar.source, workvar.target, model.typeSave[workvar.typeSave]); // add a work
                    Console.WriteLine("[+] Work succesfull add.");
                }
                catch (Exception error)
                {
                    Console.WriteLine("[-] ", error.Message);
                    Console.WriteLine("[-] ", error.StackTrace);
                    var inner = error.InnerException;

                    while(inner != null)
                    {
                        Console.WriteLine("[-] ", inner.StackTrace);
                        inner = inner.InnerException;
                    }
                }
            }
            else
            {
                //too much works saved
            }
        }
        private void ModifyWork()
        { //  select parameter
            string selectedWorkStr = PrintWorks();
            if (selectedWorkStr.Equals("workCreated"))
            {
                return;
            }
            foreach (char selectedWork in selectedWorkStr)
            {

                string answer = view.WichWorkField();
                foreach (char id in answer)
                {
                    if (int.TryParse(id.ToString(), out int fieldChosen))
                    {
                        if (fieldChosen < 5 && fieldChosen > 0)
                        {
                            if (fieldChosen == 4)
                            {
                                try
                                {
                                    int newSaveType = int.Parse(view.AskSaveType(model.typeSave));
                                    model.ModifyWork(model.GetWorks()[int.Parse(selectedWork.ToString())], fieldChosen, newSaveType);
                                }
                                catch (Exception error)
                                {
                                    Console.WriteLine("[-] " + error.Message);
                                    Console.WriteLine("[-] " + error.StackTrace);
                                }
                            }
                            else
                            {
                                try
                                {
                                    string newField = view.AskStr();
                                    model.ModifyWork(model.GetWorks()[int.Parse(selectedWork.ToString())], fieldChosen, newField);
                                }
                                catch (Exception error)
                                {
                                    Console.WriteLine(error.Message);
                                    Console.WriteLine(error.StackTrace);
                                }
                            }
                        }
                        //start save work
                    }

                }

            }
        }
    }
}