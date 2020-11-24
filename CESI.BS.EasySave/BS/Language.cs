using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace LanguageClass
{
    public static class Language
    {
      /*  public struct languageFileData
        {
            public string name;
            public string path;
        }*/
        private static List<string> languages = new List<string>() {"fr", "en"};// could be cool if languages were separated in several files
        private static string chosenLanguage = "fr";

        private static readonly string dataFilePath = Environment.CurrentDirectory + @"\LanguagesFiles";
        private static readonly string dataFileName = "translation.xml";

        private static readonly string regexFilePath = Environment.CurrentDirectory + @"\LanguagesFiles";
        private static readonly string regexFileName = @"\LangRegex.conf";

        private static string requestedString;

        private static Regex groupRegex;
        private static readonly String groupRegexP1 = @"<dataGroup>\s+(";
        private static readonly String groupRegexP2 = @".+)\s+<\/dataGroup>";

        private static Regex langRegex;
        private static readonly String langRegexP1 = @"<";
        private static readonly String langRegexP2 = @">(.+)<\/(fr)>";

        private static Match match;


        public static void SetChosenLanguage(string newLanguage)
        {
            chosenLanguage = newLanguage;
        }

        public static string GetRequestedString(int stringID)
        {
            ExtractStringByID(stringID);
            return RequestedString;
        }
        private static void ExtractStringByID(int stringID)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(RegexFilePath + RegexFileName); ;
            groupRegex = new Regex(GroupRegexP1 + stringID + GroupRegexP2);
            langRegex = new Regex(LangRegexP1 + ChosenLanguage + LangRegexP2);
            File.ReadAllText(DataFilePath + DataFileName);
            match = groupRegex.Match(RequestedString);
            string temp = match.Groups[0].Value;

            while (file.ReadLine() != null)
            {
                match = langRegex.Match(file.ReadLine());
                if (match.Groups[0].Value != null)
                {
                    SetRequestedString(match.Groups[1].Value);
                }
            }

        }
        public static List<string> GetAllLanguages() 
        {
           return languages;
        }

        private static string ChosenLanguage => chosenLanguage;

        private static string DataFilePath => dataFilePath;
        private static string DataFileName => dataFileName;

        private static string RegexFilePath => regexFilePath;
        private static string RegexFileName => regexFileName;

        private static void SetRequestedString(string newString) => requestedString = newString;

        private static string RequestedString => requestedString;

        private static string GroupRegexP1 => groupRegexP1;
        private static string GroupRegexP2 => groupRegexP2;

        private static string LangRegexP1 => langRegexP1;
        private static string LangRegexP2 => langRegexP2;





        /*
        public struct languageFileData
        {
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
        public static string GetDifferentialName()
        {
            return differential;
        }
        public static string GetAskLanguage()
        {
            return askLanguage;
        }
        public static string GetFullName()
        {
            return full;
        }
        public static string GetValidate()
        {
            return validate;
        }

        public static void ChangeLanguage(int v)
        {
            Console.WriteLine("La langue a été changée");
        }

        public static string GetAskName()
        {
            return askName;
        }

        public static string GetDismiss()
        {
            return dismiss;
        }

        public static string GetAskTarget()
        {
            return askTarget;
        }



        public static string GetAskSaveType()
        {
            return askSaveType;
        }

        public static string GetAskStr()
        {
            return askStr;
        }

        public static string GetConfirmDelete()
        {
            return confirmDelete;
        }

        public static string GetWichWorkField()
        {
            return wichWorkField;
        }

        public static string GetAskSource()
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
        */
    }
}
