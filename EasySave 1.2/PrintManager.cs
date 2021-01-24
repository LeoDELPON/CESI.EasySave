using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace EasySave_1._2
{
    public class PrintManager
    {
        public DirectoryInfo currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
        public string languagesDirectory;
        public string extention = ".xaml";
        public string[] LanguageFilesNames { get; set; } = { "fr-FR", "en-US" };
        private readonly Dictionary<string, string> Printable = new Dictionary<string, string>();

        public PrintManager()
        {
            languagesDirectory = currentDirectory.Parent.Parent.Parent.Parent.FullName + @"\WpfApp1\WpfApp1\Language\";
        }
        public void LoadLanguage(int languageIndex)// load the language into the dictionnary. the index correspond to the index of the language in LanguageFilesNames;
        {
            if (languageIndex > LanguageFilesNames.Length - 1)
            {
                Console.WriteLine("ERROR");
                //error out of range
                return;
            }
            if (File.Exists(languagesDirectory + LanguageFilesNames[languageIndex] + extention))

            {
                StreamReader sr;
                sr = File.OpenText(languagesDirectory + LanguageFilesNames[languageIndex] + extention);
                string strReturn;
                while ((strReturn = sr.ReadLine()) != null)
                {
                    // regex for the languages
                    //     
                    if (strReturn.Trim().Length >= 23)
                    {
                        if (strReturn.Trim().Substring(0, 22).Equals("<system:String x:Key=\""))
                        {

                            Match mtch1 = Regex.Match(strReturn.Trim(), "x:Key=\".*\">");
                            Match mtch2 = Regex.Match(strReturn.Trim(), ">.*<");
                            //string result1 = mtch1.Value.Substring(6, mtch1.Value.Length - 2);
                            string result2 = mtch2.Value[1..^1];
                            string result1 = mtch1.Value[7..^2];
                            Printable[result1] = result2;
                            // Console.WriteLine(Printable[result1]);
                        }
                    }
                }
                sr.Close();
            }
        }
        public string GetPrintable(string key)
        {
            return Printable[key];
        }

    }
}
