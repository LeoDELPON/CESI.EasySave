using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave.Controller
{
    public class Controller
    {
        View view;
        Model model;
        int LimitWork = 5;
        public Controller()
        {
            view = new ViewConsole();
            model = new Model();
            programLoop();
        }

        private void programLoop()
        {
            String answerMainMenu = view.printMainMenu();
            string idWork;
            switch (answerMainMenu)
            {
                case "1": //l'utilisateur demande d'afficher les travaux pour les executer
                    idWork = printWorks();
                    model.getWorks()[int.Parse(idWork)].save.perform();//execute le travail de sauvegarde
                    break;
                case "2":
                    createWork();
                    break;
                case "3":
                    idWork = printWorks();



            }



        }

        private string printWorks()
        {
            if (model.getWorks().Count == 0)
            {
                if (view.printNoWork().ToUpper().Equals(Language.getValidate()))
                {
                    createWork();

                }
                return "";
            }
            else
            {
                return view.printWorks(model.getWorks());
            }
        }
        private void createWork()
        {
            string name = view.askName();
            string source = view.askSource();
            string target = view.askTarget();
            string save;
            do
            {
                save = view.askSaveType(model.typeSave);
            } while (int.Parse(save) > model.typeSave.Count || int.Parse(save) < 0); // vérifier que l'entrée est bien un int
            model.addWork(name, source, target, model.typeSave[int.Parse(save)]); // ajouter un travail
        }
        private void modifyWork()
        { //  select parameter
            string name = view.askName();
            string source = view.askSource();
            string target = view.askTarget();
            string save;
            do
            {
                save = view.askSaveType(model.typeSave);
            } while (int.Parse(save) > model.typeSave.Count || int.Parse(save) < 0);
            model / modifyWork()// vérifier que l'entrée est bien un int
        }
    }
}