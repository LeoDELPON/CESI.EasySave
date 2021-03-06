﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace LanguageClass
{
    public static class Language
    {
          public struct languageFileData
          {
              public string name;
              public string path;
          }
          private static List<string> languages = new List<string>() {"fr", "en"};// could be cool if languages were separated in several files
           //Default language applied
          private static string chosenLanguage = "fr";

          //data file containing translation for the software
          private static readonly string dataFilePath = Environment.CurrentDirectory + @"\LanguagesFiles\";
          private static readonly string dataFileName = "translation.xml";

          //instantiation of the regular expression
          private static Regex regex;

          //Can be called to choose and change language
          public static void SetChosenLanguage(string newLanguage)
          {
              chosenLanguage = newLanguage;
          }
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
          private static string ChosenLanguage => chosenLanguage;

          private static string DataFilePath => dataFilePath;
          private static string DataFileName => dataFileName;
    }
}
