using System;
using System.Collections.Generic;
using System.Text;

namespace Console_EasySave
{
    class Differential : Save
    {
        override
             public void Perform()
        {
            Console.WriteLine("Sauvegarde differentielle");
        }

        override
        public string GetName()
        {
            return Language.GetDifferentialName();
        }
    }
}
