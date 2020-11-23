using System;
using System.Collections.Generic;
using System.Text;

namespace Console_EasySave
{
    public static class Language
    {
        public struct languageFileData{
            public string name;
            public string path;
            }
        public static List<languageFileData> allLanguages = new List<languageFileData>();

        public static string askStr = "Entrez le champ : ";
        public static string confirmDelete = "Etes-vous sur de vouloir supprimer ce travail ? (O/N)";
        public static string askName = "Entrez le nom du nouveau travail : ";
        public static string wichWorkField = "Quel champ voulez-vous modifier ?\n" +
            "1) nom \n" +
            "2) source \n" +
            "3) cible \n" +
            "4) type de sauvegarde\n";
        public static string askLanguage = "Choisissez la langue : ";
        public static string askTarget = "Entrez le chemin du répertoire cible : ";
        public static string askSource = "Entrez le chemin du répertoire source : ";
        public static string validate = "O";       
        public static string dismiss = "N";
        public static string askSaveType = "Choisissez le type de sauvegarde : ";
        static string mainMenu = "Bienvenue sur EasySave ! Choisissez une option ci-dessous \n" +
            "1) Executer un travail \n" +
            "2) Creer un travail \n" +
            "3) Modifier un travail \n" +
            "4) Supprimer un travail \n" +
            "5) Changer la langue \n" +
            "6) Quitter \n";      
        static string noWorks = "Il n'y a aucun travail pour le moment. Voulez-vous en créer un? (O/N)";
        static string getWorks = "Voici les différents Travaux : ";
        static string differential = "Differentielle";
        static string full = "Complete";
        public static List<languageFileData> GetAllLanguages()
        {
            return allLanguages;
        }
        internal static string GetDifferentialName()
        {
            return differential;
        }
        public static string GetAskLanguage()
        {
            return askLanguage;
        } 
        internal static string GetFullName()
        {
            return full;
        }
        public static string GetValidate()
        {
            return validate;
        }

        internal static void ChangeLanguage(int v)
        {
            Console.WriteLine("La langue a été changée");
        }

        internal static string GetAskName()
        {
            return askName;
        }

        public static string GetDismiss()
        {
            return dismiss;
        }

        internal static string GetAskTarget()
        {
            return askTarget;
        }

        

        internal static string GetAskSaveType()
        {
            return askSaveType;
        }

        internal static string GetAskStr()
        {
            return askStr;
        }

        internal static string GetConfirmDelete()
        {
            return confirmDelete;
        }

        internal static string GetWichWorkField()
        {
            return wichWorkField;
        }

        internal static string GetAskSource()
        {
            return askSource;
        }

        public static string GetMainMenu()
        {
            return mainMenu;
        }
        public static string GetGetWork()
        {
            return getWorks;
        }
        public static string GetNoWorks()
        {
            return noWorks;
        }
    }
}
