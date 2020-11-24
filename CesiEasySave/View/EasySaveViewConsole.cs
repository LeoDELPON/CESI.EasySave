using System;
using System.Collections.Generic;
using System.Text;
using CesiEasySave.View.Interface;
using CESI.BS.EasySave.BS;
using CESI.BS.EasySave.DAL;
using LanguageClass;

namespace CesiEasySave.View
{
    class EasySaveViewConsole : IEasySaveView
    {

        public void Clear()
        {
            Console.Clear();
        }

        public string PrintMainMenu()
        {
            Console.WriteLine(Language.GetMainMenu());

            return Console.ReadLine();
        }
        public string PrintWorks(List<Work> works)
        {
            Console.WriteLine(Language.GetGetWork());
            int i = 0;
            foreach (Work work in works)
            {
                Console.WriteLine(i + ") " + work.name);
                i++;
            }
            return Console.ReadLine();
        }
        public string PrintNoWork()
        {
            Console.WriteLine(Language.GetNoWorks());
            return Console.ReadLine();
        }

        public string AskName()
        {
            Console.WriteLine(Language.GetAskName());
            return Console.ReadLine();
        }

        public string AskTarget()
        {
            Console.WriteLine(Language.GetAskTarget());
            return Console.ReadLine();
        }

        public string AskSource()
        {
            Console.WriteLine(Language.GetAskSource());
            return Console.ReadLine();
        }

        public string AskSaveType(List<Save> typeSave)
        {
            Console.WriteLine(Language.GetAskSaveType());
            int i = 0;
            foreach (Save save in typeSave)
            {
                Console.WriteLine(i + ") " + save.GetName());
                i++;
            }
            return Console.ReadLine();
        }

        public string WichWorkField() // we could do something in case we change the name/numbers of parametters in work
        {
            Console.WriteLine(Language.GetWichWorkField());
            return Console.ReadLine();
        }

        public string AskStr()
        {
            Console.WriteLine(Language.GetAskStr());
            return Console.ReadLine();
        }

        public bool ConfirmDelete(string name)
        {
            Console.WriteLine(Language.GetConfirmDelete());
            Console.WriteLine(name);
            return (Console.ReadLine().ToUpper().Equals(Language.GetValidate()));
        }

        public string AskLanguage()
        {
            int i = 0;
            Console.WriteLine(Language.GetAskLanguage());
            foreach (Language.languageFileData language in Language.GetAllLanguages())
            {
                Console.WriteLine(i + ") " + language.name);
                i++;
            }
            return Console.ReadLine();
        }


    }
}
