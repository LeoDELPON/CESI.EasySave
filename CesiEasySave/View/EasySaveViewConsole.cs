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
            try
            {
                Console.WriteLine(Language.GetRequestedString(1));// printMenu
                Console.WriteLine(Language.GetRequestedString(2));// printMenuChoice1
                Console.WriteLine(Language.GetRequestedString(3));// printMenuChoice2
                Console.WriteLine(Language.GetRequestedString(4));// printMenuChoice3
                Console.WriteLine(Language.GetRequestedString(5));// printMenuChoice4
                Console.WriteLine(Language.GetRequestedString(6));// printMenuChoice5
                Console.WriteLine(Language.GetRequestedString(17));// printMenuChoice6
                Console.WriteLine("[+] Success GetMainMenu()");
            }
            catch (Exception error)
            {

                Console.WriteLine("[-] ", error.Message);
                Console.WriteLine("[-] ", error.StackTrace);
                var inner = error.InnerException;

                while (inner != null)
                {
                    Console.WriteLine("[-] ", inner.StackTrace);
                    inner = inner.InnerException;
                }
            }

            return Console.ReadLine();


        }
        public string PrintWorks(List<Work> works)
        {
            try
            {
                Console.WriteLine(Language.GetRequestedString(13));//getSavedWork
                int i = 0;
                foreach (Work work in works)
                {
                    Console.WriteLine(i + ") " + work.name);
                    i++;
                }
                Console.WriteLine("[+] Success GetGetWork()");
            }
            catch (Exception error)
            {
                Console.WriteLine("[-] ", error.Message);
                Console.WriteLine("[-] ", error.StackTrace);
                var inner = error.InnerException;

                while (inner != null)
                {
                    Console.WriteLine("[-] ", inner.StackTrace);
                    inner = inner.InnerException;
                }
            }
            return Console.ReadLine();
        }
        public string PrintNoWork()
        {
            try
            {
                Console.WriteLine(Language.GetRequestedString(7));//print there is no work for now
                Console.WriteLine("[+] Success GetNoWorks()");

            }
            catch (Exception error)
            {
                Console.WriteLine("[-] ", error.Message);
                Console.WriteLine("[-] ", error.StackTrace);
                var inner = error.InnerException;

                while (inner != null)
                {
                    Console.WriteLine("[-] ", inner.StackTrace);
                    inner = inner.InnerException;
                }
            }
            return Console.ReadLine();
        }

        public string AskName()
        {
            Console.WriteLine(Language.GetRequestedString(10));//print askName
            return Console.ReadLine();
        }

        public string AskTarget()
        {
            Console.WriteLine(Language.GetRequestedString(12));//print askTarget
            return Console.ReadLine();
        }

        public string AskSource()
        {
            Console.WriteLine(Language.GetRequestedString(11));//print askSource
            return Console.ReadLine();
        }

        public string AskSaveType(List<Save> typeSave)
        {
            Console.WriteLine(Language.GetRequestedString(14));//print askSaveType
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
            Console.WriteLine(Language.GetRequestedString(18));//choose field to modify (name, source, target, or savetype)
            Console.WriteLine(Language.GetRequestedString(19));
            Console.WriteLine(Language.GetRequestedString(20));
            Console.WriteLine(Language.GetRequestedString(21));
            Console.WriteLine(Language.GetRequestedString(22));
            return Console.ReadLine();
        }

        public string AskStr()
        {
            Console.WriteLine(Language.GetRequestedString(23));
            return Console.ReadLine();
        }

        public bool ConfirmDelete(string name)
        {
            Console.WriteLine(Language.GetRequestedString(24));
            Console.WriteLine(name);
            return (Console.ReadLine().ToUpper().Equals(Language.GetRequestedString(9)));
        }

        public string AskLanguage()
        {
            int i = 0;
            Console.WriteLine(Language.GetRequestedString(25));
            foreach (string language in Language.GetAllLanguages())
            {
                Console.WriteLine(i + ") " + language);
                i++;
            }
            return Console.ReadLine();
        }

        public void TooMuchWorks()
        {
            Console.WriteLine(Language.GetRequestedString(26));
        }
    }
}
