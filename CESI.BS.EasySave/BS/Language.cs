using System;
using System.Collections.Generic;
using System.Text;

namespace CESI.BS.EasySave.BS
{
    public static class Language
    {
        public static string askName = "Entrez le nom du nouveau travail : ";
        public static string askTarget = "Entrez le chemin du répertoire cible : ";
        public static string askSource = "Entrez le chemin du répertoire source : ";
        public static string validate = "O";
        public static string dismiss = "N";
        public static string askSaveType = "Choisissez le type de sauvegarde : ";
        static string mainMenu = "Bienvenue sur EasySave ! Choisissez une option ci-dessous \n" +
            "1) Afficher les travaux \n" +
            "2) Creer un travail \n" +
            "3) Modifier un travail \n" +
            "4) Supprimer un travail \n" +
            "5) Changer la langue \n";
        static string noWorks = "Il n'y a aucun travail pour le moment. Voulez-vous en créer un? (O/N)";
        static string getWorks = "Voici les différents Travaux : ";
        static string differential = "Differentielle";
        static string full = "Complete";
        internal static string getDifferentialName()
        {
            return differential;
        }
        internal static string getFullName()
        {
            return full;
        }
        public static string getValidate()
        {
            return validate;
        }

        internal static string getAskName()
        {
            return askName;
        }

        public static string getDismiss()
        {
            return dismiss;
        }

        internal static string getAskTarget()
        {
            return askTarget;
        }



        internal static string getAskSaveType()
        {
            return askSaveType;
        }

        internal static string getAskSource()
        {
            return askSource;
        }

        public static string getMainMenu()
        {
            return mainMenu;
        }
        public static string getGetWork()
        {
            return getWorks;
        }
        public static string getNoWorks()
        {
            return noWorks;
        }
    }
}