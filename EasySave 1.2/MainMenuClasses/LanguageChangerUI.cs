using CESI.BS.EasySave.BS;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_1._2.MainMenuClasses
{
    class LanguageChangerUI : Printer, IMainMenuMethod
    {
        public string choice;
        List<InfoLanguage> listLanguage = new List<InfoLanguage>();
        struct InfoLanguage
        {
            public string name;
            public Uri path;

        }
        
        public LanguageChangerUI(BSEasySave bs, PrintManager pm) : base(bs, pm)
        {
            listLanguage.Add(new InfoLanguage { name = "Français", path = new Uri(@"\Language\fr-FR.xaml", UriKind.Relative) });
            listLanguage.Add(new InfoLanguage { name = "English", path = new Uri(@"\Language\en-US.xaml", UriKind.Relative) });
            
            
        }
        public void Perform()
        {
            Console.Clear();
            Console.WriteLine(pm.GetPrintable("SelectLanguage"));
            Console.WriteLine("1) Francais \n2)English");
            choice = Console.ReadLine().ToString();
            if (choice == "1")
            {
                pm.LoadLanguage(0);
            }
            else if (choice == "2")
            {
                pm.LoadLanguage(1);
            }
            else
            {
                Console.WriteLine("1)Error language");
            }


        }
    }
}
