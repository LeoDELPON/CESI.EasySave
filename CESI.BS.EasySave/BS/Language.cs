using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace CESI.BS.EasySave.BS
{
    public static class Language
    {
        // 1 = French, 2 = English
        private static string chosenLanguage = "fr";

        //data file containing translation for the software
        private static readonly string dataFilePath = Environment.CurrentDirectory + @"\LanguagesFiles\";
        private static readonly string dataFileName = "translation.xml";

        //instantiation of the regular expression
        private static Regex regex;

        //Can be called to choose and change language
        public static bool SetChosenLanguage(string newLanguage)
        {
            foreach(string lang in languages)
            {
                if(newLanguage == lang)
                {
                    chosenLanguage = newLanguage;
                    return true;
                }
            }
            return false;
        }
        public static List<string> languages = new List<string>() { "fr", "en" };
        public static List<string> GetAllLanguages() 
        {
            return languages;
        }

        //Can be called to get required string to print on view | act as interface for "ExtractStringByID"
        public static string GetRequestedString(int stringID)
        {
            return ExtractStringByID(stringID);
        }

        //Core function for "GetRequestedString to work, it apply regex to whole file and return wanted value
        private static string ExtractStringByID(int stringID)
        {
            regex = new Regex(@"(?s)" + stringID + @"<.+?" + chosenLanguage + @">(.*?)<\/.{2}>");
            Match match = regex.Match(File.ReadAllText(DataFilePath + DataFileName));
            return match.Groups[1].Value;
        }


        //Property of all class variables
        private static string DataFilePath => dataFilePath;
        private static string DataFileName => dataFileName;
    }
}
