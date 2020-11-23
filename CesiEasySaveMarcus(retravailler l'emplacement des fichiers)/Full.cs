using System;
using System.Collections.Generic;
using System.Text;

namespace Console_EasySave
{
    class Full : Save
    {
        public override void Perform()
        {
            Console.WriteLine("Sauvegarde Complete");
        }
        override
     public string GetName()
        {
            return Language.GetFullName();
        }
    }

}
