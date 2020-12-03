﻿using CESI.BS.EasySave.BS;
using CesiEasySave.View;
using CesiEasySave.View.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using static CESI.BS.EasySave.BS.ConfSaver.ConfSaver;


namespace CesiEasySave.Controller
{
    public class Controller 
    {
        IEasySaveView view;
        CESI.BS.EasySave.BS.BSEasySave model;
        int limitWork = 5;
        
         Thread threadView = new Thread(ThreadViewFct);
        
        public Controller()
        {
           
            view = new EasySaveViewConsole();
            model = new BSEasySave();
            threadView.SetApartmentState(ApartmentState.STA);
            threadView.Start();
            
            
            FetchSavesConfs();
            ProgramLoop();
        }
        private static void ThreadViewFct(object o)
        {

            Window viewWindow= new WpfApp1.MainWindow();
            viewWindow.Show();
            System.Windows.Threading.Dispatcher.Run();





        }
        
        private void FetchSavesConfs()
        {
            List<WorkVar> works = model.confSaver.GetSavedWorks();
            foreach (WorkVar work in works)
            {
                model.AddWork(work.name, work.source, work.target, model.typeSave[work.typeSave].idTypeSave);
            }
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
                                    if (FolderBuilder.CheckFolder(model.GetWorks()[intID].Source))
                                    {
                                        model.GetWorks()[intID].Perform();//start the save
                                    }
                                    else
                                    {
                                        view.unreachablePath();
                                    }
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

                if (languageSelected.Length > 0)
                {
                    char charSelected = languageSelected[0];
                    if (int.TryParse(charSelected.ToString(), out int intSelected))
                    {
                        if (intSelected >= 0 && intSelected < Language.GetAllLanguages().Count)
                        {
                            Language.SetChosenLanguage(Language.GetAllLanguages()[intSelected]);
                        }
                    }
                }

                
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
                    if (!view.ConfirmDelete(model.GetWorks()[int.Parse(work.ToString())].Name))
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
                if (view.PrintNoWork().ToUpper().Equals(Language.GetRequestedString(8)))
                {
                    CreateWork();
                }
                return "workCreated";

            }
            else
            {
                string strReturn;
                bool outOfBoundWorks;
                do
                {
                    GetPerfectString(out strReturn, out outOfBoundWorks);

                } while (!int.TryParse(strReturn, out strReturnInt) || !outOfBoundWorks);
                return strReturn;

         
            }
        }

        private void GetPerfectString(out string strReturn, out bool outOfBoundWorks)
        {
            outOfBoundWorks = true;
            strReturn = view.PrintWorks(model.GetWorks());
            foreach (char ch in strReturn)
            {
                if (int.Parse(ch.ToString()) < 0 || int.Parse(ch.ToString()) > model.GetWorks().Count)
                {
                    outOfBoundWorks = false;
                }
            }
        }

        public WorkVar AskDataWork()
        {
            WorkVar workvar = new WorkVar();
            int savetype;
           
                workvar.name = view.AskName();
          
             do{
            workvar.source = view.AskSource();
                if (!FolderBuilder.CheckFolder(workvar.source))
                {
                    view.unreachablePath();
                }
            } while (!FolderBuilder.CheckFolder(workvar.source));
            do
            {
                workvar.target = view.AskTarget();
                if (!FolderBuilder.CheckFolder(workvar.target))
                {
                    view.unreachablePath();
                }
            } while (!FolderBuilder.CheckFolder(workvar.target));
            
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
           
            if (model.GetWorks().Count < limitWork)
            {
                try
                {
                WorkVar workvar = AskDataWork();
                 
                model.AddWork(workvar.name, workvar.source, workvar.target, model.typeSave[workvar.typeSave].idTypeSave); // add a work
                    model.confSaver.SaveWork(workvar);
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
                view.TooMuchWorks();
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
                                    model.confSaver.ModifyFile(model.GetWorks()[int.Parse(selectedWork.ToString())].Name, fieldChosen, newSaveType.ToString());
                                    model.ModifyWork(model.GetWorks()[int.Parse(selectedWork.ToString())], fieldChosen, newSaveType);
                                 
                                }
                                catch (Exception error)
                                {
                                    Console.WriteLine("[-] ", error.Message);
                                    Console.WriteLine("[-] ", error.StackTrace);
                                    var inner = error.InnerException;

                                    while (inner != null)
                                    {
                                        Console.WriteLine("[-] ", inner.StackTrace);
                                        inner = inner.InnerException;
                                    }
                                }
                            }
                            else
                            {
                                try
                                {
                                    string newField = view.AskStr();
                                    model.confSaver.ModifyFile(model.GetWorks()[int.Parse(selectedWork.ToString())].Name, fieldChosen, newField);
                                    model.ModifyWork(model.GetWorks()[int.Parse(selectedWork.ToString())], fieldChosen, newField);
                               
                                }
                                catch (Exception error)
                                {
                                    Console.WriteLine("[-] ", error.Message);
                                    Console.WriteLine("[-] ", error.StackTrace);
                                    var inner = error.InnerException;

                                    while (inner != null)
                                    {
                                        Console.WriteLine("[-] ", inner.StackTrace);
                                        inner = inner.InnerException;
                                    }
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