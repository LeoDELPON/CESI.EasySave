using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace LanguageClass
{
    static class Language
    {
        //Default language applied
        private static string chosenLanguage = "fr";

        //data file containing translation for the software
        private static readonly string dataFilePath = @"C:\Users\REMI\source\repos\LanguageClass\vendor\";
        private static readonly string dataFileName = "translation.xml";

        //Both parts of the regex used to parse String
        private static readonly String regexModifier = @"(?s)";
        private static readonly String regexP1 = @"+<.+";
        private static readonly String regexP2 = @">(.+?)<\/.{2}>";

        //instantiation of the regular expression
        private static Regex regex;

        //Can be called to choose and change language
        public static void SetChosenLanguage(string newLanguage)
        {
            chosenLanguage = newLanguage;
        }

        //Can be called to get required string to print on view | act as interface for "ExtractStringByID"
        public static string GetRequestedString(int stringID)
        {
            return ExtractStringByID(stringID);
        }

        //Core function for "GetRequestedString to work, it apply regex to whole file and return wanted value
        private static string ExtractStringByID(int stringID)
        {
            regex = new Regex(RegexModifier + stringID + RegexP1 + chosenLanguage + RegexP2);
            Match match = regex.Match(File.ReadAllText(DataFilePath + DataFileName));
            return match.Groups[1].Value;
        }

        //Property of all class variables
        private static string ChosenLanguage => chosenLanguage;

        private static string DataFilePath => dataFilePath;
        private static string DataFileName => dataFileName;

        private static string RegexModifier => regexModifier;
        private static string RegexP1 => regexP1;
        private static string RegexP2 => regexP2;
    }
}
