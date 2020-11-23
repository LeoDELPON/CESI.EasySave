using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace LanguageClass
{
    static class Language
    {
        // 1 = French, 2 = English
        private static string chosenLanguage = "fr";

        private static readonly string dataFilePath = @"C:\Users\REMI\source\repos\LanguageClass\vendor\";
        private static readonly string dataFileName = "translation.xml";

        private static readonly string regexFilePath = @"C:\Users\REMI\source\repos\LanguageClass\vendor\";
        private static readonly string regexFileName = "LangRegex.conf";

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

        public static string GetrequestedString(int stringID)
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

        
    }
}
