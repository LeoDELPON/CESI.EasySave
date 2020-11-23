using System;
using EasySave.View.Interface;
using System.Collections.Generic;
using System.Text;

namespace EasySave.View
{
    class ViewConsole : IEasySaveView
    {

        public string printMainMenu()
        {
            Console.WriteLine(Language.getMainMenu());
            return Console.ReadLine();
        }
        public string printWorks(List<Work> works)
        {
            Console.WriteLine(Language.getGetWork());
            int i = 0;
            foreach (Work work in works)
            {
                Console.WriteLine(i + ") " + work.name);
                i++;
            }
            return Console.ReadLine();
        }
        public string printNoWork()
        {
            Console.WriteLine(Language.getNoWorks());
            return Console.ReadLine();
        }

        public string askName()
        {
            Console.WriteLine(Language.getAskName());
            return Console.ReadLine();
        }

        public string askTarget()
        {
            Console.WriteLine(Language.getAskTarget());
            return Console.ReadLine();
        }

        public string askSource()
        {
            Console.WriteLine(Language.getAskSource());
            return Console.ReadLine();
        }

        public string askSaveType(List<Save> typeSave)
        {
            Console.WriteLine(Language.getAskSaveType());
            int i = 0;
            foreach (Save save in typeSave)
            {
                Console.WriteLine(i + ") " + save.getName());
                i++;
            }
            return Console.ReadLine();
        }


    }
}
